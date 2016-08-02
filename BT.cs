﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Guitar;
namespace Guitar
{
    /// <summary>
    /// Class to control the bluetooth connection to the Arduino.
    /// </summary>
    public class ConnectionManager
    {
        public bool IsConnected { get; set; }
        /// <summary>
        /// Socket used to communicate with Arduino.
        /// </summary>
        private StreamSocket socket;

        /// <summary>
        /// DataWriter used to send commands easily.
        /// </summary>
        private DataWriter dataWriter;

        /// <summary>
        /// DataReader used to receive messages easily.
        /// </summary>
        private DataReader dataReader;

        /// <summary>
        /// Thread used to keep reading data from socket.
        /// </summary>
        private BackgroundWorker dataReadWorker;

        /// <summary>
        /// Delegate used by event handler.
        /// </summary>
        /// <param name="message">The message received.</param>
        public delegate void MessageReceivedHandler(string message);

        /// <summary>
        /// Event fired when a new message is received from Arduino.
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// Initialize the manager, should be called in OnNavigatedTo of main page.
        /// </summary>
        public void Initialize()
        {
            socket = new StreamSocket();
        //    EndpointPair pair = new EndpointPair()
            /*
            dataReadWorker = new BackgroundWorker();
            dataReadWorker.WorkerSupportsCancellation = true;
            dataReadWorker.DoWork += new DoWorkEventHandler(ReceiveMessages);*/
        }

        /// <summary>
        /// Finalize the connection manager, should be called in OnNavigatedFrom of main page.
        /// </summary>
        public void Terminate()
        {
            if (socket != null)
            {
                socket.Dispose();
            }
            if (dataReadWorker != null)
            {
                dataReadWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Connect to the given host device.
        /// </summary>
        /// <param name="deviceHostName">The host device name.</param>
        public async Task<bool> Connect(HostName deviceHostName)
        {
            if (socket != null)
            {
                await socket.ConnectAsync(deviceHostName, "1");
                dataReader = new DataReader(socket.InputStream);
                dataReadWorker.RunWorkerAsync();
                dataWriter = new DataWriter(socket.OutputStream);
                IsConnected = true;
                return true;
            }
            else
            {
                Debug.WriteLine("Couldn't connect to Device - Even that I've found the correct Name...");
                IsConnected = false;
                return false;
            }
        }

        /// <summary>
        /// Receive messages from the Arduino through bluetooth.
        /// </summary>
        private async void ReceiveMessages(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    // Read first byte (length of the subsequent message, 255 or less). 
                    uint sizeFieldCount = await dataReader.LoadAsync(1);
                    if (sizeFieldCount != 1)
                    {
                        // The underlying socket was closed before we were able to read the whole data. 
                        return;
                    }

                    // Read the message. 
                    uint messageLength = dataReader.ReadByte();
                    uint actualMessageLength = await dataReader.LoadAsync(messageLength);
                    if (messageLength != actualMessageLength)
                    {
                        // The underlying socket was closed before we were able to read the whole data. 
                        return;
                    }
                    // Read the message and process it.
                    string message = dataReader.ReadString(actualMessageLength);


                    MessageReceived(message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Send command to the Arduino through bluetooth.
        /// </summary>
        /// <param name="command">The sent command.</param>
        /// <returns>The number of bytes sent</returns>
        public async Task<uint> SendCommand(string command)
        {
            uint sentCommandSize = 0;
            if (dataWriter != null)
            {
                uint commandSize = dataWriter.MeasureString(command);
                dataWriter.WriteByte((byte)commandSize);
                sentCommandSize = dataWriter.WriteString(command);
                await dataWriter.StoreAsync();
            }
            return sentCommandSize;
        }
    }
}

