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
    public partial class TeamDatabase : UserControl
    {
        private string filepath = @"\\BENZ\LiveGFX";
        private string photopath = "";
        private List<string> teamname = new List<string>();
        db sqldb = new db();
        public TeamDatabase()
        {
            InitializeComponent();
            try
            {
                teamname = sqldb.getdata("teamname", "teamdatabase");
            }
            catch (Exception d)
            {
            }
            foreach (string i in teamname)
            {
                combo.Items.Add(i);
            }
            playercombo1.SelectedIndex = 0;
            playercombo2.SelectedIndex = 1;
            playercombo3.SelectedIndex = 2;
            playercombo4.SelectedIndex = 3;
            playercombo5.SelectedIndex = 4;
            playercombo6.SelectedIndex = 5;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            int exist = 0;
            string[] val = new string[2];
            string select = teamname_box.Text;
            val[0] = "teamname";
            val[1] = select;
            exist = sqldb.checkexist("teamdatabase", val);
            string[] type = new string[18];
            string[] info = new string[18];
            type[0] = "teamname";
            type[1] = "shortname";
            type[2] = "logo";
            type[3] = "teampic";
            type[4] = "tm_pic";
            type[5] = "bracket_pic";
            type[6] = "playername1";
            type[7] = "playername2";
            type[8] = "playername3";
            type[9] = "playername4";
            type[10] = "playername5";
            type[11] = "playername6";
            type[12] = "playerpic1";
            type[13] = "playerpic2";
            type[14] = "playerpic3";
            type[15] = "playerpic4";
            type[16] = "playerpic5";
            type[17] = "playerpic6";
            info[0] = teamname_box.Text;
            info[1] = shortname.Text;
            info[2] = logo.ImageLocation;
            info[3] = teampic.ImageLocation;
            info[4] = tm_pic.ImageLocation;
            info[5] = bracket_pic.ImageLocation;
            switch(this.playercombo1.GetItemText(this.playercombo1.SelectedItem))
            {
                case "player1":
                    info[6] = playername1.Text;
                    info[12] = playerpic1.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername1.Text;
                    info[13] = playerpic1.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername1.Text;
                    info[14] = playerpic1.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername1.Text;
                    info[15] = playerpic1.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername1.Text;
                    info[16] = playerpic1.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername1.Text;
                    info[17] = playerpic1.ImageLocation;
                    break;
                default:
                    break;
            }
            switch (this.playercombo2.GetItemText(this.playercombo2.SelectedItem))
            {
                case "player1":
                    info[6] = playername2.Text;
                    info[12] = playerpic2.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername2.Text;
                    info[13] = playerpic2.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername2.Text;
                    info[14] = playerpic2.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername2.Text;
                    info[15] = playerpic2.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername2.Text;
                    info[16] = playerpic2.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername2.Text;
                    info[17] = playerpic2.ImageLocation;
                    break;
                default:
                    break;
            }
            switch (this.playercombo3.GetItemText(this.playercombo3.SelectedItem))
            {
                case "player1":
                    info[6] = playername3.Text;
                    info[12] = playerpic3.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername3.Text;
                    info[13] = playerpic3.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername3.Text;
                    info[14] = playerpic3.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername3.Text;
                    info[15] = playerpic3.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername3.Text;
                    info[16] = playerpic3.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername3.Text;
                    info[17] = playerpic3.ImageLocation;
                    break;
                default:
                    break;
            }
            switch (this.playercombo4.GetItemText(this.playercombo4.SelectedItem))
            {
                case "player1":
                    info[6] = playername4.Text;
                    info[12] = playerpic4.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername4.Text;
                    info[13] = playerpic4.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername4.Text;
                    info[14] = playerpic4.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername4.Text;
                    info[15] = playerpic4.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername4.Text;
                    info[16] = playerpic4.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername4.Text;
                    info[17] = playerpic4.ImageLocation;
                    break;
                default:
                    break;
            }
            switch (this.playercombo5.GetItemText(this.playercombo5.SelectedItem))
            {
                case "player1":
                    info[6] = playername5.Text;
                    info[12] = playerpic5.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername5.Text;
                    info[13] = playerpic5.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername5.Text;
                    info[14] = playerpic5.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername5.Text;
                    info[15] = playerpic5.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername5.Text;
                    info[16] = playerpic5.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername5.Text;
                    info[17] = playerpic5.ImageLocation;
                    break;
                default:
                    break;
            }
            switch (this.playercombo6.GetItemText(this.playercombo6.SelectedItem))
            {
                case "player1":
                    info[6] = playername6.Text;
                    info[12] = playerpic6.ImageLocation;
                    break;
                case "player2":
                    info[7] = playername6.Text;
                    info[13] = playerpic6.ImageLocation;
                    break;
                case "player3":
                    info[8] = playername6.Text;
                    info[14] = playerpic6.ImageLocation;
                    break;
                case "player4":
                    info[9] = playername6.Text;
                    info[15] = playerpic6.ImageLocation;
                    break;
                case "player5":
                    info[10] = playername6.Text;
                    info[16] = playerpic6.ImageLocation;
                    break;
                case "substitute1":
                    info[11] = playername6.Text;
                    info[17] = playerpic6.ImageLocation;
                    break;
                default:
                    break;
            }


            if (exist == 0)
            {
                sqldb.insertdata("teamdatabase", info, type);
                teamname = sqldb.getdata("teamname", "teamdatabase");
                combo.Items.Add(info[0]);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                sqldb.updatedata("teamdatabase", info, type, val);
                MessageBox.Show("SAVING COMPLETE", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete ?", "INVLIVE-GFX", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string[] val = new string[2];
                string select = teamname_box.Text;
                val[0] = "teamname";
                val[1] = select;
                sqldb.deletedata("teamdatabase", val);
                teamname_box.Text = "";
                shortname.Text = "";
                logo.ImageLocation = "";
                teampic.ImageLocation = "";
                tm_pic.ImageLocation = "";
                bracket_pic.ImageLocation = "";
                playername1.Text = "";
                playerpic1.ImageLocation = "";
                playername2.Text = "";
                playerpic2.ImageLocation = "";
                playername3.Text = "";
                playerpic3.ImageLocation = "";
                playername4.Text = "";
                playerpic4.ImageLocation = "";
                playername5.Text = "";
                playerpic5.ImageLocation = "";
                playername6.Text = "";
                playerpic6.ImageLocation = "";
            }
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.combo.GetItemText(this.combo.SelectedItem);
            List<string> info = new List<string>();
            info = sqldb.getdata("*", "teamdatabase", "teamname", select);
            teamname_box.Text = info[0];
            shortname.Text = info[1];
            logo.ImageLocation = info[2];
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
            teampic.ImageLocation = info[3];
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
            tm_pic.ImageLocation = info[4];
            tm_pic.SizeMode = PictureBoxSizeMode.StretchImage;
            bracket_pic.ImageLocation = info[5];
            bracket_pic.SizeMode = PictureBoxSizeMode.StretchImage;
            playername1.Text = info[6];
            playerpic1.ImageLocation = info[7];
            playerpic1.SizeMode = PictureBoxSizeMode.StretchImage;
            playername2.Text = info[8];
            playerpic2.ImageLocation = info[9];
            playerpic2.SizeMode = PictureBoxSizeMode.StretchImage;
            playername3.Text = info[10];
            playerpic3.ImageLocation = info[11];
            playerpic3.SizeMode = PictureBoxSizeMode.StretchImage;
            playername4.Text = info[12];
            playerpic4.ImageLocation = info[13];
            playerpic4.SizeMode = PictureBoxSizeMode.StretchImage;
            playername5.Text = info[14];
            playerpic5.ImageLocation = info[15];
            playerpic5.SizeMode = PictureBoxSizeMode.StretchImage;
            playername6.Text = info[16];
            playerpic6.ImageLocation = info[17];
            playerpic6.SizeMode = PictureBoxSizeMode.StretchImage;
            MessageBox.Show("LOADING COMPLETE", "INVLIVE-GFX",
                   MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void logo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                logo.ImageLocation = photopath;
                logo.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void teampic_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                teampic.ImageLocation = photopath;
                teampic.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void tm_pic_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                tm_pic.ImageLocation = photopath;
                tm_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void bracket_pic_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                bracket_pic.ImageLocation = photopath;
                bracket_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic1.ImageLocation = photopath;
                playerpic1.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic2.ImageLocation = photopath;
                playerpic2.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic3.ImageLocation = photopath;
                playerpic3.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic4.ImageLocation = photopath;
                playerpic4.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic5.ImageLocation = photopath;
                playerpic5.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }

        private void playerpic6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Portrait File";
            openFileDialog1.InitialDirectory = filepath;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                photopath = openFileDialog1.FileName.ToString();
                playerpic6.ImageLocation = photopath;
                playerpic6.SizeMode = PictureBoxSizeMode.StretchImage;
                filepath = Path.GetDirectoryName(photopath);
            }
        }
    }
}
