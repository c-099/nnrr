namespace nnrr
{
    partial class StageContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button_menu = new Button();
            button_srecoil_horizontal = new Button();
            button_srecoil_vertical = new Button();
            button_srecoil_delay = new Button();
            nud_starttime = new NumericUpDown();
            contextMenuStrip1 = new ContextMenuStrip(components);
            repeatToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_starttime).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.BackColor = Color.Black;
            flowLayoutPanel1.Controls.Add(button_menu);
            flowLayoutPanel1.Controls.Add(button_srecoil_horizontal);
            flowLayoutPanel1.Controls.Add(button_srecoil_vertical);
            flowLayoutPanel1.Controls.Add(button_srecoil_delay);
            flowLayoutPanel1.Controls.Add(nud_starttime);
            flowLayoutPanel1.ForeColor = Color.Crimson;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(0, 13, 0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(388, 40);
            flowLayoutPanel1.TabIndex = 28;
            flowLayoutPanel1.WrapContents = false;
            // 
            // button_menu
            // 
            button_menu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button_menu.BackColor = Color.Black;
            button_menu.FlatAppearance.BorderColor = Color.FromArgb(80, 0, 20);
            button_menu.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            button_menu.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            button_menu.FlatStyle = FlatStyle.Flat;
            button_menu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_menu.ForeColor = Color.Crimson;
            button_menu.Location = new Point(5, 0);
            button_menu.Margin = new Padding(5, 0, 25, 0);
            button_menu.Name = "button_menu";
            button_menu.Size = new Size(40, 40);
            button_menu.TabIndex = 22;
            button_menu.Text = "≡";
            button_menu.TextAlign = ContentAlignment.TopCenter;
            button_menu.UseVisualStyleBackColor = false;
            // 
            // button_srecoil_horizontal
            // 
            button_srecoil_horizontal.BackColor = Color.Black;
            button_srecoil_horizontal.BackgroundImageLayout = ImageLayout.None;
            button_srecoil_horizontal.FlatAppearance.BorderColor = Color.Crimson;
            button_srecoil_horizontal.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            button_srecoil_horizontal.FlatAppearance.MouseOverBackColor = Color.PaleTurquoise;
            button_srecoil_horizontal.FlatStyle = FlatStyle.Flat;
            button_srecoil_horizontal.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_srecoil_horizontal.ForeColor = Color.Crimson;
            button_srecoil_horizontal.Location = new Point(70, 0);
            button_srecoil_horizontal.Margin = new Padding(0, 0, 15, 0);
            button_srecoil_horizontal.Name = "button_srecoil_horizontal";
            button_srecoil_horizontal.Size = new Size(60, 40);
            button_srecoil_horizontal.TabIndex = 1;
            button_srecoil_horizontal.Text = "-3.5";
            button_srecoil_horizontal.UseVisualStyleBackColor = false;
            // 
            // button_srecoil_vertical
            // 
            button_srecoil_vertical.BackColor = Color.Black;
            button_srecoil_vertical.BackgroundImageLayout = ImageLayout.None;
            button_srecoil_vertical.FlatAppearance.BorderColor = Color.Crimson;
            button_srecoil_vertical.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            button_srecoil_vertical.FlatAppearance.MouseOverBackColor = Color.PaleTurquoise;
            button_srecoil_vertical.FlatStyle = FlatStyle.Flat;
            button_srecoil_vertical.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_srecoil_vertical.ForeColor = Color.Crimson;
            button_srecoil_vertical.Location = new Point(145, 0);
            button_srecoil_vertical.Margin = new Padding(0, 0, 25, 0);
            button_srecoil_vertical.Name = "button_srecoil_vertical";
            button_srecoil_vertical.Size = new Size(60, 40);
            button_srecoil_vertical.TabIndex = 2;
            button_srecoil_vertical.Text = "-3.5";
            button_srecoil_vertical.UseVisualStyleBackColor = false;
            // 
            // button_srecoil_delay
            // 
            button_srecoil_delay.BackColor = Color.Black;
            button_srecoil_delay.BackgroundImageLayout = ImageLayout.None;
            button_srecoil_delay.FlatAppearance.BorderColor = Color.Crimson;
            button_srecoil_delay.FlatAppearance.MouseDownBackColor = Color.DodgerBlue;
            button_srecoil_delay.FlatAppearance.MouseOverBackColor = Color.PaleTurquoise;
            button_srecoil_delay.FlatStyle = FlatStyle.Flat;
            button_srecoil_delay.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_srecoil_delay.ForeColor = Color.Crimson;
            button_srecoil_delay.Location = new Point(230, 0);
            button_srecoil_delay.Margin = new Padding(0, 0, 25, 0);
            button_srecoil_delay.Name = "button_srecoil_delay";
            button_srecoil_delay.Size = new Size(45, 40);
            button_srecoil_delay.TabIndex = 3;
            button_srecoil_delay.Text = "15";
            button_srecoil_delay.UseVisualStyleBackColor = false;
            // 
            // nud_starttime
            // 
            nud_starttime.BackColor = Color.Black;
            nud_starttime.BorderStyle = BorderStyle.None;
            nud_starttime.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nud_starttime.ForeColor = Color.FromArgb(100, 0, 40);
            nud_starttime.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            nud_starttime.Location = new Point(300, 2);
            nud_starttime.Margin = new Padding(0, 2, 0, 0);
            nud_starttime.Maximum = new decimal(new int[] { 9990, 0, 0, 0 });
            nud_starttime.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nud_starttime.Name = "nud_starttime";
            nud_starttime.Size = new Size(88, 34);
            nud_starttime.TabIndex = 13;
            nud_starttime.TextAlign = HorizontalAlignment.Center;
            nud_starttime.Value = new decimal(new int[] { 1000, 0, 0, 0 });
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
            repeatToolStripMenuItem.Click += repeatToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(210, 24);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // StageContainer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "StageContainer";
            Size = new Size(858, 471);
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nud_starttime).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button button_menu;
        private Button button_srecoil_horizontal;
        private Button button_srecoil_vertical;
        private Button button_srecoil_delay;
        private NumericUpDown nud_starttime;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem repeatToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
    }
}
