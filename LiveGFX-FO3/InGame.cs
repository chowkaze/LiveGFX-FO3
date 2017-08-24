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
        bool[] button_on = new bool[20];
        bool[] test_on = new bool[20];
        bool test_channel = false;
        public InGame()
        {
            InitializeComponent();
            caspar_.Connected += new EventHandler<NetworkEventArgs>(caspar__Connected);
            caspar_.FailedConnect += new EventHandler<NetworkEventArgs>(caspar__FailedConnected);
            caspar_.Disconnected += new EventHandler<NetworkEventArgs>(caspar__Disconnected);
            for(int i = 0; i<button_on.Length;i++)
            {
                button_on[i] = false;
                test_on[i] = false;
            }
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
        public void runvideo(string name, int channal, int layer)
        {




            try
            {
                caspar_.SendString("PLAY " + channal + "-" + layer + " " + "\"" + name + "\"" + " MIX 20 EASEINSINE AUTO");
            }
            catch
            {
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
            metroComboBox2.Items.Clear();
            info = sqldb.getdata("*", "teamdatabase", "teamname", select);
            bunifuCustomLabel1.Text = info[0];
            bunifuCustomLabel69.Text = info[0];
            bunifuCustomLabel23.Text = info[0];
            bunifuCustomLabel16.Text = info[0] + " DRAGON";
            bunifuCustomLabel37.Text = info[0] + " DRAGON BUFF";
            bunifuCustomLabel14.Text = info[0] + " WIN";
            pictureBox1.ImageLocation = info[2];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            metroComboBox2.Items.Add(info[6]);
            metroComboBox2.Items.Add(info[7]);
            metroComboBox2.Items.Add(info[8]);
            metroComboBox2.Items.Add(info[9]);
            metroComboBox2.Items.Add(info[10]);
        }

        private void teamr_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string select1 = this.teamr_combo.GetItemText(this.teamr_combo.SelectedItem);
            metroComboBox3.Items.Clear();
            info1 = sqldb.getdata("*", "teamdatabase", "teamname", select1);
            bunifuCustomLabel9.Text = info1[0];
            bunifuCustomLabel50.Text = info1[0];
            bunifuCustomLabel68.Text = info1[0];
            bunifuCustomLabel35.Text = info1[0] + " DRAGON";
            bunifuCustomLabel38.Text = info1[0] + " DRAGON BUFF";
            bunifuCustomLabel36.Text = info1[0] + " WIN";
            pictureBox2.ImageLocation = info1[2];
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            metroComboBox3.Items.Add(info1[6]);
            metroComboBox3.Items.Add(info1[7]);
            metroComboBox3.Items.Add(info1[8]);
            metroComboBox3.Items.Add(info1[9]);
            metroComboBox3.Items.Add(info1[10]);
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
            metroComboBox2.Items.Clear();
            metroComboBox3.Items.Clear();
            metroComboBox2.Items.Add(info[6]);
            metroComboBox2.Items.Add(info[7]);
            metroComboBox2.Items.Add(info[8]);
            metroComboBox2.Items.Add(info[9]);
            metroComboBox2.Items.Add(info[10]);
            metroComboBox3.Items.Add(info1[6]);
            metroComboBox3.Items.Add(info1[7]);
            metroComboBox3.Items.Add(info1[8]);
            metroComboBox3.Items.Add(info1[9]);
            metroComboBox3.Items.Add(info1[10]);
        }

        private void bunifuFlatButton37_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[6];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[6];
                }
                if (check_on)
                {
                    bunifuFlatButton37.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton37.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[6] = false;
                    }
                    else
                    {
                        button_on[6] = false;
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
                        inputsource[4] = @"\\Casparcgserver\assets\Keystone\"+ this.metroComboBox55.GetItemText(this.metroComboBox55.SelectedItem)+".png";
                        inputsource[5] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox54.GetItemText(this.metroComboBox54.SelectedItem) + ".png";
                        inputsource[6] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox53.GetItemText(this.metroComboBox53.SelectedItem) + ".png";
                        inputsource[7] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox52.GetItemText(this.metroComboBox52.SelectedItem) + ".png";
                        inputsource[8] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox51.GetItemText(this.metroComboBox51.SelectedItem) + ".png";
                        inputsource[9] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox50.GetItemText(this.metroComboBox50.SelectedItem) + ".png";
                        inputsource[10] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox49.GetItemText(this.metroComboBox49.SelectedItem) + ".png";
                        inputsource[11] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox48.GetItemText(this.metroComboBox48.SelectedItem) + ".png";
                        inputsource[12] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox47.GetItemText(this.metroComboBox47.SelectedItem) + ".png";
                        inputsource[13] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox46.GetItemText(this.metroComboBox46.SelectedItem) + ".png";


                        if (test_channel)
                        {
                            bunifuFlatButton37.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton37.Textcolor = Color.White;
                            test_on[6] = true;
                        }
                        else
                        {
                            bunifuFlatButton37.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton37.Textcolor = Color.White;
                            button_on[6] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL2017/in_game1");
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
                bool check_on = button_on[2];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[2];
                }
                if (check_on)
                {
                    bunifuFlatButton12.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton12.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[2] = false;
                    }
                    else
                    {
                        button_on[2] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch(this.metroComboBox22.GetItemText(this.metroComboBox22.SelectedItem))
                        {
                            case "Cloud":
                                temp = "GPL2017/GPL_INGAME_ Dragonwind_L";
                                break;
                            case "Infernal":
                                temp = "GPL2017/GPL_INGAME_ Dragonfire_L";
                                break;
                            case "Ocean":
                                temp = "GPL2017/GPL_INGAME_ Dragonwater_L";
                                break;
                            case "Mountain":
                                temp = "GPL2017/GPL_INGAME_ Dragonearth_L";
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
                            test_on[2] = true;
                        }
                        else
                        {
                            bunifuFlatButton12.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton12.Textcolor = Color.White;
                            button_on[2] = true;
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
                bool check_on = button_on[3];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[3];
                }
                if (check_on)
                {
                    bunifuFlatButton13.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton13.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[3] = false;
                    }
                    else
                    {
                        button_on[3] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch (this.metroComboBox23.GetItemText(this.metroComboBox23.SelectedItem))
                        {
                            case "Cloud":
                                temp = "GPL2017/GPL_INGAME_ Dragonwind_R";
                                break;
                            case "Infernal":
                                temp = "GPL2017/GPL_INGAME_ Dragonfire_R";
                                break;
                            case "Ocean":
                                temp = "GPL2017/GPL_INGAME_ Dragonwater_R";
                                break;
                            case "Mountain":
                                temp = "GPL2017/GPL_INGAME_ Dragonearth_R";
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
                            test_on[3] = true;
                        }
                        else
                        {
                            bunifuFlatButton13.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton13.Textcolor = Color.White;
                            button_on[3] = true;
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
                bool check_on = button_on[1];
                string temp = "GPL2017/dragon_get";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[1];
                }
                if (check_on)
                {
                    bunifuFlatButton6.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton6.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[1] = false;
                    }
                    else
                    {
                        button_on[1] = false;
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
                        if (select != " "&& select!= "")
                        {
                            inputsource[0] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[0] = "";
                        }
                        select = this.metroComboBox6.GetItemText(this.metroComboBox6.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[1] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[1] = "";
                        }
                        select = this.metroComboBox7.GetItemText(this.metroComboBox7.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[2] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[2] = "";
                        }
                        select = this.metroComboBox8.GetItemText(this.metroComboBox8.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[3] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[3] = "";
                        }
                        select = this.metroComboBox9.GetItemText(this.metroComboBox9.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[4] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[4] = "";
                        }
                        select = this.metroComboBox10.GetItemText(this.metroComboBox10.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[5] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[5] = "";
                        }
                        select = this.metroComboBox11.GetItemText(this.metroComboBox11.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[6] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[6] = "";
                        }
                        select = this.metroComboBox12.GetItemText(this.metroComboBox12.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[7] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[7] = "";
                        }
                        select = this.metroComboBox19.GetItemText(this.metroComboBox19.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[8] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[8] = "";
                        }
                        select = this.metroComboBox18.GetItemText(this.metroComboBox18.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[9] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[9] = "";
                        }
                        select = this.metroComboBox17.GetItemText(this.metroComboBox17.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[10] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[10] = "";
                        }
                        select = this.metroComboBox16.GetItemText(this.metroComboBox16.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[11] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[11] = "";
                        }
                        select = this.metroComboBox15.GetItemText(this.metroComboBox15.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[12] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[12] = "";
                        }
                        select = this.metroComboBox14.GetItemText(this.metroComboBox14.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[13] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[13] = "";
                        }
                        select = this.metroComboBox13.GetItemText(this.metroComboBox13.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[14] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[14] = "";
                        }
                        select = this.metroComboBox1.GetItemText(this.metroComboBox1.SelectedItem);
                        if (select != " " && select != "")
                        {
                            inputsource[15] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
                        }
                        else
                        {
                            inputsource[15] = "";
                        }



                        if (test_channel)
                        {
                            bunifuFlatButton6.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton6.Textcolor = Color.White;
                            test_on[1] = true;
                        }
                        else
                        {
                            bunifuFlatButton6.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton6.Textcolor = Color.White;
                            button_on[1] = true;
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
                bool check_on = button_on[9];
                string temp = "GPL2017/Draft";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[9];
                }
                if (check_on)
                {
                    bunifuFlatButton14.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton14.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[9] = false;
                    }
                    else
                    {
                        button_on[9] = false;
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
                        inputsource[0] = @"\\Casparcgserver\assets\Draft_Logo\L\" + info[1] + ".png";
                        inputsource[1] = @"\\Casparcgserver\assets\Draft_Logo\R\" + info1[1] + ".png";
                        inputsource[2] = this.metroComboBox24.GetItemText(this.metroComboBox24.SelectedItem);
                        inputsource[3] = this.metroComboBox25.GetItemText(this.metroComboBox25.SelectedItem);
                        if (test_channel)
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton14.Textcolor = Color.White;
                            test_on[9] = true;
                        }
                        else
                        {
                            bunifuFlatButton14.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton14.Textcolor = Color.White;
                            button_on[9] = true;
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
                bool check_on = button_on[11];
                string temp = "GPL2017/highlights";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[11];
                }
                if (check_on)
                {
                    bunifuFlatButton38.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton38.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[11] = false;
                    }
                    else
                    {
                        button_on[11] = false;
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
                            test_on[11] = true;
                        }
                        else
                        {
                            bunifuFlatButton38.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton38.Textcolor = Color.White;
                            button_on[11] = true;
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
                bool check_on = button_on[10];
                string temp = "GPL2017/replay";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[10];
                }
                if (check_on)
                {
                    bunifuFlatButton8.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton8.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[10] = false;
                    }
                    else
                    {
                        button_on[10] = false;
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
                            test_on[10] = true;
                        }
                        else
                        {
                            bunifuFlatButton8.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton8.Textcolor = Color.White;
                            button_on[10] = true;
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
                string[] ty = new string[50];
                string[] inputsource = new string[50];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[0];
                string temp = "GPL2017/GPL_LoadGame";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bunifuFlatButton4.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton4.Textcolor = Color.Black;
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
                        ty[32] = "L_CName_1";
                        ty[33] = "L_CName_2";
                        ty[34] = "L_CName_3";
                        ty[35] = "L_CName_4";
                        ty[36] = "L_CName_5";
                        ty[37] = "R_CName_1";
                        ty[38] = "R_CName_2";
                        ty[39] = "R_CName_3";
                        ty[40] = "R_CName_4";
                        ty[41] = "R_CName_5";
                        inputsource[0] = @"\\Casparcgserver\assets\Draft_Logo\L\" + info[1] + ".png";
                        inputsource[1] = @"\\Casparcgserver\assets\Draft_Logo\R\" + info1[1] + ".png";
                        inputsource[2] = info[6].ToUpper();
                        inputsource[3] = info[7].ToUpper();
                        inputsource[4] = info[8].ToUpper();
                        inputsource[5] = info[9].ToUpper();
                        inputsource[6] = info[10].ToUpper();
                        inputsource[7] = @"\\Casparcgserver\assets\character\L\" + this.comboBox10.GetItemText(this.comboBox10.SelectedItem) + ".png";
                        inputsource[8] = @"\\Casparcgserver\assets\character\L\" + this.comboBox9.GetItemText(this.comboBox9.SelectedItem) + ".png";
                        inputsource[9] = @"\\Casparcgserver\assets\character\L\" + this.comboBox8.GetItemText(this.comboBox8.SelectedItem) + ".png";
                        inputsource[10] = @"\\Casparcgserver\assets\character\L\" + this.comboBox7.GetItemText(this.comboBox7.SelectedItem) + ".png";
                        inputsource[11] = @"\\Casparcgserver\assets\character\L\" + this.comboBox6.GetItemText(this.comboBox6.SelectedItem) + ".png";
                        inputsource[12] = info1[6].ToUpper();
                        inputsource[13] = info1[7].ToUpper();
                        inputsource[14] = info1[8].ToUpper();
                        inputsource[15] = info1[9].ToUpper();
                        inputsource[16] = info1[10].ToUpper();
                        inputsource[17] = @"\\Casparcgserver\assets\character\R\R-" + this.comboBox1.GetItemText(this.comboBox1.SelectedItem) + ".png";
                        inputsource[18] = @"\\Casparcgserver\assets\character\R\R-" + this.comboBox2.GetItemText(this.comboBox2.SelectedItem) + ".png";
                        inputsource[19] = @"\\Casparcgserver\assets\character\R\R-" + this.comboBox3.GetItemText(this.comboBox3.SelectedItem) + ".png";
                        inputsource[20] = @"\\Casparcgserver\assets\character\R\R-" + this.comboBox4.GetItemText(this.comboBox4.SelectedItem) + ".png";
                        inputsource[21] = @"\\Casparcgserver\assets\character\R\R-" + this.comboBox5.GetItemText(this.comboBox5.SelectedItem) + ".png";
                        inputsource[22] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox15.GetItemText(this.comboBox15.SelectedItem) + "Square.png";
                        inputsource[23] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox14.GetItemText(this.comboBox14.SelectedItem) + "Square.png";
                        inputsource[24] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox13.GetItemText(this.comboBox13.SelectedItem) + "Square.png";
                        inputsource[25] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox12.GetItemText(this.comboBox12.SelectedItem) + "Square.png";
                        inputsource[26] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox11.GetItemText(this.comboBox11.SelectedItem) + "Square.png";
                        inputsource[27] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox20.GetItemText(this.comboBox20.SelectedItem) + "Square.png";
                        inputsource[28] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox19.GetItemText(this.comboBox19.SelectedItem) + "Square.png";
                        inputsource[29] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox18.GetItemText(this.comboBox18.SelectedItem) + "Square.png";
                        inputsource[30] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox17.GetItemText(this.comboBox17.SelectedItem) + "Square.png";
                        inputsource[31] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox16.GetItemText(this.comboBox16.SelectedItem) + "Square.png";
                        inputsource[32] = this.comboBox10.GetItemText(this.comboBox10.SelectedItem).ToUpper();
                        inputsource[33] = this.comboBox9.GetItemText(this.comboBox9.SelectedItem).ToUpper();
                        inputsource[34] = this.comboBox8.GetItemText(this.comboBox8.SelectedItem).ToUpper();
                        inputsource[35] = this.comboBox7.GetItemText(this.comboBox7.SelectedItem).ToUpper();
                        inputsource[36] =  this.comboBox6.GetItemText(this.comboBox6.SelectedItem).ToUpper();
                        inputsource[37] =  this.comboBox1.GetItemText(this.comboBox1.SelectedItem).ToUpper();
                        inputsource[38] = this.comboBox2.GetItemText(this.comboBox2.SelectedItem).ToUpper();
                        inputsource[39] = this.comboBox3.GetItemText(this.comboBox3.SelectedItem).ToUpper();
                        inputsource[40] = this.comboBox4.GetItemText(this.comboBox4.SelectedItem).ToUpper();
                        inputsource[41] = this.comboBox5.GetItemText(this.comboBox5.SelectedItem).ToUpper();
                        if (test_channel)
                        {
                            bunifuFlatButton4.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton4.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bunifuFlatButton4.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton4.Textcolor = Color.White;
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
                bool check_on = button_on[4];
                string temp = "";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[4];
                }
                if (check_on)
                {
                    bunifuFlatButton7.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton7.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[4] = false;
                    }
                    else
                    {
                        button_on[4] = false;
                    }
                }
                else
                {
                    try
                    {
                        switch (this.metroComboBox4.GetItemText(this.metroComboBox4.SelectedItem))
                        {
                            case "Cloud":
                                temp = "GPL2017/GPL_INGAME_ NEXT_Dragonwind";
                                break;
                            case "Infernal":
                                temp = "GPL2017/GPL_INGAME_ NEXT_Dragonfire";
                                break;
                            case "Ocean":
                                temp = "GPL2017/GPL_INGAME_ NEXT_Dragonwater";
                                break;
                            case "Mountain":
                                temp = "GPL2017/GPL_INGAME_ NEXT_Dragonearth";
                                break;
                            case "Elder":
                                temp = "GPL2017/GPL_INGAME_ NEXT_DragonElder";
                                break;
                            default:
                                break;
                        }


                        if (test_channel)
                        {
                            bunifuFlatButton7.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton7.Textcolor = Color.White;
                            test_on[4] = true;
                        }
                        else
                        {
                            bunifuFlatButton7.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton7.Textcolor = Color.White;
                            button_on[4] = true;
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
            bool check_on = button_on[5];
            if (test_channel)
            {
                channal = 2;
                channal1 = 2;
                check_on = test_on[5];
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
                    test_on[5] = true;
                }
                else
                {
                    bunifuFlatButton9.Normalcolor = Color.FromArgb(57, 181, 74);
                    bunifuFlatButton9.Textcolor = Color.White;
                    button_on[5] = true;
                }
                runcg(ty1, inputsource1, layer1, channal1, "GPL2017/dragon_timer");
                runcg(ty, inputsource, layer, channal, "GPL2017/dragon_time");
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
                runcg(ty, inputsource, countdowntimelayer, countdowntimechannal, "GPL2017/dragon_live");
                times.Stop();
            }

        }

        private void bunifuFlatButton17_Click(object sender, EventArgs e)
        {
            string[] inputsource = new string[20];
            
            string select = this.metroComboBox5.GetItemText(this.metroComboBox5.SelectedItem);
            if (select != " "&&select !="")
            {
                inputsource[0] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[0] = "";
            }
            select = this.metroComboBox6.GetItemText(this.metroComboBox6.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[1] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[1] = "";
            }
            select = this.metroComboBox7.GetItemText(this.metroComboBox7.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[2] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[2] = "";
            }
            select = this.metroComboBox8.GetItemText(this.metroComboBox8.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[3] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[3] = "";
            }
            select = this.metroComboBox9.GetItemText(this.metroComboBox9.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[4] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[4] = "";
            }
            select = this.metroComboBox10.GetItemText(this.metroComboBox10.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[5] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[5] = "";
            }
            select = this.metroComboBox11.GetItemText(this.metroComboBox11.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[6] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[6] = "";
            }
            select = this.metroComboBox12.GetItemText(this.metroComboBox12.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[7] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[7] = "";
            }
            select = this.metroComboBox19.GetItemText(this.metroComboBox19.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[8] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[8] = "";
            }
            select = this.metroComboBox18.GetItemText(this.metroComboBox18.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[9] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[9] = "";
            }
            select = this.metroComboBox17.GetItemText(this.metroComboBox17.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[10] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[10] = "";
            }
            select = this.metroComboBox16.GetItemText(this.metroComboBox16.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[11] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[11] = "";
            }
            select = this.metroComboBox15.GetItemText(this.metroComboBox15.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[12] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[12] = "";
            }
            select = this.metroComboBox14.GetItemText(this.metroComboBox14.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[13] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[13] = "";
            }
            select = this.metroComboBox13.GetItemText(this.metroComboBox13.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[14] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[14] = "";
            }
            select = this.metroComboBox1.GetItemText(this.metroComboBox1.SelectedItem);
            if (select != " " && select != "")
            {
                inputsource[15] = @"\\Casparcgserver\assets\Dragon\" + select + ".png";
            }
            else
            {
                inputsource[15] = "";
            }

            cgData.Clear();
            cgData.SetData("dragonl1", inputsource[0]);
            cgData.SetData("dragonl2", inputsource[1]);
            cgData.SetData("dragonl3", inputsource[2]);
            cgData.SetData("dragonl4", inputsource[3]);
            cgData.SetData("dragonl5", inputsource[4]);
            cgData.SetData("dragonl6", inputsource[5]);
            cgData.SetData("dragonl7", inputsource[6]);
            cgData.SetData("dragonl8", inputsource[7]);
            cgData.SetData("dragonr1", inputsource[8]);
            cgData.SetData("dragonr2", inputsource[9]);
            cgData.SetData("dragonr3", inputsource[10]);
            cgData.SetData("dragonr4", inputsource[11]);
            cgData.SetData("dragonr5", inputsource[12]);
            cgData.SetData("dragonr6", inputsource[13]);
            cgData.SetData("dragonr7", inputsource[14]);
            cgData.SetData("dragonr8", inputsource[15]);
            caspar_.Channels[0].CG.Update(16, cgData);
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string selected = this.match_combobox.GetItemText(this.match_combobox.SelectedItem);
            int channal = 2;
            if (test_channel)
            {
                channal = 3;
            }
            switch (selected)
            {
                case "Round 1":
                    if (bo1_checkbox.Checked)
                    {
                        runvideo("GPL2017/GPL_Gamefinal", channal, 18);
                    }
                    else
                    {
                        runvideo("GPL2017/GPL_Game1", channal, 18);
                    }

                    break;
                case "Round 2":
                    runvideo("GPL2017/GPL_Game2", channal, 18);
                    break;
                case "Round 3":
                    if (bo3_checkbox.Checked)
                    {
                        runvideo("GPL2017/GPL_Gamefinal", channal, 18);
                    }
                    else
                    {
                        runvideo("GPL2017/GPL_Game3", channal, 18);
                    }

                    break;
                case "Round 4":
                    runvideo("GPL2017/GPL_Game4", channal, 18);
                    break;
                case "Round 5":
                    runvideo("GPL2017/GPL_Gamefinal", channal, 18);
                    break;
                default:
                    break;

            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            runvideo("GPL2017/GPL_VS_"+info[1]+"-"+info1[1], 2, 18);
        }

        private void bunifuFlatButton16_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 18;
                int channal = 1;
                bool check_on = button_on[7];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[7];
                }
                if (check_on)
                {
                    bunifuFlatButton16.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton16.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[7] = false;
                    }
                    else
                    {
                        button_on[7] = false;
                    }
                }
                else
                {
                    string select = this.metroComboBox20.GetItemText(this.metroComboBox20.SelectedItem);
                    try
                    {
                        if(select == "winner_s")
                        {
                            ty[0] = "Title";
                            ty[1] = "Desc";
                            ty[2] = "Logo";
                            inputsource[0] = info[0];
                            string selected = this.metroComboBox24.GetItemText(this.metroComboBox24.SelectedItem);
                            string selected1 = this.metroComboBox25.GetItemText(this.metroComboBox25.SelectedItem);
                            int lscore = Int32.Parse(selected);
                            int rscore = Int32.Parse(selected1);
                            if(lscore > rscore)
                            {
                                inputsource[1] = "LEADS SERIES "+ selected + " - " + selected1 ; 
                            }
                            else
                            {
                                if(lscore == rscore)
                                {
                                    inputsource[1] = "TIES SERIES " + selected + " - " + selected1;
                                }
                                else
                                {
                                    if (lscore < rscore)
                                    {
                                        inputsource[1] = "TRAILS SERIES " + selected + " - " + selected1;
                                    }
                                }
                            }
                            inputsource[2] = info[2];
                            if (test_channel)
                            {
                                bunifuFlatButton16.Normalcolor = Color.FromArgb(65, 211, 253);
                                bunifuFlatButton16.Textcolor = Color.White;
                                test_on[7] = true;
                            }
                            else
                            {
                                bunifuFlatButton16.Normalcolor = Color.FromArgb(57, 181, 74);
                                bunifuFlatButton16.Textcolor = Color.White;
                                button_on[7] = true;
                            }
                            runcg(ty, inputsource, layer, channal, "GPL2017/GPL_TAB_Winner");
                        }
                        else
                        {
                            runvideo("GPL2017/"+info[1], channal+1, layer);
                        }
                        
                       
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

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 18;
                int channal = 1;
                bool check_on = button_on[8];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[8];
                }
                if (check_on)
                {
                    bunifuFlatButton11.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton11.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[8] = false;
                    }
                    else
                    {
                        button_on[8] = false;
                    }
                }
                else
                {
                    string select = this.metroComboBox21.GetItemText(this.metroComboBox21.SelectedItem);
                    try
                    {
                        if (select == "winner_s")
                        {
                            ty[0] = "Title";
                            ty[1] = "Desc";
                            ty[2] = "Logo";
                            inputsource[0] = info1[0];
                            string selected = this.metroComboBox24.GetItemText(this.metroComboBox24.SelectedItem);
                            string selected1 = this.metroComboBox25.GetItemText(this.metroComboBox25.SelectedItem);
                            int lscore = Int32.Parse(selected);
                            int rscore = Int32.Parse(selected1);
                            if (rscore > lscore)
                            {
                                inputsource[1] = "LEADS SERIES " + selected + " - " + selected1;
                            }
                            else
                            {
                                if (lscore == rscore)
                                {
                                    inputsource[1] = "TIES SERIES " + selected + " - " + selected1;
                                }
                                else
                                {
                                    if (lscore > rscore)
                                    {
                                        inputsource[1] = "TRAILS SERIES " + selected + " - " + selected1;
                                    }
                                }
                            }
                            inputsource[2] = info1[2];
                            if (test_channel)
                            {
                                bunifuFlatButton11.Normalcolor = Color.FromArgb(65, 211, 253);
                                bunifuFlatButton11.Textcolor = Color.White;
                                test_on[8] = true;
                            }
                            else
                            {
                                bunifuFlatButton11.Normalcolor = Color.FromArgb(57, 181, 74);
                                bunifuFlatButton11.Textcolor = Color.White;
                                button_on[8] = true;
                            }
                            runcg(ty, inputsource, layer, channal, "GPL2017/GPL_TAB_Winner");
                        }
                        else
                        {
                            runvideo("GPL2017/"+info1[1], channal+1, layer);
                        }


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

        private void clear_ch1_Click(object sender, EventArgs e)
        {
            try
            {
                this.caspar_.Channels[0].CG.Clear();
                this.caspar_.Channels[0].Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private void clear_ch2_Click(object sender, EventArgs e)
        {
            try
            {
                this.caspar_.Channels[1].CG.Clear();
                this.caspar_.Channels[1].Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private void clear_ch3_Click(object sender, EventArgs e)
        {
            try
            {
                this.caspar_.Channels[2].CG.Clear();
                this.caspar_.Channels[2].Clear();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 18;
                int channal = 1;
                bool check_on = button_on[12];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[12];
                }
                if (check_on)
                {
                    bunifuFlatButton10.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton10.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[12] = false;
                    }
                    else
                    {
                        button_on[12] = false;
                    }
                }
                else
                {
                    string select = this.metroComboBox2.GetItemText(this.metroComboBox2.SelectedItem);
                    string selected = this.metroComboBox3.GetItemText(this.metroComboBox3.SelectedItem);

                    try
                    {
                        ty[0] = "L_Logo";
                        ty[1] = "R_Logo";
                        ty[2] = "L_Name";
                        ty[3] = "R_Name";
                        ty[4] = "L_Desc";
                        ty[5] = "R_Desc";
                        ty[6] = "L_Role";
                        ty[7] = "R_Role";
                        inputsource[0] = @"\\Casparcgserver\assets\KDA_Player\"+info[1]+"\\L\\" + select + ".png"; ;
                        inputsource[1] = @"\\Casparcgserver\assets\KDA_Player\" + info1[1] + "\\R\\" + selected + ".png"; ;
                        inputsource[2] = select.ToUpper();
                        inputsource[3] = selected.ToUpper();
                        inputsource[4] = analyzer_name.Text;
                        inputsource[5] = bunifuMaterialTextbox1.Text;
                        switch(this.metroComboBox2.SelectedIndex)
                        {
                            case 0:
                                inputsource[6] = "TOP";
                                break;
                            case 1:
                                inputsource[6] = "JUNG";
                                break;
                            case 2:
                                inputsource[6] = "MID";
                                break;
                            case 3:
                                inputsource[6] = "ADC";
                                break;
                            case 4:
                                inputsource[6] = "SUP";
                                break;
                            default:
                                break;
                        }
                        switch (this.metroComboBox3.SelectedIndex)
                        {
                            case 0:
                                inputsource[7] = "TOP";
                                break;
                            case 1:
                                inputsource[7] = "JUNG";
                                break;
                            case 2:
                                inputsource[7] = "MID";
                                break;
                            case 3:
                                inputsource[7] = "ADC";
                                break;
                            case 4:
                                inputsource[7] = "SUP";
                                break;
                            default:
                                break;
                        }
                        if (test_channel)
                        {
                            bunifuFlatButton10.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton10.Textcolor = Color.White;
                            test_on[12] = true;
                        }
                        else
                        {
                            bunifuFlatButton10.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton10.Textcolor = Color.White;
                            button_on[12] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL2017/GPL_TAB_KDA");
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

        private void bunifuFlatButton18_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[50];
                string[] inputsource = new string[50];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[13];
                string temp = "GPL2017/GPL_TAB_ANALYZE";
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[13];
                }
                if (check_on)
                {
                    bunifuFlatButton18.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton18.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[13] = false;
                    }
                    else
                    {
                        button_on[13] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "L_Logo";
                        ty[1] = "R_Logo";
                        ty[2] = "L_Pick1";
                        ty[3] = "L_Pick2";
                        ty[4] = "L_Pick3";
                        ty[5] = "L_Pick4";
                        ty[6] = "L_Pick5";
                        ty[7] = "R_Pick1";
                        ty[8] = "R_Pick2";
                        ty[9] = "R_Pick3";
                        ty[10] = "R_Pick4";
                        ty[11] = "R_Pick5";
                        ty[12] = "L_Ban1";
                        ty[13] = "L_Ban2";
                        ty[14] = "L_Ban3";
                        ty[15] = "L_Ban4";
                        ty[16] = "L_Ban5";
                        ty[17] = "R_Ban1";
                        ty[18] = "R_Ban2";
                        ty[19] = "R_Ban3";
                        ty[20] = "R_Ban4";
                        ty[21] = "R_Ban5";
                        ty[22] = "L_Stat";
                        ty[23] = "R_Stat";
                        ty[24] = "Title";
                        inputsource[0] = info[2];
                        inputsource[1] = info1[2];
                        inputsource[2] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox10.GetItemText(this.comboBox10.SelectedItem) + "Square.png";
                        inputsource[3] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox9.GetItemText(this.comboBox9.SelectedItem) + "Square.png";
                        inputsource[4] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox8.GetItemText(this.comboBox8.SelectedItem) + "Square.png";
                        inputsource[5] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox7.GetItemText(this.comboBox7.SelectedItem) + "Square.png";
                        inputsource[6] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox6.GetItemText(this.comboBox6.SelectedItem) + "Square.png";
                        inputsource[7] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox1.GetItemText(this.comboBox1.SelectedItem) + "Square.png";
                        inputsource[8] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox2.GetItemText(this.comboBox2.SelectedItem) + "Square.png";
                        inputsource[9] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox3.GetItemText(this.comboBox3.SelectedItem) + "Square.png";
                        inputsource[10] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox4.GetItemText(this.comboBox4.SelectedItem) + "Square.png";
                        inputsource[11] = @"\\Casparcgserver\assets\Champion Icon\" + this.comboBox5.GetItemText(this.comboBox5.SelectedItem) + "Square.png";
                        inputsource[12] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox15.GetItemText(this.comboBox15.SelectedItem) + "Square.png";
                        inputsource[13] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox14.GetItemText(this.comboBox14.SelectedItem) + "Square.png";
                        inputsource[14] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox13.GetItemText(this.comboBox13.SelectedItem) + "Square.png";
                        inputsource[15] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox12.GetItemText(this.comboBox12.SelectedItem) + "Square.png";
                        inputsource[16] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox11.GetItemText(this.comboBox11.SelectedItem) + "Square.png";
                        inputsource[17] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox20.GetItemText(this.comboBox20.SelectedItem) + "Square.png";
                        inputsource[18] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox19.GetItemText(this.comboBox19.SelectedItem) + "Square.png";
                        inputsource[19] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox18.GetItemText(this.comboBox18.SelectedItem) + "Square.png";
                        inputsource[20] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox17.GetItemText(this.comboBox17.SelectedItem) + "Square.png";
                        inputsource[21] = @"\\Casparcgserver\assets\character\bw\" + this.comboBox16.GetItemText(this.comboBox16.SelectedItem) + "Square.png";
                        inputsource[24] = bunifuMaterialTextbox2.Text;
                        if(analyzed_winl.Checked)
                        {
                            inputsource[22] = "WIN";
                            inputsource[23] = "LOSS";
                        }
                        else
                        {
                            if(analyzed_winr.Checked)
                            {
                                inputsource[23] = "WIN";
                                inputsource[22] = "LOSS";
                            }
                        }
                        if (test_channel)
                        {
                            bunifuFlatButton18.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton18.Textcolor = Color.White;
                            test_on[13] = true;
                        }
                        else
                        {
                            bunifuFlatButton18.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton18.Textcolor = Color.White;
                            button_on[13] = true;
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

        private void bunifuFlatButton20_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 0;
                bool check_on = button_on[14];
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[14];
                }
                if (check_on)
                {
                    bunifuFlatButton20.Normalcolor = Color.FromArgb(183, 183, 183);
                    bunifuFlatButton20.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[14] = false;
                    }
                    else
                    {
                        button_on[14] = false;
                    }
                }
                else
                {
                    try
                    {
                        ty[0] = "keystonel1";
                        ty[1] = "keystonel2";
                        ty[2] = "keystonel3";
                        ty[3] = "keystonel4";
                        ty[4] = "keystonel5";
                        ty[5] = "keystoner1";
                        ty[6] = "keystoner2";
                        ty[7] = "keystoner3";
                        ty[8] = "keystoner4";
                        ty[9] = "keystoner5";
                        inputsource[0] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox35.GetItemText(this.metroComboBox35.SelectedItem) + ".png";
                        inputsource[1] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox34.GetItemText(this.metroComboBox34.SelectedItem) + ".png";
                        inputsource[2] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox33.GetItemText(this.metroComboBox33.SelectedItem) + ".png";
                        inputsource[3] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox32.GetItemText(this.metroComboBox32.SelectedItem) + ".png";
                        inputsource[4] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox31.GetItemText(this.metroComboBox31.SelectedItem) + ".png";
                        inputsource[5] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox30.GetItemText(this.metroComboBox30.SelectedItem) + ".png";
                        inputsource[6] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox29.GetItemText(this.metroComboBox29.SelectedItem) + ".png";
                        inputsource[7] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox28.GetItemText(this.metroComboBox28.SelectedItem) + ".png";
                        inputsource[8] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox27.GetItemText(this.metroComboBox27.SelectedItem) + ".png";
                        inputsource[9] = @"\\Casparcgserver\assets\Keystone\" + this.metroComboBox26.GetItemText(this.metroComboBox26.SelectedItem) + ".png";


                        if (test_channel)
                        {
                            bunifuFlatButton20.Normalcolor = Color.FromArgb(65, 211, 253);
                            bunifuFlatButton20.Textcolor = Color.White;
                            test_on[14] = true;
                        }
                        else
                        {
                            bunifuFlatButton20.Normalcolor = Color.FromArgb(57, 181, 74);
                            bunifuFlatButton20.Textcolor = Color.White;
                            button_on[14] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL2017/keystone");
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
    }
}
