using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nnrr
{
    public partial class StageContainer : UserControl
    {
        public StageContainer()
        {
            InitializeComponent();

            //button_menu.Click += (s, e) =>
            //{
            //    ContextMenuStrip menu = new ContextMenuStrip();
            //    var deleteItem = new ToolStripMenuItem("Delete Stage");
            //    deleteItem.Click += (s2, e2) =>
            //    {
            //        this.Parent?.Controls.Remove(this);
            //    };
            //    menu.Items.Add(deleteItem);
            //    menu.Show(button_menu, new Point(0, button_menu.Height));
            //};
        }

        private void repeatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
