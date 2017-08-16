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
        db sqldb = new db();
        public EventInfo()
        {
            InitializeComponent();
            try
            {
                teamname = sqldb.getdata("teamname", "teamdatabase");
                l3 = sqldb.getdata("title", "lowerthird");
                l3_long = sqldb.getdata("title", "lowerthird_long");
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
                foreach (string i in l3_long)
                {
                    l3_longcombo.Items.Add(i);
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

        private void l3_longsave_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = l3_longtitle.Text;
            val[0] = "title";
            val[1] = select;
            exist = sqldb.checkexist("lowerthird_long", val);
            string[] type = new string[2];
            string[] info = new string[2];
            type[0] = "title";
            type[1] = "description";
            info[0] = l3_longtitle.Text;
            info[1] = l3_longdesc.Text;
            if (exist == 0)
            {
                sqldb.insertdata("lowerthird_long", info, type);
                l3_long = sqldb.getdata("title", "lowerthird_long");
                l3_longcombo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("lowerthird_long", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void l3_longdelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = l3_longtitle.Text;
                val[0] = "title";
                val[1] = select;
                sqldb.deletedata("lowerthird_long", val);
                l3_longtitle.Text = "";
                l3_longdesc.Text = "";
            }
        }

        private void l3_longcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.l3_combo.GetItemText(this.l3_combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "lowerthird_long", "title", select);
            l3_longtitle.Text = info[0];
            l3_longdesc.Text = info[1];
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
    }
}
