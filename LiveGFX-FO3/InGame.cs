using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveGFX_FO3
{
    public partial class InGame : UserControl
    {
        public InGame()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel34_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton25_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel39_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel45_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel50_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void bo1_checkbox_OnChange(object sender, EventArgs e)
        {
            bo1_checkbox.Checked = true;
            bo3_checkbox.Checked = false;
            bo5_checkbox.Checked = false;
        }

        private void bo3_checkbox_OnChange(object sender, EventArgs e)
        {
            bo3_checkbox.Checked = true;
            bo1_checkbox.Checked = false;
            bo5_checkbox.Checked = false;
        }

        private void bo5_checkbox_OnChange(object sender, EventArgs e)
        {
            bo5_checkbox.Checked = true;
            bo1_checkbox.Checked = false;
            bo3_checkbox.Checked = false;
        }
    }
}
