using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svt.Caspar;
using Svt.Network;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Timers;

namespace LiveGFX_FO3
{
    public partial class InGame : UserControl
    {
        db sqldb = new db();
        List<string> teamname = new List<string>();
        List<string> lm_name = new List<string>();
        private delegate void UpdateGUI(object parameter);
        CasparDevice caspar_ = new CasparDevice();
        CasparCGDataCollection cgData = new CasparCGDataCollection();
        List<string> info = new List<string>();
        List<string> info1 = new List<string>();
        int countdowntimelayer = 0;
        int countdowntimechannal = 0;
        bool countdowntick = false;
        System.Windows.Forms.Timer times = new System.Windows.Forms.Timer();
        //l3_playing,countdown,playerl_stat,playerr_stat,l3_h2h,scorebug,highlight_bug,latest_match
        bool[] button_on = new bool[8] { false, false, false, false, false, false, false, false };
        bool[] test_on = new bool[8] { false, false, false, false, false, false, false, false };
        bool test_channel = false;
        public InGame()
        {
            InitializeComponent();
            caspar_.Connected += new EventHandler<NetworkEventArgs>(caspar__Connected);
            caspar_.FailedConnect += new EventHandler<NetworkEventArgs>(caspar__FailedConnected);
            caspar_.Disconnected += new EventHandler<NetworkEventArgs>(caspar__Disconnected);
            try
            {
                teamname = sqldb.getdata("teamname", "teamdatabase");
            }
            catch (Exception d)
            {
            }
            foreach (string i in teamname)
            {
                teaml_combo.Items.Add(i);
                teamr_combo.Items.Add(i);
            }
        }
        #region caspar connection

        //button handlers
        private void Connect_Button_Click_1(object sender, EventArgs e)
        {

            if (!caspar_.IsConnected)
            {
                caspar_.Settings.Hostname = this.bunifuMaterialTextbox11.Text; // Properties.Settings.Default.Hostname;
                Properties.Settings.Default.Server_ad = this.bunifuMaterialTextbox11.Text;
                caspar_.Settings.Port = 5250;
                caspar_.Connect();



            }
            else
            {
                caspar_.Disconnect();
            }
        }

        //caspar event - connected
        void caspar__Connected(object sender, NetworkEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(new UpdateGUI(OnCasparConnected), e);
            else
                OnCasparConnected(e);
        }
        void OnCasparConnected(object param)
        {
            bunifuFlatButton19.Enabled = true;
            updateConnectButtonText();

            caspar_.RefreshMediafiles();
            caspar_.RefreshDatalist();

            NetworkEventArgs e = (NetworkEventArgs)param;

        }



        //caspar event - failed connect
        void caspar__FailedConnected(object sender, NetworkEventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(new UpdateGUI(OnCasparFailedConnect), e);
            else
                OnCasparFailedConnect(e);
        }
        void OnCasparFailedConnect(object param)
        {
            bunifuFlatButton19.Enabled = true;
            updateConnectButtonText();

            NetworkEventArgs e = (NetworkEventArgs)param;
        }

        //caspar event - disconnected
        void caspar__Disconnected(object sender, NetworkEventArgs e)
        {

            if (InvokeRequired)
                BeginInvoke(new UpdateGUI(OnCasparDisconnected), e);
            else
                OnCasparDisconnected(e);
        }
        void OnCasparDisconnected(object param)
        {
            bunifuFlatButton19.Enabled = true;
            updateConnectButtonText();

            NetworkEventArgs e = (NetworkEventArgs)param;

        }

        // update text on button
        private void updateConnectButtonText()
        {
            System.Diagnostics.Debug.WriteLine("" + caspar_.IsConnected + "" + caspar_.Channels.Count);
            if (!caspar_.IsConnected)
            {

                bunifuFlatButton19.Text = "CONNECT";// to " + Properties.Settings.Default.Hostname;
                bunifuFlatButton19.Normalcolor = Color.FromArgb(183, 183, 183);
                bunifuFlatButton19.Textcolor = Color.Black;

            }
            else
            {
                bunifuFlatButton19.Text = "CONNECTED"; // from " + Properties.Settings.Default.Hostname;
                bunifuFlatButton19.Normalcolor = Color.SeaGreen;
                bunifuFlatButton19.Textcolor = Color.White;
            }
        }

