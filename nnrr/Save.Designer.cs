namespace nnrr
{
    partial class Save
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
            tableLayoutPanel1 = new TableLayoutPanel();
            save_listBox = new ListBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            save_loadButton = new Button();
            save_deleteButton = new Button();
            save_saveButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(save_listBox, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.Size = new Size(162, 373);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // save_listBox
            // 
            save_listBox.BackColor = Color.Black;
            save_listBox.BorderStyle = BorderStyle.None;
            save_listBox.Dock = DockStyle.Fill;
            save_listBox.ForeColor = Color.Crimson;
            save_listBox.FormattingEnabled = true;
            save_listBox.Items.AddRange(new object[] { "New" });
            save_listBox.Location = new Point(3, 3);
            save_listBox.Name = "save_listBox";
            save_listBox.Size = new Size(156, 197);
            save_listBox.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(save_loadButton);
            flowLayoutPanel1.Controls.Add(save_deleteButton);
            flowLayoutPanel1.Controls.Add(save_saveButton);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(38, 203);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(86, 170);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // save_loadButton
            // 
            save_loadButton.BackColor = Color.Black;
            save_loadButton.FlatAppearance.BorderColor = Color.FromArgb(80, 0, 20);
            save_loadButton.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            save_loadButton.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            save_loadButton.FlatStyle = FlatStyle.Flat;
            save_loadButton.ForeColor = Color.Crimson;
            save_loadButton.Location = new Point(0, 10);
            save_loadButton.Margin = new Padding(0, 10, 0, 20);
            save_loadButton.Name = "save_loadButton";
            save_loadButton.Size = new Size(86, 33);
            save_loadButton.TabIndex = 5;
            save_loadButton.Text = "Load";
            save_loadButton.UseVisualStyleBackColor = false;
            save_loadButton.Click += save_loadButton_Click;
            // 
            // save_deleteButton
            // 
            save_deleteButton.BackColor = Color.Black;
            save_deleteButton.FlatAppearance.BorderColor = Color.FromArgb(80, 0, 20);
            save_deleteButton.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            save_deleteButton.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            save_deleteButton.FlatStyle = FlatStyle.Flat;
            save_deleteButton.ForeColor = Color.Crimson;
            save_deleteButton.Location = new Point(0, 63);
            save_deleteButton.Margin = new Padding(0, 0, 0, 20);
            save_deleteButton.Name = "save_deleteButton";
            save_deleteButton.Size = new Size(86, 33);
            save_deleteButton.TabIndex = 6;
            save_deleteButton.Text = "Delete";
            save_deleteButton.UseVisualStyleBackColor = false;
            save_deleteButton.Click += save_deleteButton_Click;
            // 
            // save_saveButton
            // 
            save_saveButton.BackColor = Color.Black;
            save_saveButton.FlatAppearance.BorderColor = Color.FromArgb(80, 0, 20);
            save_saveButton.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            save_saveButton.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            save_saveButton.FlatStyle = FlatStyle.Flat;
            save_saveButton.ForeColor = Color.Crimson;
            save_saveButton.Location = new Point(0, 116);
            save_saveButton.Margin = new Padding(0, 0, 0, 20);
            save_saveButton.Name = "save_saveButton";
            save_saveButton.Size = new Size(86, 33);
            save_saveButton.TabIndex = 7;
            save_saveButton.Text = "Save";
            save_saveButton.UseVisualStyleBackColor = false;
            save_saveButton.Click += save_saveButton_Click;
            // 
            // Save
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(162, 373);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.Crimson;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Save";
            ShowInTaskbar = false;
            Text = "nnrr.save";
            Load += Save_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ListBox save_listBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button save_loadButton;
        private Button save_deleteButton;
        private Button save_saveButton;
    }
}