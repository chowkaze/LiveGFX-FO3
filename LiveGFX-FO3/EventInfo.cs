using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LiveGFX_FO3
{
    public partial class EventInfo : UserControl
    {
        private List<string> teamname = new List<string>();
        private List<string> l3 = new List<string>();
        private List<string> l3_long = new List<string>();
        private List<string> tm = new List<string>();
        private List<string> caster = new List<string>();
        private List<string> interview = new List<string>();
        private List<string> analyzer = new List<string>();
        private List<string> analyzed = new List<string>();
        db sqldb = new db();
        public EventInfo()
        {
            InitializeComponent();
            try
            {
                teamname = sqldb.getdata("teamname", "teamdatabase");
                l3 = sqldb.getdata("title", "lowerthird");
                tm = sqldb.getdata("sname", "today_matches");
                caster = sqldb.getdata("name", "caster_lowerthird");
                interview = sqldb.getdata("name", "interviewer");
                analyzer = sqldb.getdata("name", "analyzer");
            }
            catch(Exception d)
            {

            }
            finally
            {

                foreach (string i in l3)
                {
                    l3_combo.Items.Add(i);
                }
                foreach (string i in tm)
                {
                    tm_combo.Items.Add(i);
                }
                foreach (string i in caster)
                {
                    caster_combo.Items.Add(i);
                }
                foreach (string i in interview)
                {
                    interview_combo.Items.Add(i);
                }
                foreach (string i in analyzer)
                {
                    analyzer_combo.Items.Add(i);
                }
                foreach (string i in teamname)
                {
                    tm_teaml1.Items.Add(i);
                    tm_teaml2.Items.Add(i);
                    tm_teamr1.Items.Add(i);
                    tm_teamr2.Items.Add(i);
                }
            }
        }

        private void l3_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = l3_title.Text;
            val[0] = "title";
            val[1] = select;
            exist = sqldb.checkexist("lowerthird", val);
            string[] type = new string[2];
            string[] info = new string[2];
            type[0] = "title";
            type[1] = "description";
            info[0] = l3_title.Text;
            info[1] = l3_desc.Text;
            if (exist == 0)
            {
                sqldb.insertdata("lowerthird", info, type);
                l3 = sqldb.getdata("title", "lowerthird");
                l3_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("lowerthird", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void l3_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = l3_title.Text;
                val[0] = "title";
                val[1] = select;
                sqldb.deletedata("lowerthird", val);
                l3_title.Text = "";
                l3_desc.Text = "";
            }
        }

        private void l3_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.l3_combo.GetItemText(this.l3_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "lowerthird", "title", select);
            l3_title.Text = info[0];
            l3_desc.Text = info[1];
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void caster_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = caster_name.Text;
            val[0] = "name";
            val[1] = select;
            exist = sqldb.checkexist("caster_lowerthird", val);
            string[] type = new string[2];
            string[] info = new string[2];
            type[0] = "name";
            type[1] = "description";
            info[0] = caster_name.Text;
            info[1] = caster_desc.Text;
            if (exist == 0)
            {
                sqldb.insertdata("caster_lowerthird", info, type);
                caster = sqldb.getdata("name", "caster_lowerthird");
                caster_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("caster_lowerthird", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void caster_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = caster_name.Text;
                val[0] = "name";
                val[1] = select;
                sqldb.deletedata("caster_lowerthird", val);
                caster_name.Text = "";
                caster_desc.Text = "";
            }
        }

        private void caster_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.caster_combo.GetItemText(this.caster_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "caster_lowerthird", "name", select);
            caster_name.Text = info[0];
            caster_desc.Text = info[1];
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void analyzer_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = analyzer_name.Text;
            val[0] = "name";
            val[1] = select;
            exist = sqldb.checkexist("analyzer_lowerthird", val);
            string[] type = new string[2];
            string[] info = new string[2];
            type[0] = "name";
            type[1] = "description";
            info[0] = analyzer_name.Text;
            info[1] = analyzer_desc.Text;
            if (exist == 0)
            {
                sqldb.insertdata("analyzer_lowerthird", info, type);
                analyzer = sqldb.getdata("name", "analyzer_lowerthird");
                analyzer_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("analyzer_lowerthird", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void analyzer_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = analyzer_name.Text;
                val[0] = "name";
                val[1] = select;
                sqldb.deletedata("analyzer_lowerthird", val);
                analyzer_name.Text = "";
                analyzer_desc.Text = "";
            }
        }

        private void analyzer_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.analyzer_combo.GetItemText(this.analyzer_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "analyzer_lowerthird", "name", select);
            analyzer_name.Text = info[0];
            analyzer_desc.Text = info[1];
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }


        private void interview_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = analyzer_name.Text;
            val[0] = "name";
            val[1] = select;
            exist = sqldb.checkexist("interview_lowerthird", val);
            string[] type = new string[2];
            string[] info = new string[2];
            type[0] = "name";
            type[1] = "description";
            info[0] = interview_title.Text;
            info[1] = interview_desc.Text;
            if (exist == 0)
            {
                sqldb.insertdata("interview_lowerthird", info, type);
                interview = sqldb.getdata("name", "interview_lowerthird");
                interview_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("interview_lowerthird", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void interview_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = interview_title.Text;
                val[0] = "name";
                val[1] = select;
                sqldb.deletedata("interview_lowerthird", val);
                interview_title.Text = "";
                interview_desc.Text = "";
            }
        }
        private void interview_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.interview_combo.GetItemText(this.interview_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "interview_lowerthird", "name", select);
            interview_title.Text = info[0];
            interview_desc.Text = info[1];
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void tm_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = tm_sn.Text;
            val[0] = "savename";
            val[1] = select;
            exist = sqldb.checkexist("today_matches", val);
            string[] type = new string[12];
            string[] info = new string[12];
            type[0] = "savename";
            type[1] = "title";
            type[2] = "teaml1";
            type[3] = "teamr1";
            type[4] = "teaml2";
            type[5] = "teamr2";
            type[6] = "game1";
            type[7] = "game2";
            type[8] = "winl1";
            type[9] = "winr1";
            type[10] = "winl2";
            type[11] = "winr2";
            info[0] = tm_sn.Text;
            info[1] = tm_title.Text;
            info[2] = this.tm_teaml1.GetItemText(this.tm_teaml1.SelectedItem);
            info[3] = this.tm_teamr1.GetItemText(this.tm_teamr1.SelectedItem);
            info[4] = this.tm_teaml2.GetItemText(this.tm_teaml2.SelectedItem);
            info[5] = this.tm_teamr2.GetItemText(this.tm_teamr2.SelectedItem);
            info[6] = tm_time1.Text;
            info[7] = tm_time2.Text;
            if(winl1.Checked)
            {
                info[8] = "win";
            }
            else
            {
                info[8] = "";
            }
            if (winl2.Checked)
            {
                info[9] = "win";
            }
            else
            {
                info[9] = "";
            }
            if (winr1.Checked)
            {
                info[10] = "win";
            }
            else
            {
                info[10] = "";
            }
            if (winr2.Checked)
            {
                info[11] = "win";
            }
            else
            {
                info[11] = "";
            }

            if (exist == 0)
            {
                sqldb.insertdata("today_matches", info, type);
                tm = sqldb.getdata("savename", "today_matches");
                tm_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("today_matches", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void tm_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = tm_sn.Text;
                val[0] = "savename";
                val[1] = select;
                sqldb.deletedata("today_matches", val);
                tm_sn.Text = "";
                tm_title.Text = "";
                this.tm_teaml1.SelectedItem = "";
                this.tm_teamr1.SelectedItem = "";
                this.tm_teaml2.SelectedItem = "";
                this.tm_teamr2.SelectedItem = "";
                tm_time1.Text = "";
                tm_time2.Text = "";
                winl1.Checked = false;
                winl2.Checked = false;
                winr1.Checked = false;
                winr2.Checked = false;

            }
        }

        private void tm_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.tm_combo.GetItemText(this.tm_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "today_matches", "savename", select);
            tm_sn.Text = info[0];
            tm_title.Text = info[1];
            this.tm_teaml1.SelectedItem = info[2];
            this.tm_teamr1.SelectedItem = info[3];
            this.tm_teaml2.SelectedItem = info[4];
            this.tm_teamr2.SelectedItem = info[5];
            tm_time1.Text = info[6];
            tm_time2.Text = info[7];
            if(info[8] == "win")
            {
                winl1.Checked = true;
            }
            else
            {
                winl1.Checked = false;
            }
            if (info[9] == "win")
            {
                winl2.Checked = true;
            }
            else
            {
                winl2.Checked = false;
            }
            if (info[10] == "win")
            {
                winr1.Checked = true;
            }
            else
            {
                winr1.Checked = false;
            }
            if (info[11] == "win")
            {
                winr2.Checked = true;
            }
            else
            {
                winr2.Checked = false;
            }
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void analyzed_save_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = analyzed_title.Text;
            val[0] = "title";
            val[1] = select;
            exist = sqldb.checkexist("analyzed_tab", val);
            string[] type = new string[25];
            string[] info = new string[25];
            type[0] = "title";
            type[1] = "teaml";
            type[2] = "teamr";
            type[3] = "pickl1";
            type[4] = "pickl2";
            type[5] = "pickl3";
            type[6] = "pickl4";
            type[7] = "pickl5";
            type[8] = "banl1";
            type[9] = "banl2";
            type[10] = "banl3";
            type[11] = "banl4";
            type[12] = "banl5";
            type[13] = "pickr1";
            type[14] = "pickr2";
            type[15] = "pickr3";
            type[16] = "pickr4";
            type[17] = "pickr5";
            type[18] = "banl1";
            type[19] = "banl2";
            type[20] = "banl3";
            type[21] = "banl4";
            type[22] = "banl5";
            type[23] = "winl";
            type[24] = "winr";
            info[0] = analyzed_title.Text;
            info[1] = this.analyzed_teaml.GetItemText(this.analyzed_teaml.SelectedItem);
            info[2] = this.analyzed_teamr.GetItemText(this.analyzed_teamr.SelectedItem);
            info[3] = this.analyzed_pickl1.GetItemText(this.analyzed_pickl1.SelectedItem);
            info[4] = this.analyzed_pickl2.GetItemText(this.analyzed_pickl2.SelectedItem);
            info[5] = this.analyzed_pickl3.GetItemText(this.analyzed_pickl3.SelectedItem);
            info[6] = this.analyzed_pickl4.GetItemText(this.analyzed_pickl4.SelectedItem);
            info[7] = this.analyzed_pickl5.GetItemText(this.analyzed_pickl5.SelectedItem);
            info[8] = this.analyzed_banl1.GetItemText(this.analyzed_banl1.SelectedItem);
            info[9] = this.analyzed_banl2.GetItemText(this.analyzed_banl2.SelectedItem);
            info[10] = this.analyzed_banl3.GetItemText(this.analyzed_banl3.SelectedItem);
            info[11] = this.analyzed_banl4.GetItemText(this.analyzed_banl4.SelectedItem);
            info[12] = this.analyzed_banl5.GetItemText(this.analyzed_banl5.SelectedItem);
            info[13] = this.analyzed_pickr1.GetItemText(this.analyzed_pickr1.SelectedItem);
            info[14] = this.analyzed_pickr2.GetItemText(this.analyzed_pickr2.SelectedItem);
            info[15] = this.analyzed_pickr3.GetItemText(this.analyzed_pickr3.SelectedItem);
            info[16] = this.analyzed_pickr4.GetItemText(this.analyzed_pickr4.SelectedItem);
            info[17] = this.analyzed_pickr5.GetItemText(this.analyzed_pickr5.SelectedItem);
            info[18] = this.analyzed_banr1.GetItemText(this.analyzed_banr1.SelectedItem);
            info[19] = this.analyzed_banr2.GetItemText(this.analyzed_banr2.SelectedItem);
            info[20] = this.analyzed_banr3.GetItemText(this.analyzed_banr3.SelectedItem);
            info[21] = this.analyzed_banr4.GetItemText(this.analyzed_banr4.SelectedItem);
            info[22] = this.analyzed_banr5.GetItemText(this.analyzed_banr5.SelectedItem);
            if (analyzed_winl.Checked)
            {
                info[23] = "win";
            }
            else
            {
                info[23] = "";
            }
            if (analyzed_winr.Checked)
            {
                info[24] = "win";
            }
            else
            {
                info[24] = "";
            }
            if (exist == 0)
            {
                sqldb.insertdata("analyzed_tab", info, type);
                analyzed = sqldb.getdata("title", "analyzed_tab");
                analyzed_combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("analyzed_tab", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void analyzed_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = analyzed_title.Text;
                val[0] = "title";
                val[1] = select;
                sqldb.deletedata("analyzed_tab", val);
                analyzed_title.Text = "";
                this.analyzed_teaml.SelectedItem = "";
                this.analyzed_teamr.SelectedItem = "";
                this.analyzed_pickl1.SelectedItem = "";
                this.analyzed_pickl2.SelectedItem = "";
                this.analyzed_pickl3.SelectedItem = "";
                this.analyzed_pickl4.SelectedItem = "";
                this.analyzed_pickl5.SelectedItem = "";
                this.analyzed_banl1.SelectedItem = "";
                this.analyzed_banl2.SelectedItem = "";
                this.analyzed_banl3.SelectedItem = "";
                this.analyzed_banl4.SelectedItem = "";
                this.analyzed_banl5.SelectedItem = "";
                this.analyzed_pickr1.SelectedItem = "";
                this.analyzed_pickr2.SelectedItem = "";
                this.analyzed_pickr3.SelectedItem = "";
                this.analyzed_pickr4.SelectedItem = "";
                this.analyzed_pickr5.SelectedItem = "";
                this.analyzed_banr1.SelectedItem = "";
                this.analyzed_banr2.SelectedItem = "";
                this.analyzed_banr3.SelectedItem = "";
                this.analyzed_banr4.SelectedItem = "";
                this.analyzed_banr5.SelectedItem = "";
                analyzed_winl.Checked = false;
                analyzed_winr.Checked = false;
            }
        }

        private void analyzed_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.analyzed_combo.GetItemText(this.analyzed_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "analyzed_tab", "title", select);
            analyzed_title.Text = info[0];
            this.analyzed_teaml.SelectedItem = info[1];
            this.analyzed_teamr.SelectedItem = info[2];
            this.analyzed_pickl1.SelectedItem = info[3];
            this.analyzed_pickl2.SelectedItem = info[4];
            this.analyzed_pickl3.SelectedItem = info[5];
            this.analyzed_pickl4.SelectedItem = info[6];
            this.analyzed_pickl5.SelectedItem = info[7];
            this.analyzed_banl1.SelectedItem = info[8];
            this.analyzed_banl2.SelectedItem = info[9];
            this.analyzed_banl3.SelectedItem = info[10];
            this.analyzed_banl4.SelectedItem = info[11];
            this.analyzed_banl5.SelectedItem = info[12];
            this.analyzed_pickr1.SelectedItem = info[13];
            this.analyzed_pickr2.SelectedItem = info[14];
            this.analyzed_pickr3.SelectedItem = info[15];
            this.analyzed_pickr4.SelectedItem = info[16];
            this.analyzed_pickr5.SelectedItem = info[17];
            this.analyzed_banr1.SelectedItem = info[18];
            this.analyzed_banr2.SelectedItem = info[19];
            this.analyzed_banr3.SelectedItem = info[20];
            this.analyzed_banr4.SelectedItem = info[21];
            this.analyzed_banr5.SelectedItem = info[22];
            if(info[23] == "win")
            {
                analyzed_winl.Checked = true;
            }
            else
            {
                analyzed_winl.Checked = false;
            }
            if (info[24] == "win")
            {
                analyzed_winr.Checked = true;
            }
            else
            {
                analyzed_winr.Checked = false;
            }

            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
