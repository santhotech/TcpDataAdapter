﻿using System;
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
                count++;                
                sndExistData(client);
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

    }
}