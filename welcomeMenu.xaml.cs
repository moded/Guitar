using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class welcomeMenu : Page
    {
        public welcomeMenu()
        {
            InitializeComponent();
            if (MainPage.firstUsage)
            {
                Frame.Navigate(typeof(firstUsage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            userName.Text = MainPage.userDetails.id;
            userImage.Source = new BitmapImage(new Uri("ms-appx:///"+MainPage.userDetails.pathToPic, UriKind.Absolute));
            if (MainPage.userDetails.unreadMsg > 0)
                unreadMsg.Text = MainPage.userDetails.unreadMsg.ToString();
            else
            {
                unreadMsg.Visibility = Visibility.Collapsed;
                mailCircle.Visibility = Visibility.Collapsed;
            }
            MainPage.BT.Connect();

            
         /*   while (!MainPage.BT.isConnected)
            {
                popUp("Could not connect to STAR. Check connectivity and try again");
                MainPage.BT.Connect();
            }
            */

        }
        private async void popUp(String str)
        {
            MessageDialog msgbox = new MessageDialog(str);
            await msgbox.ShowAsync();
        } 


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                MainPage.BT.terminate();
                e.Handled = true;
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void learnButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(chooseLesson));
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(settings));
        }

        private void playSongsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(songs));
        }

        private void effectsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(effects));
        }

        private void messageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(messages));
        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(record));
        }
    }
}
