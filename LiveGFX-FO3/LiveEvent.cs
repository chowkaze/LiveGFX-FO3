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

        private void l3_long1_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[3];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[3];
                }
                if (check_on)
                {
                    l3_long1_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_long1_butt.Textcolor = Color.Black;
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
                    string select = this.l3_long1.GetItemText(this.l3_long1.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird_long", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_long1_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_long1_butt.Textcolor = Color.White;
                            test_on[3] = true;
                        }
                        else
                        {
                            l3_long1_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_long1_butt.Textcolor = Color.White;
                            button_on[3] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/l3_long");
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

        private void l3_long2_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[4];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[4];
                }
                if (check_on)
                {
                    l3_long2_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_long2_butt.Textcolor = Color.Black;
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
                    string select = this.l3_long2.GetItemText(this.l3_long2.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird_long", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_long2_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_long2_butt.Textcolor = Color.White;
                            test_on[4] = true;
                        }
                        else
                        {
                            l3_long2_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_long2_butt.Textcolor = Color.White;
                            button_on[4] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/l3_long");
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

        private void l3_long3_butt_Click(object sender, EventArgs e)
        {
            if (caspar_.IsConnected)
            {
                string[] ty = new string[20];
                string[] inputsource = new string[20];
                int layer = 15;
                int channal = 1;
                bool check_on = button_on[5];
                List<string> info = new List<string>();
                if (test_channel)
                {
                    channal = 2;
                    check_on = test_on[5];
                }
                if (check_on)
                {
                    l3_long3_butt.Normalcolor = Color.FromArgb(183, 183, 183);
                    l3_long3_butt.Textcolor = Color.Black;
                    stopcg(layer, channal);
                    if (test_channel)
                    {
                        test_on[5] = false;
                    }
                    else
                    {
                        button_on[5] = false;
                    }
                }
                else
                {
                    string select = this.l3_long3.GetItemText(this.l3_long3.SelectedItem);
                    try
                    {
                        info = sqldb.getdata("description", "lowerthird_long", "title", select);
                        ty[0] = "title";
                        ty[1] = "desc";
                        inputsource[0] = select;
                        inputsource[1] = info[0];
                        if (test_channel)
                        {
                            l3_long3_butt.Normalcolor = Color.FromArgb(65, 211, 253);
                            l3_long3_butt.Textcolor = Color.White;
                            test_on[5] = true;
                        }
                        else
                        {
                            l3_long3_butt.Normalcolor = Color.FromArgb(57, 181, 74);
                            l3_long3_butt.Textcolor = Color.White;
                            button_on[5] = true;
                        }
                        runcg(ty, inputsource, layer, channal, "GPL/l3_long");
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
    }
}
