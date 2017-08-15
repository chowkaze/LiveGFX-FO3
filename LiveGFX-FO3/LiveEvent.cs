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
    }
}
