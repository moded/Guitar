using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.UI.Popups;

namespace Guitar
{

    public delegate void messageReciever(object sender,String e);

    public class Bluetooth
    {
        #region Members
        private StreamSocket socket = new StreamSocket();
        private StreamWriter writer;
        private StreamReader reader;
        private bool listen;
        public bool isConnected;
        

        public event messageReciever messageRecieved;
        #endregion

        #region public methods
        public async void Connect()
        {
            listen = true;
            PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
            var pairedDevices = await PeerFinder.FindAllPeersAsync();

            if (pairedDevices.Count == 0)
            {
            }
            else
            {
                PeerInformation selectedDevice = pairedDevices[0]; // pick the first paired device

                try // 'try' used in the case the socket has already been connected
                {
                    
                    await socket.ConnectAsync(selectedDevice.HostName, "1");

                    writer = new StreamWriter(socket.OutputStream.AsStreamForWrite());
                    

                    reader = new StreamReader(socket.InputStream.AsStreamForRead());

                    isConnected = true;

                    doListen();
                }
                catch (Exception)
                {
                    isConnected = false;
                }
            }
        }

        public async void doListen()
        {
            while (listen)
            {
                try
                {
                    messageRecieved(this, await MainPage.BT.Read());   
                }
                catch(Exception)
                {

                }
            }
        }

        public void Write(String s)
        {
            try
            {
                writer.WriteAsync(s);
                writer.Flush();
            }
            catch(Exception)
            {
                popUp("Error - communication error accured");
            }
        }

        public void terminate()
        {
            listen = false;
            isConnected = false;
            writer.Dispose();
            reader.Dispose();
            socket.Dispose();
        }
        #endregion

        #region private methods
        private async Task<String> Read()
        {
            //char[] c = new char[3];
            String s = "";
            try
            {
                //await reader.ReadAsync(c, 0, 3);
                s = await reader.ReadLineAsync();
                //s = new String(c);
            }
            catch(Exception)
            {
                popUp("Error - communication error accured");
            }
            return s;
        }

        private async void popUp(String str)
        {
            MessageDialog msgbox = new MessageDialog(str);
            await msgbox.ShowAsync();
        }
        #endregion
    }
}
