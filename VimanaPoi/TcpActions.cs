using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace VimanaPoi
{
    class TcpActions
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private ArrayList clients;
        private int count;

        public delegate void ClientCountChangedEventHandler(int clientCount);
        public event ClientCountChangedEventHandler ClientCountChanged;
        
        public int ClientCount
        {
            get { return this.count; }
            set
            {
                this.count = value;
                if (ClientCountChanged != null)
                {
                    this.ClientCountChanged(count);
                }
            }
        }

        public TcpActions()
        {            
            int port = Properties.Settings.Default.port;
            this.tcpListener = new TcpListener(IPAddress.Any, port);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            clients = new ArrayList();            
            this.listenThread.Start();
        }

        //method that actually listens for calls from the clients
        private void ListenForClients()
        {
            this.tcpListener.Start();
            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();                
                clients.Add(client);
                ClientCount++;                                
                Thread a = new Thread(new ParameterizedThreadStart(CheckClientActive));
                a.Start(client);
            }
        }

        //Handles the data sending to the various clients that are connected.
        private void sndData(string cmdTxt, int setFlg)
        {            
            int a = clients.Count;            
            try
            {
                foreach (object ob in clients)
                {
                    try
                    {
                        TcpClient tcpClient = (TcpClient)ob;
                        NetworkStream clientStream = tcpClient.GetStream();
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] buffer = encoder.GetBytes(cmdTxt);
                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex + "j");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "p2");
            }
        }

        public void CheckClientActive(object ob)
        {
            TcpClient tcpClient = (TcpClient)ob;
            NetworkStream clientStream = tcpClient.GetStream();
            while (true)
            {
                try
                {
                    byte[] message = new byte[4096];
                    int bytesRead;
                    bytesRead = 0;
                    try
                    {
                        //blocks until a client sends a message   
                        bytesRead = clientStream.Read(message, 0, 4096);
                    }
                    catch
                    {
                        //a socket error has occured
                        //break;

                    }
                    if (bytesRead == 0)
                    {
                        //the client has disconnected from the server                        
                        --ClientCount;                        
                        clients.Remove(ob);
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Thread.Sleep(2000);
            }
        }

    }
}
