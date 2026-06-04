using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace nnrr
{
    public partial class Main : Form
    {
        [Serializable]
        public struct RecoilSettings
        {
            public string name { get; set; } = "Default";
            public float recoil_h { get; set; } = 0f;
            public float recoil_v { get; set; } = 1.8f;
            public int recoil_d { get; set; } = 14;
            //(0.7, 1)70~140%  (0.8, 0.5)80~120%
            public float random_range { get; set; } = 0.7f;
            public float random_impact { get; set; } = 1.0f;

            public List<RecoilStages> stage_list = new List<RecoilStages>();
            public RecoilSettings() { }
        }


        public struct RecoilStages
        {
            public bool repeat { get; set; } = false;
            public int stage_starttime { get; set; } = 1000;
            public float recoil_h { get; set; } = 0f;
            public float recoil_v { get; set; } = 0f;
            public int recoil_d { get; set; } = 7;
            public int index { get; set; } = 0;
            public RecoilStages() { }
        }

        private sealed class AppSettings
        {
            public string activation_key { get; set; } = nameof(KeyboardHook.VKeys.INSERT);
            public float default_random_range { get; set; } = 0.7f;
            public float default_random_impact { get; set; } = 1.0f;
            public RecoilSettings? last_profile { get; set; }
        }

        public sealed class ActivationKeyOption
        {
            public string DisplayName { get; }
            public KeyboardHook.VKeys Key { get; }

            public ActivationKeyOption(string displayName, KeyboardHook.VKeys key)
            {
                DisplayName = displayName;
                Key = key;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }

        public static readonly ActivationKeyOption[] ActivationKeyOptions =
        {
            new ActivationKeyOption("Insert", KeyboardHook.VKeys.INSERT),
            new ActivationKeyOption("Home", KeyboardHook.VKeys.HOME),
            new ActivationKeyOption("End", KeyboardHook.VKeys.END),
            new ActivationKeyOption("Page Up", KeyboardHook.VKeys.PRIOR),
            new ActivationKeyOption("Page Down", KeyboardHook.VKeys.NEXT),
            new ActivationKeyOption("F8", KeyboardHook.VKeys.F8),
            new ActivationKeyOption("F9", KeyboardHook.VKeys.F9),
            new ActivationKeyOption("F10", KeyboardHook.VKeys.F10),
            new ActivationKeyOption("F11", KeyboardHook.VKeys.F11),
            new ActivationKeyOption("F12", KeyboardHook.VKeys.F12),
        };

        public static RecoilSettings settings = new RecoilSettings();
        public static List<RecoilSettings> settingsList = new List<RecoilSettings>();
        public static KeyboardHook.VKeys activationKey { get; private set; } = KeyboardHook.VKeys.INSERT;

        private MouseHook mouseHook;
        private KeyboardHook? keyboardHook;
        private static bool b_mouseRB = false;
        private static bool b_mouseLB = false;
        public static bool b_checkRB { get; set; } = true;
        private static bool b_insertEnabled = true;
        private static bool b_insertKeyHeld = false;
        private static object stageLock = new object();
        private Thread? recoilThread;

        private Random rand;
        private const string AppSettingsFileName = "settings.txt";
        private const string SaveProfilesFileName = "saveprofiles.txt";
        private const int BaseClientHeight = 210;
        private const int StageRowHeight = 52;
        private const int StageRowTopMargin = 10;
        private const int StageMenuWidth = 40;
        private const int StageValueWidth = 72;
        private const int StageStartTimeWidth = 88;
        private const int StageControlHeight = 40;
        private const int StageMenuRightMargin = 20;
        private const int StageColumnRightMargin = 15;
        private const int StageDelayRightMargin = 25;
        private const int StageStartTimeTopMargin = 2;
        private string lastElapsedText = "--ms";

        public Main()
        {
            InitializeComponent();
            LoadAppSettings();
            SetupUI();
            rand = new Random();
            mouseHook = new MouseHook();
            keyboardHook = new KeyboardHook();
            mouseHook.RightButtonDown += (s) => { b_mouseRB = true; };
            mouseHook.RightButtonUp += (s) => { b_mouseRB = false; };
            mouseHook.LeftButtonUp += (s) => { b_mouseLB = false; };
            mouseHook.LeftButtonDown += MouseHook_LeftButtonDown;
            mouseHook.Install();
            keyboardHook!.KeyDown += KeyboardHook_KeyDown;
            keyboardHook!.KeyUp += KeyboardHook_KeyUp;
            keyboardHook!.Install();
            Application.ApplicationExit += (s, e) =>
            {
                SaveAppSettings();
                mouseHook.Uninstall();
                keyboardHook?.Uninstall();
            };

            this.BackColor = Theme.Primary;
            this.ForeColor = Theme.Text;

            // TitleBar Logic
            button_close.Click += (s, e) => { Application.Exit(); };
            button_minimize.Click += (s, e) => { this.WindowState = FormWindowState.Minimized; };

            panel_titlebar.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, 0xA1, 0x2, 0);
                }
            };
        }

        private static string ActivationKeyDisplayName
        {
            get
            {
                return ActivationKeyOptions.FirstOrDefault(option => option.Key == activationKey)?.DisplayName ?? activationKey.ToString();
            }
        }

        private void LoadAppSettings()
        {
            activationKey = KeyboardHook.VKeys.INSERT;
            RecoilSettings defaultSettings = new RecoilSettings();
            settings.random_range = defaultSettings.random_range;
            settings.random_impact = defaultSettings.random_impact;

            if (!File.Exists(AppSettingsFileName))
                return;

            try
            {
                string json = File.ReadAllText(AppSettingsFileName);
                if (MigrateLegacyProfilesIfNeeded(json))
                    return;

                AppSettings? appSettings = JsonConvert.DeserializeObject<AppSettings>(json);

                if (appSettings == null)
                    return;

                ActivationKeyOption? option = ActivationKeyOptions.FirstOrDefault(candidate =>
                    string.Equals(candidate.Key.ToString(), appSettings.activation_key, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(candidate.DisplayName, appSettings.activation_key, StringComparison.OrdinalIgnoreCase));

                if (option != null)
                    activationKey = option.Key;

                if (IsValidRandomRange(appSettings.default_random_range))
                    settings.random_range = appSettings.default_random_range;

                if (IsValidRandomImpact(appSettings.default_random_impact))
                    settings.random_impact = appSettings.default_random_impact;

                if (appSettings.last_profile.HasValue)
                    ApplySettings(appSettings.last_profile.Value);
            }
            catch (JsonException)
            {
                activationKey = KeyboardHook.VKeys.INSERT;
            }
            catch (IOException)
            {
                activationKey = KeyboardHook.VKeys.INSERT;
            }
        }

        private static bool MigrateLegacyProfilesIfNeeded(string json)
        {
            try
            {
                List<RecoilSettings>? legacyProfiles = JsonConvert.DeserializeObject<List<RecoilSettings>>(json);
                if (legacyProfiles == null)
                    return false;

                if (!File.Exists(SaveProfilesFileName))
                    File.WriteAllText(SaveProfilesFileName, JsonConvert.SerializeObject(legacyProfiles, Formatting.Indented));

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        internal static void SaveAppSettings()
        {
            var appSettings = new AppSettings
            {
                activation_key = activationKey.ToString(),
                default_random_range = settings.random_range,
                default_random_impact = settings.random_impact,
                last_profile = settings,
            };

            try
            {
                File.WriteAllText(AppSettingsFileName, JsonConvert.SerializeObject(appSettings, Formatting.Indented));
            }
            catch (IOException)
            {
            }
        }

        private static bool IsValidRandomRange(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value) && value >= 0.1f;
        }

        private static bool IsValidRandomImpact(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        public static void SetActivationKey(KeyboardHook.VKeys key)
        {
            if (!ActivationKeyOptions.Any(option => option.Key == key))
                key = KeyboardHook.VKeys.INSERT;

            activationKey = key;
            b_insertKeyHeld = false;
            SaveAppSettings();

            foreach (Main mainForm in Application.OpenForms.OfType<Main>())
            {
                mainForm.UpdateStatusLine();
            }
        }

        public static void SetRandomDefaults(float randomRange, float randomImpact)
        {
            settings.random_range = Math.Max(0.1f, randomRange);
            settings.random_impact = randomImpact;
            SaveAppSettings();
        }

        private void MouseHook_LeftButtonDown(MouseHook.MSLLHOOKSTRUCT mouseStruct)
        {
            if (this.DesktopBounds.Contains(Cursor.Position)) return;

            b_mouseLB = true;
            if (!b_insertEnabled)
                return;

            if (b_checkRB && !b_mouseRB)
                return;

            if (recoilThread != null && recoilThread.IsAlive) return;

            recoilThread = new Thread(MouseThread);
            recoilThread.IsBackground = true;
            recoilThread.Start();
        }

        private void KeyboardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (key != activationKey || b_insertKeyHeld)
                return;

            b_insertKeyHeld = true;
            b_insertEnabled = !b_insertEnabled;
            UpdateStatusLine();
        }

        private void KeyboardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (key == activationKey)
                b_insertKeyHeld = false;
        }

        private void UpdateStatusLine()
        {
            label_statusLine.Text = $"{ActivationKeyDisplayName}: {(b_insertEnabled ? "ON" : "OFF")} | {lastElapsedText}";
            label_statusLine.ForeColor = b_insertEnabled ? Theme.Accent : Theme.TextDim;
        }

        private float Randish()
        {
            float r = settings.random_range * (1 + settings.random_impact * rand.NextSingle());
            return r;
        }

        int r_h = 0;
        int r_v = 0;

        private void DoRecoil(float h, float v)
        {
            int ih = 0;
            int iv = 0;
            float rh = 0f;
            float rv = 0f;
            int move_h = 0;
            int move_v = 0;

            //separate whole and fractional parts
            if (h > 0f)
            {
                h = Randish() * h;
                ih = (int)Math.Floor(h);
            }
            else if (h < 0f)
            {
                h = Randish() * h;
                ih = (int)Math.Ceiling(h);
            }

            rh = Math.Abs(h) - (float)Math.Floor(Math.Abs(h));

            if (v > 0f)
            {
                v = Randish() * v;
                iv = (int)Math.Floor(v);

            }
            else if (v < 0f)
            {
                v = Randish() * v;
                iv = (int)Math.Ceiling(v);
            }
            rv = Math.Abs(v) - (float)Math.Floor(Math.Abs(v));

            // add whole parts to move buffer
            move_h += ih;
            move_v += iv;

            // add fractional parts to move buffer when they add up to 1
            r_h += (int)Math.Floor(rh * 1000f);
            r_v += (int)Math.Floor(rv * 1000f);
            if (r_h >= 1000)
            {
                if (settings.recoil_h < 0f)
                    move_h += -1;
                else
                    move_h += 1;

                r_h -= 1000;
            }

            if (r_v >= 1000)
            {
                if (settings.recoil_v < 0f)
                    move_v += -1;
                else
                    move_v += 1;

                r_v -= 1000;
            }

            //move mouse if buffer not empty
            if (move_h != 0 || move_v != 0)
                MoveMouse(move_h, move_v);
        }

        private void MouseThread()
        {
            if (settings.recoil_h == 0f && settings.recoil_v == 0f)
                return;

            float o_recoil_h = settings.recoil_h;
            float o_recoil_v = settings.recoil_v;
            int o_recoil_d = settings.recoil_d;

            int currentStage = 0;
            int? repeatBoundaryMs = null;

            var now = DateTime.Now;

            while (b_mouseLB)
            {
                DoRecoil(settings.recoil_h, settings.recoil_v);

                int sleep = rand.Next(5);

                if (rand.NextSingle() > 0.5f)
                    Thread.Sleep(settings.recoil_d + sleep);
                else
                {
                    sleep = settings.recoil_d - sleep;

                    if (sleep > 1)
                        Thread.Sleep(sleep);
                    else
                        Thread.Sleep(1);
                }

                lock (stageLock)
                {
                    int elapsedMs = (int)(DateTime.Now - now).TotalMilliseconds;

                    if (repeatBoundaryMs.HasValue && elapsedMs >= repeatBoundaryMs.Value)
                    {
                        settings.recoil_h = o_recoil_h;
                        settings.recoil_v = o_recoil_v;
                        settings.recoil_d = o_recoil_d;
                        currentStage = 0;
                        repeatBoundaryMs = null;
                        now = DateTime.Now;
                        continue;
                    }

                    if (currentStage < settings.stage_list.Count)
                    {
                        if (elapsedMs >= settings.stage_list[currentStage].stage_starttime)
                        {
                            RecoilStages stage = settings.stage_list[currentStage];

                            settings.recoil_h = stage.recoil_h;
                            settings.recoil_v = stage.recoil_v;
                            settings.recoil_d = stage.recoil_d;

                            if (stage.repeat)
                                repeatBoundaryMs = GetRepeatBoundaryMs(currentStage);

                            currentStage++;
                        }
                    }
                }

                if (settings.recoil_h == 0f && settings.recoil_v == 0f)
                    break;
            }

            //Display elapsed time
            label_statusLine.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                lastElapsedText = (DateTime.Now - now).TotalMilliseconds.ToString("0") + "ms";
                UpdateStatusLine();
            });

            settings.recoil_h = o_recoil_h;
            settings.recoil_v = o_recoil_v;
            settings.recoil_d = o_recoil_d;
        }

        private static int GetRepeatBoundaryMs(int stageIndex)
        {
            if (stageIndex + 1 < settings.stage_list.Count)
                return settings.stage_list[stageIndex + 1].stage_starttime;

            int currentStart = settings.stage_list[stageIndex].stage_starttime;
            int duration = stageIndex == 0
                ? currentStart
                : currentStart - settings.stage_list[stageIndex - 1].stage_starttime;

            return currentStart + Math.Max(duration, 1);
        }

        #region UI Setup + Button Logic
        private void SetupUI()
        {
            ApplyFirstStageLayout();
            UpdateStatusLine();

            button_recoil_horizontal.Text = settings.recoil_h.ToString("0.0");
            button_recoil_horizontal.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    settings.recoil_h = (float)Math.Round(settings.recoil_h - 0.1f, 1);
                else
                    settings.recoil_h = (float)Math.Round(settings.recoil_h + 0.1f, 1);
                button_recoil_horizontal.Text = settings.recoil_h.ToString("0.0");
            };
            button_recoil_horizontal.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        settings.recoil_h = (float)Math.Round(settings.recoil_h + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        settings.recoil_h = (float)Math.Round(settings.recoil_h - 0.1f, 1);
                        break;
                }
                button_recoil_horizontal.Text = settings.recoil_h.ToString("0.0");
            };

            button_recoil_vertical.Text = settings.recoil_v.ToString("0.0");
            button_recoil_vertical.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    settings.recoil_v = (float)Math.Round(settings.recoil_v - 0.1f, 1);
                else
                    settings.recoil_v = (float)Math.Round(settings.recoil_v + 0.1f, 1);

                button_recoil_vertical.Text = settings.recoil_v.ToString("0.0");
            };
            button_recoil_vertical.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        settings.recoil_v = (float)Math.Round(settings.recoil_v + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        settings.recoil_v = (float)Math.Round(settings.recoil_v - 0.1f, 1);
                        break;
                }
                button_recoil_vertical.Text = settings.recoil_v.ToString("0.0");
            };

            button_recoil_delay.Text = settings.recoil_d.ToString();
            button_recoil_delay.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                {
                    settings.recoil_d--;
                    if (settings.recoil_d < 1) settings.recoil_d = 1;
                }
                else
                    settings.recoil_d++;
                button_recoil_delay.Text = settings.recoil_d.ToString();
            };
            button_recoil_delay.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        settings.recoil_d++;
                        break;
                    case MouseButtons.Right:
                        settings.recoil_d--;
                        if (settings.recoil_d < 1) settings.recoil_d = 1;
                        break;
                }
                button_recoil_delay.Text = settings.recoil_d.ToString();
            };

            button_settings.Click += (s, e) =>
            {
                Settings? settingsForm = Application.OpenForms.OfType<Settings>().FirstOrDefault();
                if (settingsForm != null)
                {
                    settingsForm.Close();
                }
                else
                {
                    settingsForm = new Settings();
                    settingsForm.Show();
                }
            };

            button_addStage.Click += (s, e) =>
            {
                AddStage();
            };

            button_save.Click += (s, e) =>
            {
                if (Application.OpenForms.OfType<Save>().Any())
                {
                    Application.OpenForms.OfType<Save>().First().BringToFront();
                }
                else
                {
                    var saveForm = new Save();
                    saveForm.Show();
                }
            };
        }

        private void ApplyFirstStageLayout()
        {
            lblPlaceholder.Size = new Size(StageMenuWidth, 20);
            lblPlaceholder.Margin = new Padding(0, 0, StageMenuRightMargin, 0);

            label1.Text = "Horiz";
            label1.Size = new Size(StageValueWidth, 20);
            label1.Margin = new Padding(0, 0, StageColumnRightMargin, 0);

            label2.Text = "Vert";
            label2.Size = new Size(StageValueWidth, 20);
            label2.Margin = new Padding(0, 0, StageColumnRightMargin, 0);

            label3.Text = "Delay";
            label3.Size = new Size(StageValueWidth, 20);
            label3.Margin = new Padding(0, 0, StageDelayRightMargin, 0);

            placeholder_menu.Size = new Size(StageMenuWidth, StageControlHeight);
            placeholder_menu.Margin = new Padding(0, 0, StageMenuRightMargin, 0);

            button_recoil_horizontal.Size = new Size(StageValueWidth, StageControlHeight);
            button_recoil_horizontal.Margin = new Padding(0, 0, StageColumnRightMargin, 0);

            button_recoil_vertical.Size = new Size(StageValueWidth, StageControlHeight);
            button_recoil_vertical.Margin = new Padding(0, 0, StageColumnRightMargin, 0);

            button_recoil_delay.Size = new Size(StageValueWidth, StageControlHeight);
            button_recoil_delay.Margin = new Padding(0, 0, StageDelayRightMargin, 0);
        }

        private void ClearAllStages()
        {
            lock (stageLock)
            {
                settings.stage_list.Clear();
            }
            flowLayoutPanel_allStages.Controls.Clear();
            UpdateStageLayoutHeight();
        }

        private void UpdateStageLayoutHeight()
        {
            ClientSize = new Size(ClientSize.Width, BaseClientHeight + (settings.stage_list.Count * StageRowHeight));
        }

        private void RefreshRecoilControls()
        {
            button_recoil_horizontal.Text = settings.recoil_h.ToString("0.0");
            button_recoil_vertical.Text = settings.recoil_v.ToString("0.0");
            button_recoil_delay.Text = settings.recoil_d.ToString();
        }

        private void RebuildStages(IEnumerable<RecoilStages> stages)
        {
            var rebuiltStages = stages.Select((stage, index) =>
            {
                stage.index = index;
                return stage;
            }).ToList();

            ClearAllStages();

            foreach (var stage in rebuiltStages)
            {
                AddStage(stage);
            }
        }

        internal void ApplySettings(RecoilSettings loadedSettings)
        {
            settings = loadedSettings;
            settings.stage_list ??= new List<RecoilStages>();

            var stages = new List<RecoilStages>(settings.stage_list);
            settings.stage_list.Clear();

            RefreshRecoilControls();
            RebuildStages(stages);
        }

        private void AddStage(RecoilStages? stageIn = null)
        {
            RecoilStages rs;
            if (stageIn != null)
            {
                rs = (RecoilStages)stageIn;

                if (rs.index == 0)
                    ClearAllStages();
            }
            else
            {
                lock (stageLock)
                {
                    if (settings.stage_list.Count == 0)
                        rs = new RecoilStages();
                    else
                    {
                        rs = settings.stage_list[settings.stage_list.Count - 1];
                        rs.index = settings.stage_list.Count;

                        if (rs.index > 0)
                            rs.stage_starttime = settings.stage_list[rs.index - 1].stage_starttime + 1000;
                    }
                }
            }

            lock (stageLock)
            {
                settings.stage_list.Add(rs);
            }

            UpdateStageLayoutHeight();

            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            ModernButton button_menu = new ModernButton();
            ModernButton button_srecoil_horizontal = new ModernButton();
            ModernButton button_srecoil_vertical = new ModernButton();
            ModernButton button_srecoil_delay = new ModernButton();
            NumericUpDown nud_starttime = new NumericUpDown();
            ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
            ToolStripMenuItem repeatToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem deleteToolStripMenuItem = new ToolStripMenuItem();

            ((System.ComponentModel.ISupportInitialize)nud_starttime).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.BackColor = Theme.Primary; // Theme color
            flowLayoutPanel1.Controls.Add(button_menu);
            flowLayoutPanel1.Controls.Add(button_srecoil_horizontal);
            flowLayoutPanel1.Controls.Add(button_srecoil_vertical);
            flowLayoutPanel1.Controls.Add(button_srecoil_delay);
            flowLayoutPanel1.Controls.Add(nud_starttime);
            flowLayoutPanel1.ForeColor = Theme.Accent;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0, StageRowTopMargin, 0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(388, StageControlHeight);
            flowLayoutPanel1.TabIndex = 28;
            flowLayoutPanel1.WrapContents = false;
            // 
            // button_srecoil_horizontal
            // 
            button_srecoil_horizontal.BackColor = Theme.Secondary;
            button_srecoil_horizontal.BorderRadius = 0;
            button_srecoil_horizontal.Font = Theme.FontRegular;
            button_srecoil_horizontal.ForeColor = Theme.Text;
            button_srecoil_horizontal.Location = new Point(60, 0);
            button_srecoil_horizontal.Margin = new Padding(0, 0, StageColumnRightMargin, 0);
            button_srecoil_horizontal.Name = "button_srecoil_horizontal";
            button_srecoil_horizontal.Size = new Size(StageValueWidth, StageControlHeight);
            button_srecoil_horizontal.TabIndex = 1;
            button_srecoil_horizontal.Text = rs.recoil_h.ToString("0.0");
            button_srecoil_horizontal.UseVisualStyleBackColor = false;
            button_srecoil_horizontal.MouseWheel += (s, e) =>
            {
                if (s is not ModernButton button) return;

                if (e.Delta < 0)
                    rs.recoil_h = (float)Math.Round(rs.recoil_h - 0.1f, 1);
                else
                    rs.recoil_h = (float)Math.Round(rs.recoil_h + 0.1f, 1);

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button.Text = rs.recoil_h.ToString("0.0");
            };
            button_srecoil_horizontal.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        rs.recoil_h = (float)Math.Round(rs.recoil_h + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        rs.recoil_h = (float)Math.Round(rs.recoil_h - 0.1f, 1);
                        break;
                }

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button_srecoil_horizontal.Text = rs.recoil_h.ToString("0.0");
            };
            // 
            // button_srecoil_vertical
            // 
            button_srecoil_vertical.BackColor = Theme.Secondary;
            button_srecoil_vertical.BorderRadius = 0;
            button_srecoil_vertical.Font = Theme.FontRegular;
            button_srecoil_vertical.ForeColor = Theme.Text;
            button_srecoil_vertical.Location = new Point(137, 0);
            button_srecoil_vertical.Margin = new Padding(0, 0, StageColumnRightMargin, 0);
            button_srecoil_vertical.Name = "button_srecoil_vertical";
            button_srecoil_vertical.Size = new Size(StageValueWidth, StageControlHeight);
            button_srecoil_vertical.TabIndex = 2;
            button_srecoil_vertical.Text = rs.recoil_v.ToString("0.0");
            button_srecoil_vertical.UseVisualStyleBackColor = false;
            button_srecoil_vertical.MouseWheel += (s, e) =>
            {
                if (s is not ModernButton button) return;

                if (e.Delta < 0)
                    rs.recoil_v = (float)Math.Round(rs.recoil_v - 0.1f, 1);
                else
                    rs.recoil_v = (float)Math.Round(rs.recoil_v + 0.1f, 1);

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button.Text = rs.recoil_v.ToString("0.0");
            };
            button_srecoil_vertical.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        rs.recoil_v = (float)Math.Round(rs.recoil_v + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        rs.recoil_v = (float)Math.Round(rs.recoil_v - 0.1f, 1);
                        break;
                }

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button_srecoil_vertical.Text = rs.recoil_v.ToString("0.0");
            };
            // 
            // button_srecoil_delay
            // 
            button_srecoil_delay.BackColor = Theme.Secondary;
            button_srecoil_delay.BorderRadius = 0;
            button_srecoil_delay.Font = Theme.FontRegular;
            button_srecoil_delay.ForeColor = Theme.Text;
            button_srecoil_delay.Location = new Point(229, 0);
            button_srecoil_delay.Margin = new Padding(0, 0, StageDelayRightMargin, 0);
            button_srecoil_delay.Name = "button_srecoil_delay";
            button_srecoil_delay.Size = new Size(StageValueWidth, StageControlHeight);
            button_srecoil_delay.TabIndex = 3;
            button_srecoil_delay.Text = rs.recoil_d.ToString();
            button_srecoil_delay.UseVisualStyleBackColor = false;
            button_srecoil_delay.MouseWheel += (s, e) =>
            {
                if (s is not ModernButton button) return;

                if (e.Delta < 0)
                {
                    rs.recoil_d--;
                    if (rs.recoil_d < 1) rs.recoil_d = 1;
                }
                else
                    rs.recoil_d++;

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button.Text = rs.recoil_d.ToString();
            };
            button_srecoil_delay.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        rs.recoil_d++;
                        break;
                    case MouseButtons.Right:
                        rs.recoil_d--;
                        if (rs.recoil_d < 1) rs.recoil_d = 1;
                        break;
                }

                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                button_srecoil_delay.Text = rs.recoil_d.ToString();
            };
            // 
            // nud_starttime
            // 
            nud_starttime.BackColor = Theme.Secondary;
            nud_starttime.ForeColor = Theme.Text;
            nud_starttime.BorderStyle = BorderStyle.None;
            nud_starttime.Font = Theme.FontRegular;
            nud_starttime.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nud_starttime.Location = new Point(300, 2);
            nud_starttime.Margin = new Padding(0, StageStartTimeTopMargin, 0, 0);
            nud_starttime.Maximum = new decimal(new int[] { 9990, 0, 0, 0 });
            if (rs.index > 0)
                nud_starttime.Minimum = new decimal(new int[] { settings.stage_list[rs.index - 1].stage_starttime + 10, 0, 0, 0 });
            else
                nud_starttime.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nud_starttime.Name = "nud_starttime";
            nud_starttime.Size = new Size(StageStartTimeWidth, 34);
            nud_starttime.TabIndex = 13;
            nud_starttime.TextAlign = HorizontalAlignment.Center;
            nud_starttime.Value = new decimal(new int[] { rs.stage_starttime, 0, 0, 0 });
            nud_starttime.ValueChanged += (s, e) =>
            {
                if (s is not NumericUpDown nud) return;

                rs.stage_starttime = Convert.ToInt32(nud.Value);
                lock (stageLock) { settings.stage_list[rs.index] = rs; }

                for (int i = 1; i <= settings.stage_list.Count - (rs.index + 1); i++)
                {
                    NumericUpDown? temp = Controls.Find("nud_starttime", true)
                        .OfType<NumericUpDown>()
                        .ElementAtOrDefault(rs.index + i);
                    if (temp == null) continue;

                    temp.Minimum = new decimal(new int[] { rs.stage_starttime + 10, 0, 0, 0 });
                }
            };
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { repeatToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(211, 80);
            // 
            // repeatToolStripMenuItem
            // 
            repeatToolStripMenuItem.Name = "repeatToolStripMenuItem";
            repeatToolStripMenuItem.Size = new Size(210, 24);
            repeatToolStripMenuItem.Text = "Repeat";
            repeatToolStripMenuItem.Checked = rs.repeat;
            repeatToolStripMenuItem.Click += (s, e) =>
            {
                rs.repeat = !rs.repeat;
                lock (stageLock) { settings.stage_list[rs.index] = rs; }
                repeatToolStripMenuItem.Checked = rs.repeat;
            };
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(210, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += (s, e) =>
            {
                if (Tmp.ConfirmBox("Delete Stage", "Are you sure you want to delete this stage?") == DialogResult.OK)
                {
                    List<RecoilStages> remainingStages;
                    lock (stageLock)
                    {
                        remainingStages = settings.stage_list
                            .Where(stage => stage.index != rs.index)
                            .ToList();
                    }

                    RebuildStages(remainingStages);
                }
            };
            // 
            // button_menu
            // 
            button_menu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button_menu.BackColor = Theme.Secondary;
            button_menu.BorderRadius = 0; // Square
            button_menu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_menu.ForeColor = Theme.TextDim;
            button_menu.Location = new Point(5, 0);
            button_menu.Margin = new Padding(0, 0, StageMenuRightMargin, 0);
            button_menu.Name = "button_menu";
            button_menu.Size = new Size(StageMenuWidth, StageControlHeight);
            button_menu.TabIndex = 22;
            button_menu.Text = "⌫";
            button_menu.TextAlign = ContentAlignment.TopCenter;
            button_menu.UseVisualStyleBackColor = false;
            button_menu.ContextMenuStrip = contextMenuStrip1;
            button_menu.Click += (s, e) =>
            {
                contextMenuStrip1.Show(button_menu, new Point(0, button_menu.Height));
            };
            //
            // add to stages flow panel
            //
            flowLayoutPanel_allStages.Controls.Add(flowLayoutPanel1);
            flowLayoutPanel_allStages.ResumeLayout(false);
            flowLayoutPanel_allStages.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nud_starttime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private static void MoveMouse(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }

        internal void LoadStage(RecoilStages stage, bool first = false)
        {
            if (first)
            {
                RefreshRecoilControls();
            }

            AddStage(stage);
        }

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
