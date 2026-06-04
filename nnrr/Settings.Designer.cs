namespace nnrr
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkBox1 = new CheckBox();
            button_random_range = new ModernButton();
            button_random_impact = new ModernButton();
            label1 = new Label();
            button1 = new ModernButton();
            button2 = new ModernButton();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            comboBox_activationKey = new ComboBox();
            panel_titlebar = new Panel();
            button_close = new ModernButton();
            label_title = new Label();
            panel_titlebar.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = Color.FromArgb(17, 17, 17);
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.Font = new Font("Segoe UI", 9F);
            checkBox1.ForeColor = Color.FromArgb(255, 71, 87);
            checkBox1.Location = new Point(16, 44);
            checkBox1.Margin = new Padding(10, 10, 10, 15);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(154, 19);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Right click activation";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button_random_range
            // 
            button_random_range.BackColor = Color.FromArgb(45, 45, 45);
            button_random_range.BorderColor = Color.FromArgb(255, 71, 87);
            button_random_range.BorderRadius = 0;
            button_random_range.ClickColor = Color.FromArgb(75, 75, 75);
            button_random_range.FlatStyle = FlatStyle.Flat;
            button_random_range.Font = new Font("Segoe UI", 9F);
            button_random_range.ForeColor = Color.FromArgb(255, 255, 255);
            button_random_range.HoverColor = Color.FromArgb(60, 60, 60);
            button_random_range.Location = new Point(16, 209);
            button_random_range.Margin = new Padding(15, 30, 0, 0);
            button_random_range.Name = "button_random_range";
            button_random_range.NormalColor = Color.FromArgb(45, 45, 45);
            button_random_range.Size = new Size(72, 40);
            button_random_range.TabIndex = 3;
            button_random_range.Text = "-3.5";
            button_random_range.UseVisualStyleBackColor = false;
            // 
            // button_random_impact
            // 
            button_random_impact.BackColor = Color.FromArgb(45, 45, 45);
            button_random_impact.BorderColor = Color.FromArgb(255, 71, 87);
            button_random_impact.BorderRadius = 0;
            button_random_impact.ClickColor = Color.FromArgb(75, 75, 75);
            button_random_impact.FlatStyle = FlatStyle.Flat;
            button_random_impact.Font = new Font("Segoe UI", 9F);
            button_random_impact.ForeColor = Color.FromArgb(255, 255, 255);
            button_random_impact.HoverColor = Color.FromArgb(60, 60, 60);
            button_random_impact.Location = new Point(104, 209);
            button_random_impact.Margin = new Padding(15, 30, 0, 0);
            button_random_impact.Name = "button_random_impact";
            button_random_impact.NormalColor = Color.FromArgb(45, 45, 45);
            button_random_impact.Size = new Size(72, 40);
            button_random_impact.TabIndex = 4;
            button_random_impact.Text = "-3.5";
            button_random_impact.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.FromArgb(255, 71, 87);
            label1.Location = new Point(14, 139);
            label1.Margin = new Padding(3, 15, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(98, 20);
            label1.TabIndex = 5;
            label1.Text = "Randomness";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(45, 45, 45);
            button1.BorderColor = Color.FromArgb(255, 71, 87);
            button1.BorderRadius = 0;
            button1.ClickColor = Color.FromArgb(75, 75, 75);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 8F);
            button1.ForeColor = Color.FromArgb(255, 71, 87);
            button1.HoverColor = Color.FromArgb(60, 60, 60);
            button1.Location = new Point(16, 166);
            button1.Margin = new Padding(15, 15, 0, 0);
            button1.Name = "button1";
            button1.NormalColor = Color.FromArgb(45, 45, 45);
            button1.Size = new Size(72, 28);
            button1.TabIndex = 7;
            button1.Text = "reset";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(45, 45, 45);
            button2.BorderColor = Color.FromArgb(255, 71, 87);
            button2.BorderRadius = 0;
            button2.ClickColor = Color.FromArgb(75, 75, 75);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 8F);
            button2.ForeColor = Color.FromArgb(255, 71, 87);
            button2.HoverColor = Color.FromArgb(60, 60, 60);
            button2.Location = new Point(104, 166);
            button2.Margin = new Padding(15, 15, 0, 0);
            button2.Name = "button2";
            button2.NormalColor = Color.FromArgb(45, 45, 45);
            button2.Size = new Size(72, 28);
            button2.TabIndex = 8;
            button2.Text = "help";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 8F);
            label2.ForeColor = Color.FromArgb(160, 160, 160);
            label2.Location = new Point(29, 194);
            label2.Name = "label2";
            label2.Size = new Size(36, 13);
            label2.TabIndex = 9;
            label2.Text = "range";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 8F);
            label3.ForeColor = Color.FromArgb(160, 160, 160);
            label3.Location = new Point(121, 194);
            label3.Name = "label3";
            label3.Size = new Size(42, 13);
            label3.TabIndex = 10;
            label3.Text = "impact";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 8F);
            label4.ForeColor = Color.FromArgb(160, 160, 160);
            label4.Location = new Point(16, 76);
            label4.Name = "label4";
            label4.Size = new Size(77, 13);
            label4.TabIndex = 11;
            label4.Text = "activation key";
            // 
            // comboBox_activationKey
            // 
            comboBox_activationKey.BackColor = Color.FromArgb(45, 45, 45);
            comboBox_activationKey.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_activationKey.FlatStyle = FlatStyle.Flat;
            comboBox_activationKey.Font = new Font("Segoe UI", 9F);
            comboBox_activationKey.ForeColor = Color.FromArgb(255, 255, 255);
            comboBox_activationKey.FormattingEnabled = true;
            comboBox_activationKey.Location = new Point(16, 92);
            comboBox_activationKey.Name = "comboBox_activationKey";
            comboBox_activationKey.Size = new Size(176, 23);
            comboBox_activationKey.TabIndex = 12;
            comboBox_activationKey.SelectedIndexChanged += comboBox_activationKey_SelectedIndexChanged;
            // 
            // panel_titlebar
            // 
            panel_titlebar.BackColor = Color.FromArgb(30, 30, 30);
            panel_titlebar.Controls.Add(button_close);
            panel_titlebar.Controls.Add(label_title);
            panel_titlebar.Dock = DockStyle.Top;
            panel_titlebar.Location = new Point(0, 0);
            panel_titlebar.Margin = new Padding(0);
            panel_titlebar.Name = "panel_titlebar";
            panel_titlebar.Size = new Size(220, 28);
            panel_titlebar.TabIndex = 13;
            // 
            // button_close
            // 
            button_close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_close.BackColor = Color.Transparent;
            button_close.BorderColor = Color.Transparent;
            button_close.BorderRadius = 0;
            button_close.ClickColor = Color.DarkRed;
            button_close.FlatStyle = FlatStyle.Flat;
            button_close.Font = new Font("Segoe UI", 9F);
            button_close.ForeColor = Color.FromArgb(241, 242, 246);
            button_close.HoverColor = Color.Red;
            button_close.Location = new Point(185, 0);
            button_close.Margin = new Padding(0);
            button_close.Name = "button_close";
            button_close.NormalColor = Color.FromArgb(30, 30, 30);
            button_close.Size = new Size(35, 28);
            button_close.TabIndex = 1;
            button_close.Text = "✕";
            button_close.UseVisualStyleBackColor = false;
            // 
            // label_title
            // 
            label_title.AutoSize = true;
            label_title.Font = new Font("Segoe UI", 9F);
            label_title.ForeColor = Color.FromArgb(160, 160, 160);
            label_title.Location = new Point(11, 5);
            label_title.Name = "label_title";
            label_title.Size = new Size(70, 15);
            label_title.TabIndex = 0;
            label_title.Text = "nnrr.settings";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(17, 17, 17);
            ClientSize = new Size(220, 268);
            Controls.Add(panel_titlebar);
            Controls.Add(comboBox_activationKey);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(button_random_range);
            Controls.Add(button_random_impact);
            Controls.Add(checkBox1);
            ForeColor = Color.FromArgb(255, 71, 87);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "nnrr.settings";
            TopMost = true;
            panel_titlebar.ResumeLayout(false);
            panel_titlebar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private ModernButton button_random_range;
        private ModernButton button_random_impact;
        private Label label1;
        private ModernButton button1;
        private ModernButton button2;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox comboBox_activationKey;
        private Panel panel_titlebar;
        private ModernButton button_close;
        private Label label_title;
    }
}
