using System.Runtime.InteropServices;

namespace nnrr
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            BackColor = Theme.Primary;
            ForeColor = Theme.Accent;

            button_close.Click += (s, e) => { Close(); };
            panel_titlebar.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, 0xA1, 0x2, 0);
                }
            };

            checkBox1.Checked = Main.b_checkRB;

            comboBox_activationKey.Items.AddRange(Main.ActivationKeyOptions);
            comboBox_activationKey.SelectedItem = Main.ActivationKeyOptions.FirstOrDefault(option => option.Key == Main.activationKey)
                ?? Main.ActivationKeyOptions[0];

            button_random_range.Text = Main.settings.random_range.ToString("0.0");
            button_random_range.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                {
                    Main.settings.random_range = (float)Math.Round(Main.settings.random_range - 0.1f, 1);

                    if (Main.settings.random_range <= 0.1f)
                        Main.settings.random_range = 0.1f;
                }
                else
                    Main.settings.random_range = (float)Math.Round(Main.settings.random_range + 0.1f, 1);
                Main.SetRandomDefaults(Main.settings.random_range, Main.settings.random_impact);
                button_random_range.Text = Main.settings.random_range.ToString("0.0");
            };
            button_random_range.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        Main.settings.random_range = (float)Math.Round(Main.settings.random_range + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        Main.settings.random_range = (float)Math.Round(Main.settings.random_range - 0.1f, 1);
                        if (Main.settings.random_range <= 0.1f)
                            Main.settings.random_range = 0.1f;
                        break;
                }
                Main.SetRandomDefaults(Main.settings.random_range, Main.settings.random_impact);
                button_random_range.Text = Main.settings.random_range.ToString("0.0");
            };

            button_random_impact.Text = Main.settings.random_impact.ToString("0.0");
            button_random_impact.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    Main.settings.random_impact = (float)Math.Round(Main.settings.random_impact - 0.1f, 1);
                else
                    Main.settings.random_impact = (float)Math.Round(Main.settings.random_impact + 0.1f, 1);
                Main.SetRandomDefaults(Main.settings.random_range, Main.settings.random_impact);
                button_random_impact.Text = Main.settings.random_impact.ToString("0.0");
            };
            button_random_impact.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        Main.settings.random_impact = (float)Math.Round(Main.settings.random_impact + 0.1f, 1);
                        break;
                    case MouseButtons.Right:
                        Main.settings.random_impact = (float)Math.Round(Main.settings.random_impact - 0.1f, 1);
                        break;
                }
                Main.SetRandomDefaults(Main.settings.random_range, Main.settings.random_impact);
                button_random_impact.Text = Main.settings.random_impact.ToString("0.0");
            };
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not CheckBox chk) return;

            Main.b_checkRB = chk.Checked;
        }

        private void comboBox_activationKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_activationKey.SelectedItem is not Main.ActivationKeyOption option)
                return;

            Main.SetActivationKey(option.Key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Formula: \n" +
                            "       RANGE * ( 1 + IMPACT * (random value between 0.0 and 1.0) )\n" +
                            "Example: \n" +
                            "       range: 0.7  impact: 1.0 is about 70-140% \n" +
                            "       range: 0.8  impact: 0.5 is about 80-120%",
                            "nnrr");
        }

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();
    }
}
