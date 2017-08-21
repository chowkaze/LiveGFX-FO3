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
    public partial class Table : UserControl
    {
        private List<string> teamname = new List<string>();
        private List<string> bracket = new List<string>();
        db sqldb = new db();
        public Table()
        {
            InitializeComponent();
        }

        private void bracket_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = bracket_title.Text;
            val[0] = "title";
            val[1] = select;
            exist = sqldb.checkexist("bracket", val);
            string[] type = new string[21];
            string[] info = new string[21];
            type[0] = "title";
            type[1] = "team1";
            type[2] = "team2";
            type[3] = "team3";
            type[4] = "team4";
            type[5] = "team5";
            type[6] = "team6";
            type[7] = "team7";
            type[8] = "team8";
            type[9] = "team9";
            type[10] = "team10";
            type[11] = "score1";
            type[12] = "score2";
            type[13] = "score3";
            type[14] = "score4";
            type[15] = "score5";
            type[16] = "score6";
            type[17] = "score7";
            type[18] = "score8";
            type[19] = "score9";
            type[20] = "score10";
            info[0] = bracket_title.Text;
            info[1] = this.team1.GetItemText(this.team1.SelectedItem);
            info[2] = this.team2.GetItemText(this.team2.SelectedItem);
            info[3] = this.team3.GetItemText(this.team3.SelectedItem);
            info[4] = this.team4.GetItemText(this.team4.SelectedItem);
            info[5] = this.team5.GetItemText(this.team5.SelectedItem);
            info[6] = this.team6.GetItemText(this.team6.SelectedItem);
            info[7] = this.team7.GetItemText(this.team7.SelectedItem);
            info[8] = this.team8.GetItemText(this.team8.SelectedItem);
            info[9] = this.team9.GetItemText(this.team9.SelectedItem);
            info[10] = this.team10.GetItemText(this.team10.SelectedItem);
            info[11] = score1.Text;
            info[12] = score2.Text;
            info[13] = score3.Text;
            info[14] = score4.Text;
            info[15] = score5.Text;
            info[16] = score6.Text;
            info[17] = score7.Text;
            info[18] = score8.Text;
            info[19] = score9.Text;
            info[20] = score10.Text;
            if (exist == 0)
            {
                sqldb.insertdata("bracket", info, type);
                bracket = sqldb.getdata("title", "bracket");
                bracket_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("bracket", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bracket_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = bracket_title.Text;
                val[0] = "title";
                val[1] = select;
                sqldb.deletedata("bracket", val);
                bracket_title.Text = "";
                this.team1.SelectedItem = "";
                this.team2.SelectedItem = "";
                this.team3.SelectedItem = "";
                this.team4.SelectedItem = "";
                this.team5.SelectedItem = "";
                this.team6.SelectedItem = "";
                this.team7.SelectedItem = "";
                this.team8.SelectedItem = "";
                this.team9.SelectedItem = "";
                this.team10.SelectedItem = "";
                score1.Text = "";
                score2.Text = "";
                score3.Text = "";
                score4.Text = "";
                score5.Text = "";
                score6.Text = "";
                score7.Text = "";
                score8.Text = "";
                score9.Text = "";
                score10.Text = "";
            }
        }

        private void bracket_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.bracket_combo.GetItemText(this.bracket_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "bracket", "title", select);
            bracket_title.Text = info[0];
            this.team1.SelectedItem = info[1];
            this.team2.SelectedItem = info[2];
            this.team3.SelectedItem = info[3];
            this.team4.SelectedItem = info[4];
            this.team5.SelectedItem = info[5];
            this.team6.SelectedItem = info[6];
            this.team7.SelectedItem = info[7];
            this.team8.SelectedItem = info[8];
            this.team9.SelectedItem = info[9];
            this.team10.SelectedItem = info[10];
                score1.Text = info[11];
                score2.Text = info[12];
                score3.Text = info[13];
                score4.Text = info[14];
                score5.Text = info[15];
                score6.Text = info[16];
                score7.Text = info[17];
                score8.Text = info[18];
                score9.Text = info[19];
                score10.Text = info[20];
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
