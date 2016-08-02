using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static public bool firstUsage;
        static public DataWriter wr;
        static public DataReader rd;

        static public Bluetooth BT;

        public MainPage()
        {
            this.InitializeComponent();
            BT = new Bluetooth();
            

            firstUsage = false;
            this.NavigationCacheMode = NavigationCacheMode.Required;
           // AppToDevice();
        }

        private async void popUp(String str)
        {
            MessageDialog msgbox = new MessageDialog(str);
            await msgbox.ShowAsync();
        } 

        //private static MobileServiceCollection<Credential> items;

        static public Credential userDetails;

        public static async void generateCredential(Credential c)
        {           
            try
            {
                await App.MobileService.GetTable<Credential>().InsertAsync(c);

            }
            catch (MobileServiceInvalidOperationException n)
            {
                Debug.WriteLine(n);
            }
            try
            {
                //items.Add(item);
            }
            catch (Exception r)
            {
                Debug.WriteLine(r);
            }
        }

        public static async Task<Credential> fetchCred(String userName)
        {

            MainPage.userDetails = await App.MobileService.GetTable<Credential>().LookupAsync(userName);
            return MainPage.userDetails;
        }

        public static async Task<Credential> updateCred(Credential c)
        {
            //USAGE: To generate a new Credential with the fields to update, and fill the other
            //fields with the old details, found in MainPage.userDetails.
            MainPage.userDetails = c;
            await App.MobileService.GetTable<Credential>().UpdateAsync(c);
            return MainPage.userDetails;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(signIn)); 
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(signUp));
        }

        private async void AppToDevice()
        {

            // Configure PeerFinder to search for all paired devices.
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    Debug.WriteLine("No paired devices were found.");
                }
                else
                {
                    // Select a paired device. In this example, just pick the first one.
                    PeerInformation selectedDevice = pairedDevices[1];
                    // Attempt a connection
                    StreamSocket socket = new StreamSocket();
                    // Make sure ID_CAP_NETWORKING is enabled in your WMAppManifest.xml, or the next 
                    // line will throw an Access Denied exception.
                    // In this example, the second parameter of the call to ConnectAsync() is the RFCOMM port number, and can range 
                    // in value from 1 to 30.
                    await socket.ConnectAsync(selectedDevice.HostName, "1");
                    //DoSomethingUseful(socket);
                    //string a = "hello";
                    //socket = a;
                  /*  wr = new DataWriter(socket.OutputStream);
                    rd = new DataReader(socket.InputStream);
                    wr.WriteString("whats your name my name is ernew");
                    */
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
            
        }
    }
}
