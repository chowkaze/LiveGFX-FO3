﻿using System;
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
    public partial class LiveEvent : UserControl
    {
        CasparDevice caspar_ = new CasparDevice();
        CasparCGDataCollection cgData = new CasparCGDataCollection();
        db sqldb = new db();
        private delegate void UpdateGUI(object parameter);
        bool[] button_on = new bool[17];
        bool[] test_on = new bool[17];
        bool test_channel = false;
        List<string> teamname = new List<string>();
        List<string> l3 = new List<string>();
        List<string> analyzer = new List<string>();
        List<string> caster = new List<string>();
        List<string> l3_long = new List<string>();
        List<string> bracket = new List<string>();
        List<string> tm = new List<string>();
        int countdowntimelayer = 0;
        int countdowntimechannal = 0;
        bool countdowntick = false;
        System.Windows.Forms.Timer times = new System.Windows.Forms.Timer();
        int nextmatchtimelayer = 0;
        int nextmatchtimechannal = 0;
        bool nextmatchtick = false;
        System.Windows.Forms.Timer nextmatchtimes = new System.Windows.Forms.Timer();
        public LiveEvent()
        {
            InitializeComponent();
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

        private void l3_1_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[0];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    l3_1_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_1_butt.Textcolor = Color.Black;
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
                    string select = this.l3_1.GetItemText(this.l3_1.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_1_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_1_butt.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            l3_1_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_1_butt.Textcolor = Color.White;
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

        private void l3_2_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[1];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[1];
                }
                if (check_on)
                {
                    l3_2_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_2_butt.Textcolor = Color.Black;
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
                    string select = this.l3_2.GetItemText(this.l3_2.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_2_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_2_butt.Textcolor = Color.White;
                            test_on[1] = true;
                        }
                        else
                        {
                            l3_2_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_2_butt.Textcolor = Color.White;
                            button_on[1] = true;
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

        private void l3_3_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[2];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[2];
                }
                if (check_on)
                {
                    l3_3_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_3_butt.Textcolor = Color.Black;
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
                    string select = this.l3_3.GetItemText(this.l3_3.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_3_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_3_butt.Textcolor = Color.White;
                            test_on[2] = true;
                        }
                        else
                        {
                            l3_3_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_3_butt.Textcolor = Color.White;
                            button_on[2] = true;
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

        private void tab2_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[6];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[6];
                }
                if (check_on)
                {
                    tab2_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    tab2_butt.Textcolor = Color.Black;
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
                    string select = this.tab2_1.GetItemText(this.tab2_1.SelectedItem);
                    string selected = this.tab2_2.GetItemText(this.tab2_2.SelectedItem);

                    try
                    {
                        ty[0] = "title1";
                        ty[1] = "desc1";
                        ty[2] = "title2";
                        ty[3] = "desc2";
                        info = sqldb.getdata("description", "caster_lowerthird", "name", select);
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        inputsource[2] = selected;
                        info = sqldb.getdata("description", "caster_lowerthird", "name", selected);
                        inputsource[3] = info[0];
                        if (test_channel)
                        {
                            tab2_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            tab2_butt.Textcolor = Color.White;
                            test_on[6] = true;
                        }
                        else
                        {
                            tab2_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            tab2_butt.Textcolor = Color.White;
                            button_on[6] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/tab_2");
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

        private void tab3_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[7];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[7];
                }
                if (check_on)
                {
                    tab3_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    tab3_butt.Textcolor = Color.Black;
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
                    string select = this.tab3_1.GetItemText(this.tab3_1.SelectedItem);
                    string selected = this.tab3_2.GetItemText(this.tab3_2.SelectedItem);
                    string selected1 = this.tab3_3.GetItemText(this.tab3_3.SelectedItem);

                    try
                    {
                        ty[0] = "title1";
                        ty[1] = "desc1";
                        ty[2] = "title2";
                        ty[3] = "desc2";
                        ty[4] = "title3";
                        ty[5] = "desc3";
                        info = sqldb.getdata("description", "analyzer_lowerthird", "name", select);
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        inputsource[2] = selected;
                        info = sqldb.getdata("description", "analyzer_lowerthird", "name", selected);
                        inputsource[3] = info[0];
                        inputsource[4] = selected1;
                        info = sqldb.getdata("description", "analyzer_lowerthird", "name", selected);
                        inputsource[5] = info[0];
                        if (test_channel)
                        {
                            tab3_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            tab3_butt.Textcolor = Color.White;
                            test_on[7] = true;
                        }
                        else
                        {
                            tab3_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            tab3_butt.Textcolor = Color.White;
                            button_on[7] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/tab_2");
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

        private void l3_team1_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[8];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[8];
                }
                if (check_on)
                {
                    l3_team1_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_team1_butt.Textcolor = Color.Black;
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
                    string select = this.l3_team1.GetItemText(this.l3_team1.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("*", "teamdatabase", "teamname", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_team1_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_team1_butt.Textcolor = Color.White;
                            test_on[8] = true;
                        }
                        else
                        {
                            l3_team1_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_team1_butt.Textcolor = Color.White;
                            button_on[8] = true;
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

        private async void tm_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] date = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss:tt").Split(':');
                int year = Int32.Parse(date[0]);
                int hr = Int32.Parse(date[3]);
                int min = Int32.Parse(date[4]);
                int sec = Int32.Parse(date[5]);
                int mintimer = Int32.Parse(tm_min.Text);
                int sectimer = Int32.Parse(tm_sec.Text);
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                string[] ty1 = new string[20];
                string[] inputsource1 = new string[20];
                List<string> teaml = new List<string>();
                List<string> teamr = new List<string>();
                List<string> info = new List<string>();
                int layer = 15;
                int channal = 0;
                int layer1 = 14;
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
                    tm_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    tm_butt.Textcolor = Color.Black;
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
                    nextmatchtick = false;
                    nextmatchtimechannal = 0;
                    nextmatchtimelayer = 0;
                    nextmatchtimes.Stop();
                }
                else
                {
                    string select = this.tm_combo.GetItemText(this.tm_combo.SelectedItem);
                    info = sqldb.getdata("*", "today_matches", "savename", select);
                    ty1[0] = "desc";
                    ty1[1] = "Q1_Team_L";
                    ty1[2] = "Q1_Team_R";
                    ty1[3] = "Q1_Logo_L";
                    ty1[4] = "Q1_Logo_L";
                    ty1[5] = "Q2_Team_L";
                    ty1[6] = "Q2_Team_R";
                    ty1[7] = "Q2_Logo_L";
                    ty1[8] = "Q2_Logo_R";
                    ty1[9] = "Q1_Stat";
                    ty1[10] = "Q2_Stat";
                    ty1[11] = "Q1_Black_L";
                    ty1[12] = "Q1_Black_R";
                    ty1[13] = "Q2_Black_L";
                    ty1[14] = "Q2_Black_R";

                    teaml = sqldb.getdata("*", "teamdatabase", "teamname", info[2]);
                    teamr = sqldb.getdata("*", "teamdatabase", "teamname", info[3]);
                    inputsource1[0] = info[1];
                    inputsource1[1] = teaml[0];
                    inputsource1[2] = teamr[0];
                    inputsource1[3] = teaml[2];
                    inputsource1[4] = teamr[2];
                    teaml = sqldb.getdata("*", "teamdatabase", "teamname", info[4]);
                    teamr = sqldb.getdata("*", "teamdatabase", "teamname", info[5]);
                    inputsource1[5] = teaml[0];
                    inputsource1[6] = teamr[0];
                    inputsource1[7] = teaml[2];
                    inputsource1[8] = teamr[2];
                    inputsource1[9] = info[6];
                    inputsource1[10] = info[7];
                    if(info[8] == "win")
                    {
                        inputsource[11] = "";
                        inputsource[12] = "";
                    }
                    else
                    {
                        if(info[9] == "win")
                        {
                            inputsource[11] = "";
                            inputsource[12] = "";
                        }
                        else
                        {
                            inputsource[11] = "";
                            inputsource[12] = "";
                        }
                    }
                    if (info[10] == "win")
                    {
                        inputsource[13] = "";
                        inputsource[14] = "";
                    }
                    else
                    {
                        if (info[11] == "win")
                        {
                            inputsource[13] = "";
                            inputsource[14] = "";
                        }
                        else
                        {
                            inputsource[13] = "";
                            inputsource[14] = "";
                        }
                    }
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
                        tm_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                        tm_butt.Textcolor = Color.White;
                        test_on[6] = true;
                    }
                    else
                    {
                        tm_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                        tm_butt.Textcolor = Color.White;
                        button_on[6] = true;
                    }
                    runcg(ty1, inputsource1, layer1, channal1, "INFESTPRO2017/ro_today_matches");

                    nextmatchtick = true;
                    nextmatchtimechannal = channal;
                    nextmatchtimelayer = layer;
                    if (tm_min.Text == "00"&& tm_sec.Text == "00")
                    {
                        stopcg(nextmatchtimelayer, nextmatchtimechannal);
                        await Task.Delay(2000);
                        runcg(ty, inputsource, nextmatchtimelayer, nextmatchtimechannal, "INFESTPRO2017/today_match_message");
                        nextmatchtimes.Stop();
                    }
                    else
                    {
                        runcg(ty, inputsource, layer, channal, "INFESTPRO2017/today_match_time");
                        nextmatchtimes = new System.Windows.Forms.Timer();
                        nextmatchtimes.Interval = (((mintimer * 60) + sectimer + 2) * 1000);
                        nextmatchtimes.Tick += new EventHandler(timer_Tick2);
                        nextmatchtimes.Start();
                    }
                }
            }
            else
            {
                MessageBox.Show("You forget to connected server", "INVLIVE-GFX",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }
        private void timer_Tick2(object sender, EventArgs e)
        {
            if (nextmatchtick)
            {
                string[] ty = new string[1];
                string[] inputsource = new string[1];
                stopcg(nextmatchtimelayer, nextmatchtimechannal);
                runcg(ty, inputsource, nextmatchtimelayer, nextmatchtimechannal, "INFESTPRO2017/today_match_message");
                nextmatchtimes.Stop();
            }

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            string[] date = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss:tt").Split(':');
            int year = Int32.Parse(date[0]);
            int hr = Int32.Parse(date[3]);
            int min = Int32.Parse(date[4]);
            int sec = Int32.Parse(date[5]);
            int mintimer = Int32.Parse(tab_timemin.Text);
            int sectimer = Int32.Parse(tab_timesec.Text);
            string[] ty = new string[20];
            string[] inputsource = new string[20];
            string[] ty1 = new string[20];
            string[] inputsource1 = new string[20];
            int layer = 15;
            int channal = 1;
            int layer1 = 14;
            int channal1 = 1;
            bool check_on = button_on[6];
            if (test_channel)
            {
                channal = 2;
                channal1 = 2;
                check_on = test_on[6];
            }
            if (check_on)
            {
                tab_timebutt.Normalcolor = Color.FromArgb(183, 183, 183);
                tab_timebutt.Textcolor = Color.Black;
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
                ty1[0] = "title";
                inputsource1[0] = tab_timetitle.Text;
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
                    tab_timebutt.Normalcolor = Color.FromArgb(65, 211, 253);
                    tab_timebutt.Textcolor = Color.White;
                    test_on[6] = true;
                }
                else
                {
                    tab_timebutt.Normalcolor = Color.FromArgb(57, 181, 74);
                    tab_timebutt.Textcolor = Color.White;
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
                ty[0] = "message";
                inputsource[0] = tab_timemess.Text;
                stopcg(countdowntimelayer, countdowntimechannal);
                runcg(ty, inputsource, countdowntimelayer, countdowntimechannal, "FO3INTERCON/L3_Countdown_Message");
                times.Stop();
            }

        }

        private void current_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[0];
                List<string> teaml = new List<string>();
                List<string> teamr = new List<string>();
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    current_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    current_butt.Textcolor = Color.Black;
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
                        ty[2] = "L_Name";
                        ty[3] = "R_Name";
                        ty[4] = "Center";
                        string select = this.current_teaml.GetItemText(this.current_teaml.SelectedItem);
                        string selected = this.current_teamr.GetItemText(this.current_teamr.SelectedItem);
                        teaml = sqldb.getdata("*", "teamdatabase", "teamname", select);
                        teamr = sqldb.getdata("*", "teamdatabase", "teamname", selected);
                        inputsource[0] = teaml[2];
                        inputsource[1] = teamr[2];
                        inputsource[2] = teaml[0];
                        inputsource[3] = teamr[0];
                        inputsource[3] = current_mess.Text;
                        if (test_channel)
                        {
                            current_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            current_butt.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            current_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            current_butt.Textcolor = Color.White;
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[7];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[7];
                }
                if (check_on)
                {
                    predict_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    predict_butt.Textcolor = Color.Black;
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
                    

                    try
                    {
                        ty[0] = "title1";
                        ty[1] = "title2";
                        ty[2] = "title3";
                        ty[3] = "team1";
                        ty[4] = "team2";
                        ty[5] = "team3";
                        string select = this.predict_1.GetItemText(this.predict_1.SelectedItem);
                        string selected = this.predict_2.GetItemText(this.predict_2.SelectedItem);
                        string selected1 = this.predict_3.GetItemText(this.predict_3.SelectedItem);
                        inputsource[0] = select;
                        inputsource[1] = selected;
                        inputsource[2] = selected1;
                        select = this.predict_team1.GetItemText(this.predict_team1.SelectedItem);
                        info = sqldb.getdata("logo", "teamdatabase", "teamname", select);
                        inputsource[3] = info[0];
                        select = this.predict_team2.GetItemText(this.predict_team2.SelectedItem);
                        info = sqldb.getdata("logo", "teamdatabase", "teamname", select);
                        inputsource[4] = info[0];
                        select = this.predict_team3.GetItemText(this.predict_team3.SelectedItem);
                        info = sqldb.getdata("logo", "teamdatabase", "teamname", select);
                        inputsource[5] = info[0];
                        if (test_channel)
                        {
                            predict_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            predict_butt.Textcolor = Color.White;
                            test_on[7] = true;
                        }
                        else
                        {
                            predict_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            predict_butt.Textcolor = Color.White;
                            button_on[7] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/tab_2");
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

        private void bracket_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[0];
                List<string> info = new List<string>();
                List<string> teamm = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    bracket_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    bracket_butt.Textcolor = Color.Black;
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
                    string select = this.bracket_combo.GetItemText(this.bracket_combo.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("*", "bracket", "title", select);
                        for(int i = 0;i<10;i++)
                        {
                            ty[i] = "team" + (i + 1);
                            ty[i + 10] = "score" + (i+1);
                            teamm = sqldb.getdata("bracket_pic", "teamdatabase", "teamname", info[i + 1]);
                            inputsource[i] = teamm[0];
                            inputsource[i + 10] = info[i + 11];
                        }
                        if (test_channel)
                        {
                            bracket_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            bracket_butt.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            bracket_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            bracket_butt.Textcolor = Color.White;
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

        private void analyzed_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[0];
                List<string> info = new List<string>();
                List<string> teaml = new List<string>();
                List<string> teamr = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[0];
                }
                if (check_on)
                {
                    analyzed_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    analyzed_butt.Textcolor = Color.Black;
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
                    string select = this.analyzed_combo.GetItemText(this.analyzed_combo.SelectedItem);
                   
                    try
                    {
                        info = sqldb.getdata("*", "analyzed_tab", "title", select);
                        ty[20] = "L_Logo";
                        ty[21] = "L_Stat";
                        ty[22] = "R_Logo";
                        ty[23] = "R_Stat";
                        for (int i = 0; i < 5; i++)
                        {
                            ty[i] = "L_Pick" + (i + 1);
                            ty[i + 5] = "L_Ban1" + (i + 1);
                            ty[i+10] = "R_Pick" + (i + 1);
                            ty[i + 15] = "R_Ban1" + (i + 1);

                            inputsource[i] = info[i+3];
                            inputsource[i+5] = info[i + 8];
                            inputsource[i+10] = info[i + 13];
                            inputsource[i+15] = info[i + 18];
                        }
                        teaml = sqldb.getdata("logo", "teamdatabase", "teamname", info[1]);
                        teamr = sqldb.getdata("logo", "teamdatabase", "teamname", info[2]);
                        inputsource[20] = teaml[0];
                        inputsource[22] = teamr[0];
                        if (info[23] == "win")
                        {
                            inputsource[21] = "WIN";
                            inputsource[23] = "LOSS";

                        }
                        else
                        {
                            inputsource[21] = "LOSS";
                            inputsource[23] = "WIN";
                        }
                        if (test_channel)
                        {
                            analyzed_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            analyzed_butt.Textcolor = Color.White;
                            test_on[0] = true;
                        }
                        else
                        {
                            analyzed_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            analyzed_butt.Textcolor = Color.White;
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
    }
}