        #endregion
        public void runcg(String[] Types, String[] InputSource, int layer, int channel, String TempName)
        {
            try
            {
                // Clear old data
                cgData.Clear();

                // build data
                for (int i = 0; i < Types.Length; i++)
                {
                    cgData.SetData(Types[i], InputSource[i]);
                    System.Diagnostics.Debug.WriteLine(Types[i]);
                    System.Diagnostics.Debug.WriteLine(InputSource[i]);
                }

            }
            catch
            {

            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("" + caspar_.IsConnected + "" + caspar_.Channels.Count);
                if (caspar_.IsConnected && caspar_.Channels.Count > 0)
                {
                    caspar_.Channels[channel].CG.Add(layer, TempName, true, cgData);
                    System.Diagnostics.Debug.WriteLine("Add");
                    System.Diagnostics.Debug.WriteLine(2);
                    System.Diagnostics.Debug.WriteLine("Two - Tier - LowerThirds");
                    System.Diagnostics.Debug.WriteLine(cgData.ToXml());
                }
            }
        }
        public void stopcg(int layer, int channel)
        {
            try
            {

            }
            catch
            {

            }
            finally
            {
                if (caspar_.IsConnected && caspar_.Channels.Count > 0)
                {
                    caspar_.Channels[channel].CG.Stop(layer);
                    System.Diagnostics.Debug.WriteLine("Stop");
                    System.Diagnostics.Debug.WriteLine(2);
                }
            }
        }
        public void nextcg(int layer, int channel)
        {
            try
            {

            }
            catch
            {

            }
            finally
            {
                if (caspar_.IsConnected && caspar_.Channels.Count > 0)
                {
                    caspar_.Channels[channel].CG.Next(layer);
                    System.Diagnostics.Debug.WriteLine("Next");
                    System.Diagnostics.Debug.WriteLine(2);
                }
            }
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

        private void teaml_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select = this.teaml_combo.GetItemText(this.teaml_combo.SelectedItem);
            info = sqldb.getdata("*", "teamdatabase", "teamname", select);
            bunifuCustomLabel1.Text = info[0];
            bunifuCustomLabel69.Text = info[0];
            bunifuCustomLabel23.Text = info[0];
            bunifuCustomLabel16.Text = info[0] + " DRAGON";
            bunifuCustomLabel37.Text = info[0] + " DRAGON BUFF";
            bunifuCustomLabel14.Text = info[0] + " WIN";
            pictureBox1.ImageLocation = info[2];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void teamr_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select1 = this.teamr_combo.GetItemText(this.teamr_combo.SelectedItem);
            info1 = sqldb.getdata("*", "teamdatabase", "teamname", select1);
            bunifuCustomLabel9.Text = info1[0];
            bunifuCustomLabel50.Text = info1[0];
            bunifuCustomLabel68.Text = info1[0];
            bunifuCustomLabel35.Text = info1[0] + " DRAGON";
            bunifuCustomLabel38.Text = info1[0] + " DRAGON BUFF";
            bunifuCustomLabel36.Text = info1[0] + " WIN";
            pictureBox2.ImageLocation = info1[2];
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            List<string> wait = new List<string>();
            wait = info;
            info = info1;
            info1 = wait;
            bunifuCustomLabel9.Text = info1[0];
            bunifuCustomLabel50.Text = info1[0];
            bunifuCustomLabel68.Text = info1[0];
            bunifuCustomLabel35.Text = info1[0] + " DRAGON";
            bunifuCustomLabel38.Text = info1[0] + " DRAGON BUFF";
            bunifuCustomLabel36.Text = info1[0] + " WIN";
            pictureBox2.ImageLocation = info1[2];
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            bunifuCustomLabel1.Text = info[0];
            bunifuCustomLabel69.Text = info[0];
            bunifuCustomLabel23.Text = info[0];
            bunifuCustomLabel16.Text = info[0] + " DRAGON";
            bunifuCustomLabel37.Text = info[0] + " DRAGON BUFF";
            bunifuCustomLabel14.Text = info[0] + " WIN";
            pictureBox1.ImageLocation = info[2];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void bunifuFlatButton37_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[0];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton37.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton37.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "teaml";
                        ty[1] = "teamr";
                        ty[2] = "scorel";
                        ty[3] = "scorer";
                        ty[4] = "keystonel1";
                        ty[5] = "keystonel2";
                        ty[6] = "keystonel3";
                        ty[7] = "keystonel4";
                        ty[8] = "keystonel5";
                        ty[9] = "keystoner1";
                        ty[10] = "keystoner2";
                        ty[11] = "keystoner3";
                        ty[12] = "keystoner4";
                        ty[13] = "keystoner5";
                        inputsource[0] = info[1];
                        inputsource[1] = info1[1];
                        inputsource[2] = this.metroComboBox24.GetItemText(this.metroComboBox24.SelectedItem);
                        inputsource[3] = this.metroComboBox25.GetItemText(this.metroComboBox25.SelectedItem);
                        inputsource[4] = @"\\"+ this.metroComboBox55.GetItemText(this.metroComboBox55.SelectedItem)+".png";
                        inputsource[5] = @"\\" + this.metroComboBox54.GetItemText(this.metroComboBox54.SelectedItem) + ".png";
                        inputsource[6] = @"\\" + this.metroComboBox53.GetItemText(this.metroComboBox53.SelectedItem) + ".png";
                        inputsource[7] = @"\\" + this.metroComboBox52.GetItemText(this.metroComboBox52.SelectedItem) + ".png";
                        inputsource[8] = @"\\" + this.metroComboBox51.GetItemText(this.metroComboBox51.SelectedItem) + ".png";
                        inputsource[9] = @"\\" + this.metroComboBox50.GetItemText(this.metroComboBox50.SelectedItem) + ".png";
                        inputsource[10] = @"\\" + this.metroComboBox49.GetItemText(this.metroComboBox49.SelectedItem) + ".png";
                        inputsource[11] = @"\\" + this.metroComboBox48.GetItemText(this.metroComboBox48.SelectedItem) + ".png";
                        inputsource[12] = @"\\" + this.metroComboBox47.GetItemText(this.metroComboBox47.SelectedItem) + ".png";
                        inputsource[13] = @"\\" + this.metroComboBox46.GetItemText(this.metroComboBox46.SelectedItem) + ".png";


                        if (test_channel)
                        {
                            bunifuFlatButton37.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton37.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton37.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton37.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/l3");
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 14;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton12.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton12.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch(this.metroComboBox22.GetItemText(this.metroComboBox22.SelectedItem))
                        {
                            case"Cloud":
                                temp = "";
                                break;
                            case "Infernal":
                                temp = "";
                                break;
                            case "Ocean":
                                temp = "";
                                break;
                            case "Mountain":
                                temp = "";
                                break;
                            case "Elder":
                                temp = "";
                                break;
                            default:
                                break;
                        }


                        if (test_channel)
                        {
                            bunifuFlatButton12.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton12.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton12.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton12.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 14;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton13.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton13.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch (this.metroComboBox23.GetItemText(this.metroComboBox23.SelectedItem))
                        {
                            case "Cloud":
                                temp = "";
                                break;
                            case "Infernal":
                                temp = "";
                                break;
                            case "Ocean":
                                temp = "";
                                break;
                            case "Mountain":
                                temp = "";
                                break;
                            case "Elder":
                                temp = "";
                                break;
                            default:
                                break;
                        }


                        if (test_channel)
                        {
                            bunifuFlatButton13.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton13.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton13.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton13.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 16;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton6.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton6.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "dragonl1";
                        ty[1] = "dragonl2";
                        ty[2] = "dragonl3";
                        ty[3] = "dragonl4";
                        ty[4] = "dragonl5";
                        ty[5] = "dragonl6";
                        ty[6] = "dragonl7";
                        ty[7] = "dragonl8";
                        ty[8] = "dragonr1";
                        ty[9] = "dragonr2";
                        ty[10] = "dragonr3";
                        ty[11] = "dragonr4";
                        ty[12] = "dragonr5";
                        ty[13] = "dragonr6";
                        ty[14] = "dragonr7";
                        ty[15] = "dragonr8";
                        string select = this.metroComboBox5.GetItemText(this.metroComboBox5.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[0] = @"\\"+select+".png";
                        }
                        else
                        {
                            inputsource[0] = "";
                        }
                        select = this.metroComboBox6.GetItemText(this.metroComboBox6.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[1] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[1] = "";
                        }
                        select = this.metroComboBox7.GetItemText(this.metroComboBox7.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[2] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[2] = "";
                        }
                        select = this.metroComboBox8.GetItemText(this.metroComboBox8.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[3] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[3] = "";
                        }
                        select = this.metroComboBox9.GetItemText(this.metroComboBox9.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[4] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[4] = "";
                        }
                        select = this.metroComboBox10.GetItemText(this.metroComboBox10.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[5] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[5] = "";
                        }
                        select = this.metroComboBox11.GetItemText(this.metroComboBox11.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[6] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[6] = "";
                        }
                        select = this.metroComboBox12.GetItemText(this.metroComboBox12.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[7] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[7] = "";
                        }
                        select = this.metroComboBox19.GetItemText(this.metroComboBox19.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[8] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[8] = "";
                        }
                        select = this.metroComboBox18.GetItemText(this.metroComboBox18.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[9] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[9] = "";
                        }
                        select = this.metroComboBox17.GetItemText(this.metroComboBox17.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[10] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[10] = "";
                        }
                        select = this.metroComboBox16.GetItemText(this.metroComboBox16.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[11] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[11] = "";
                        }
                        select = this.metroComboBox15.GetItemText(this.metroComboBox15.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[12] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[12] = "";
                        }
                        select = this.metroComboBox14.GetItemText(this.metroComboBox14.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[13] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[13] = "";
                        }
                        select = this.metroComboBox13.GetItemText(this.metroComboBox13.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[14] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[14] = "";
                        }
                        select = this.metroComboBox1.GetItemText(this.metroComboBox1.SelectedItem);
                        if (select != " ")
                        {
                            inputsource[15] = @"\\" + select + ".png";
                        }
                        else
                        {
                            inputsource[15] = "";
                        }



                        if (test_channel)
                        {
                            bunifuFlatButton6.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton6.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton6.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton6.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton14.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton14.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "teaml";
                        ty[1] = "teamr";
                        ty[2] = "scorel";
                        ty[3] = "scorer";
                        inputsource[0] = info[2];
                        inputsource[1] = info1[2];
                        inputsource[2] = this.metroComboBox24.GetItemText(this.metroComboBox24.SelectedItem);
                        inputsource[3] = this.metroComboBox25.GetItemText(this.metroComboBox25.SelectedItem);
                        if (test_channel)
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton14.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton14.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton38_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton38.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton38.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                       
                        if (test_channel)
                        {
                            bunifuFlatButton38.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton38.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton38.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton38.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton8.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton8.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        
                        if (test_channel)
                        {
                            bunifuFlatButton8.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton8.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton8.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton8.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[40];
                string[] inputsource = new string[40];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton14.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton14.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "L_Logo";
                        ty[1] = "R_Logo";
                        ty[2] = "L_Name_1";
                        ty[3] = "L_Name_2";
                        ty[4] = "L_Name_3";
                        ty[5] = "L_Name_4";
                        ty[6] = "L_Name_5";
                        ty[7] = "L_Champ_1";
                        ty[8] = "L_Champ_2";
                        ty[9] = "L_Champ_3";
                        ty[10] = "L_Champ_4";
                        ty[11] = "L_Champ_5";
                        ty[12] = "R_Name_1";
                        ty[13] = "R_Name_2";
                        ty[14] = "R_Name_3";
                        ty[15] = "R_Name_4";
                        ty[16] = "R_Name_5";
                        ty[17] = "R_Champ_1";
                        ty[18] = "R_Champ_2";
                        ty[19] = "R_Champ_3";
                        ty[20] = "R_Champ_4";
                        ty[21] = "R_Champ_5";
                        ty[22] = "L_Ban1";
                        ty[23] = "L_Ban2";
                        ty[24] = "L_Ban3";
                        ty[25] = "L_Ban4";
                        ty[26] = "L_Ban5";
                        ty[27] = "R_Ban1";
                        ty[28] = "R_Ban2";
                        ty[29] = "R_Ban3";
                        ty[30] = "R_Ban4";
                        ty[31] = "R_Ban5";
                        inputsource[0] = info[2];
                        inputsource[1] = info1[2];
                        inputsource[2] = info[6];
                        inputsource[3] = info[7];
                        inputsource[4] = info[8];
                        inputsource[5] = info[9];
                        inputsource[6] = info[10];
                        inputsource[7] = this.comboBox10.GetItemText(this.comboBox10.SelectedItem);
                        inputsource[8] = this.comboBox9.GetItemText(this.comboBox9.SelectedItem);
                        inputsource[9] = this.comboBox8.GetItemText(this.comboBox8.SelectedItem);
                        inputsource[10] = this.comboBox7.GetItemText(this.comboBox7.SelectedItem);
                        inputsource[11] = this.comboBox6.GetItemText(this.comboBox6.SelectedItem);
                        inputsource[12] = info1[6];
                        inputsource[13] = info1[7];
                        inputsource[14] = info1[8];
                        inputsource[15] = info1[9];
                        inputsource[16] = info1[10];
                        inputsource[17] = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
                        inputsource[18] = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
                        inputsource[19] = this.comboBox3.GetItemText(this.comboBox3.SelectedItem);
                        inputsource[20] = this.comboBox4.GetItemText(this.comboBox4.SelectedItem);
                        inputsource[21] = this.comboBox5.GetItemText(this.comboBox5.SelectedItem);
                        inputsource[22] = this.comboBox15.GetItemText(this.comboBox15.SelectedItem);
                        inputsource[23] = this.comboBox14.GetItemText(this.comboBox14.SelectedItem);
                        inputsource[24] = this.comboBox13.GetItemText(this.comboBox13.SelectedItem);
                        inputsource[25] = this.comboBox12.GetItemText(this.comboBox12.SelectedItem);
                        inputsource[26] = this.comboBox11.GetItemText(this.comboBox11.SelectedItem);
                        inputsource[27] = this.comboBox20.GetItemText(this.comboBox20.SelectedItem);
                        inputsource[28] = this.comboBox19.GetItemText(this.comboBox19.SelectedItem);
                        inputsource[29] = this.comboBox18.GetItemText(this.comboBox18.SelectedItem);
                        inputsource[30] = this.comboBox17.GetItemText(this.comboBox17.SelectedItem);
                        inputsource[31] = this.comboBox16.GetItemText(this.comboBox16.SelectedItem);
                        if (test_channel)
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton14.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton14.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 20;
                int channal = 0;
                bool check_on = button_on[0];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton7.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton7.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[0] = false;
                    }
                    else
                    {
                        button_on[0] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch (this.metroComboBox4.GetItemText(this.metroComboBox4.SelectedItem))
                        {
                            case "Cloud":
                                temp = "";
                                break;
                            case "Infernal":
                                temp = "";
                                break;
                            case "Ocean":
                                temp = "";
                                break;
                            case "Mountain":
                                temp = "";
                                break;
                            case "Elder":
                                temp = "";
                                break;
                            default:
                                break;
                        }


                        if (test_channel)
                        {
                            bunifuFlatButton7.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton7.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton7.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton7.Textcolor = Color.White;
                            button_on[0] = true;
                        }
                        runcg(ty, inputsource, layer, channal, temp);
                    }
                    catch (Exception d)
                    {
                        MessageBox.Show("You forget to choose title", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            string[] date = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss:tt").Split(':');
            int year = Int32.Parse(date[0]);
            int hr = Int32.Parse(date[3]);
            int min = Int32.Parse(date[4]);
            int sec = Int32.Parse(date[5]);
            int mintimer = 1;
            int sectimer = 30;
            string[] ty = new string[20];
            string[] inputsource = new string[20];
            string[] ty1 = new string[20];
            string[] inputsource1 = new string[20];
            int layer = 19;
            int channal = 0;
            int layer1 = 18;
            int channal1 = 0;
            bool check_on = button_on[6];
            if (test_channel)
            {
                channal = 2;
                channal1 = 2;
                check_on = test_on[6];
            }
            if (check_on)
            {
                bunifuFlatButton9.Normalcolor = Color.FromArgb(183, 183, 183);
                bunifuFlatButton9.Textcolor = Color.Black;
                if (test_channel)
                {
                    test_on[6] = false;
                }
                else
                {
                    button_on[6] = false;
                }
                stopcg(layer, channal);
                stopcg(layer1, channal1);
                countdowntick = false;
                countdowntimechannal = 0;
                countdowntimelayer = 0;
                times.Stop();
            }
            else
            {
                if ((sec + sectimer) > 60)
                {
                    min += 1;
                    sec -= 60;
                    sec += sectimer;
                }
                else
                {
                    sec += sectimer;
                }
                if ((min + mintimer) > 60)
                {
                    hr += 1;
                    min -= 60;
                    min += mintimer;
                }
                else
                {
                    min += mintimer;
                }

                if (hr < 10)
                {
                    date[3] = "0" + hr;
                }
                else
                {
                    date[3] = "" + hr;
                }
                if (min < 10)
                {
                    date[4] = "0" + min;
                }
                else
                {
                    date[4] = "" + min;
                }
                if (sec < 10)
                {
                    date[5] = "0" + sec;
                }
                else
                {
                    date[5] = sec + "";
                }

                year -= 543;
                date[0] = "" + year;

                ty[0] = "Year";
                ty[1] = "Month";
                ty[2] = "Day";
                ty[3] = "Hour";
                ty[4] = "Min";
                ty[5] = "Sec";

                inputsource[0] = date[0];
                inputsource[1] = date[1];
                inputsource[2] = date[2];
                inputsource[3] = date[3];
                inputsource[4] = date[4];
                inputsource[5] = date[5];

                if (test_channel)
                {
                    bunifuFlatButton9.Normalcolor = Color.FromArgb(65, 211, 253);
                    bunifuFlatButton9.Textcolor = Color.White;
                    test_on[6] = true;
                }
                else
                {
                    bunifuFlatButton9.Normalcolor = Color.FromArgb(57, 181, 74);
                    bunifuFlatButton9.Textcolor = Color.White;
                    button_on[6] = true;
                }
                runcg(ty1, inputsource1, layer1, channal1, "FO3INTERCON/L3_Countdown_Title");
                runcg(ty, inputsource, layer, channal, "FO3INTERCON/L3_Countdown_Time");
                countdowntick = true;
                countdowntimechannal = channal;
                countdowntimelayer = layer;
                times = new System.Windows.Forms.Timer();
                times.Interval = (((mintimer * 60) + sectimer) * 1000);
                times.Tick += new EventHandler(timer_Tick);
                times.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (countdowntick)
            {
                string[] ty = new string[1];
                string[] inputsource = new string[1];
                stopcg(countdowntimelayer, countdowntimechannal);
                runcg(ty, inputsource, countdowntimelayer, countdowntimechannal, "FO3INTERCON/L3_Countdown_Message");
                times.Stop();
            }

        }
    }
}
