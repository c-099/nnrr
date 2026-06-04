using Newtonsoft.Json;
using static nnrr.Main;

namespace nnrr
{
    public partial class Save : Form
    {
        private const string SaveProfilesFileName = "saveprofiles.txt";
        private const string LegacyProfilesFileName = "settings.txt";

        public Save()
        {
            InitializeComponent();

            save_listBox.DrawMode = DrawMode.OwnerDrawFixed;
            save_listBox.DrawItem += (s, e) =>
            {
                if (e.Index < 0) return;

                Brush brush = Brushes.Crimson;

                // If the item is selected them change the back color.
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e = new DrawItemEventArgs(e.Graphics,
                                              e.Font,
                                              e.Bounds,
                                              e.Index,
                                              e.State ^ DrawItemState.Selected,
                                              Color.Black, // Forecolor.
                                              Color.Crimson); // Choose the Backcolor.

                    brush = Brushes.Black;
                }

                e.DrawBackground();
                e.Graphics.DrawString(save_listBox.Items[e.Index].ToString(), e.Font ?? save_listBox.Font, brush, e.Bounds, StringFormat.GenericDefault);
            };
        }

        private void Save_Load(object sender, EventArgs e)
        {
            save_listBox.Items.Clear();
            settingsList = LoadProfiles();

            foreach (var s in settingsList)
            {
                save_listBox.Items.Add(s.name);
            }

            save_listBox.Items.Add("New");
        }

        private static List<RecoilSettings> LoadProfiles()
        {
            List<RecoilSettings> profiles = ReadProfiles(SaveProfilesFileName);

            if (profiles.Count == 0 && !File.Exists(SaveProfilesFileName))
                profiles = ReadProfiles(LegacyProfilesFileName);

            return profiles;
        }

        private static List<RecoilSettings> ReadProfiles(string fileName)
        {
            if (!File.Exists(fileName))
                return new List<RecoilSettings>();

            try
            {
                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<RecoilSettings>>(json) ?? new List<RecoilSettings>();
            }
            catch (JsonException)
            {
                return new List<RecoilSettings>();
            }
            catch (IOException)
            {
                return new List<RecoilSettings>();
            }
        }

        private static void WriteProfiles()
        {
            using (var writer = new StreamWriter(SaveProfilesFileName))
            {
                writer.Write(JsonConvert.SerializeObject(settingsList, Formatting.Indented));
            }
        }

        private void save_saveButton_Click(object sender, EventArgs e)
        {
            if (save_listBox.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a save slot.");
                return;
            }

            if (save_listBox.SelectedIndex != save_listBox.Items.Count - 1)
                if (Tmp.ConfirmBox("Save", "Are you sure you want to overwrite save?") != DialogResult.OK)
                    return;

            string value = "Name";
            if (Tmp.InputBox("Save", "Save name:", ref value) == DialogResult.OK)
            {
                save_listBox.Items[save_listBox.SelectedIndex] = value;

                RecoilSettings saveSettings = new RecoilSettings()
                {
                    name = value,
                    recoil_d = settings.recoil_d,
                    recoil_h = settings.recoil_h,
                    recoil_v = settings.recoil_v,
                    random_range = settings.random_range,
                    random_impact = settings.random_impact,
                    stage_list = new List<RecoilStages>(settings.stage_list)
                };

                if (save_listBox.SelectedIndex == save_listBox.Items.Count - 1)
                {
                    settingsList.Add(saveSettings);
                    save_listBox.Items.Add("New");
                }
                else
                {
                    settingsList.RemoveAt(save_listBox.SelectedIndex);
                    settingsList.Insert(save_listBox.SelectedIndex, saveSettings);
                }

                WriteProfiles();
            }
        }

        private void save_loadButton_Click(object sender, EventArgs e)
        {
            if (save_listBox.SelectedIndex < 0)
            {
                MessageBox.Show("Select a slot first", "Error");
                return;
            }
            else if (save_listBox.SelectedIndex == save_listBox.Items.Count - 1)
            {
                MessageBox.Show("Cannot load empty slot", "Error");
            }
            else
            {
                RecoilSettings loadSettings = settingsList[save_listBox.SelectedIndex];
                Main? mainForm = Application.OpenForms.OfType<Main>().FirstOrDefault();
                if (mainForm == null)
                {
                    MessageBox.Show("Main window is not available", "Error");
                    return;
                }

                mainForm.ApplySettings(loadSettings);
            }                
        }

        private void save_deleteButton_Click(object sender, EventArgs e)
        {
            if (save_listBox.SelectedIndex < 0)
            {
                MessageBox.Show("Select a slot first");
                return;
            }

            if (Tmp.ConfirmBox("Delete", "Are you sure you want to delete?") == DialogResult.OK)
            {
                if (save_listBox.SelectedIndex != save_listBox.Items.Count - 1)
                {
                    settingsList.RemoveAt(save_listBox.SelectedIndex);
                    save_listBox.Items.RemoveAt(save_listBox.SelectedIndex);
                }

                WriteProfiles();
            }
        }
    }
}
