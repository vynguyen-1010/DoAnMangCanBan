﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTermProject
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        public Socket client;
        Socket listenerSocket;
        public IPAddress ipServer;
        public int portServer;
        string result = "";
        string desiredURL = "";
        public bool isURLExist = true;
        public TCPClient connectedClient;

        void CheckAndOpenURL()
        {
            while (client.Connected)
            {
                Byte[] recvData = new byte[1024 * 5000];
                try
                {
                    client.Receive(recvData);
                }
                catch
                {
                    return;
                }
                desiredURL = DeserializeMessage(recvData);
                if (desiredURL == "")
                    return;
                try
                {
                    Process requestURL = new Process();
                    requestURL.StartInfo.FileName = desiredURL;
                    
                    requestURL.Start();

                    isURLExist = CheckIfURLExist(desiredURL);
                    if (isURLExist)
                    {
                        result = "Yêu cầu thành công";
                        connectedClient.isURLExist = true;
                    }
                    else
                    {
                        result = "Yêu cầu thất bại";
                        connectedClient.isURLExist = false;
                    }
                    client.Send(SerializeMessage(result));
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Something went wrong\n" + ex.Message, 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
            }
        }

        bool CheckIfURLExist(string url)
        {
            bool isExist = true;
            HttpWebRequest clientRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            clientRequest.Timeout = 5000;
            clientRequest.Method = "HEAD";
            try
            {
                using (HttpWebResponse webResponse = clientRequest.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)webResponse.StatusCode;
                    if (statusCode >= 100 && statusCode < 400)
                    {
                        isExist = true;
                    }
                    else isExist = false;
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("Something went wrong\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isExist = false;
            }


            return isExist;
        }

        private byte[] SerializeMessage(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(ms, obj);
            return ms.ToArray();
        }

        private string DeserializeMessage(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                return (string)binaryFormatter.Deserialize(ms);
            }
            catch
            {
                return "";
            }
        }

        private void StartUnsafeThread()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipepServer = new IPEndPoint(ipServer,int.Parse(portBox.Text));
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(ipepServer);
            Thread listenClient = new Thread(() =>
            {
                while (true)
                {
                    listenerSocket.Listen(100);
                    client = listenerSocket.Accept();

                    Thread openRequestedURL = new Thread(CheckAndOpenURL);
                    openRequestedURL.IsBackground = true;
                    openRequestedURL.Start();
                }
            });
            listenClient.IsBackground = true;
            listenClient.Start();
        }

        private void tcpserver_Click(object sender, EventArgs e)
        {
            bool isPortValid = int.TryParse(portBox.Text, out int port);
            bool isIPValid = System.Text.RegularExpressions.Regex.IsMatch(ipaddBox.Text, "[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.");
            if (isPortValid != false && isIPValid != false)
            {
                ipServer = IPAddress.Parse(this.ipaddBox.Text);
                this.portServer = int.Parse(portBox.Text);
                Thread serverThread = new Thread(StartUnsafeThread);
                serverThread.Start();
                tcpserver.Enabled = false;
                tcpclient.Visible = true;
            }
            else
            {
                if (isIPValid == false)
                {
                    MessageBox.Show("Please enter IP address according to pattern A.B.C.D with A,B,C,D range from 0 to 255!!!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ipaddBox.Focus();
                }
                if (isPortValid == false)
                {
                    MessageBox.Show("Please enter port is integer!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    portBox.Focus();
                }               
            }

        }

        private void tcpclient_Click(object sender, EventArgs e)
        {
            TCPClient tCPClient = new TCPClient();
            tCPClient.dboard = this;
            tCPClient.Show();
            this.connectedClient = tCPClient;
        }
    }
}
