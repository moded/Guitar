using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class firstUsage : Page
    {
        int msgWndw;

        public firstUsage()
        {
            this.InitializeComponent();
            msgWndw = 1;
            msgHdrImg.Visibility = Visibility.Visible;
            msgHeader.Visibility = Visibility.Visible;
            msgImgHor.Visibility = Visibility.Visible;
            msgImgVer.Visibility = Visibility.Collapsed;
            msgRac.Visibility = Visibility.Visible;
            msgNextButton.Visibility = Visibility.Visible;
            el1.Visibility = Visibility.Visible;
            el2.Visibility = Visibility.Visible;
            el3.Visibility = Visibility.Visible;
            el4.Visibility = Visibility.Visible;
            el5.Visibility = Visibility.Visible;
            el6.Visibility = Visibility.Visible;
        }

        private void msgNextButton_Click(object sender, RoutedEventArgs e)
        {
            switch (msgWndw)
            {
                case 1:
                    msgHeader.Text = "click on the 'learn to play' button";
                    msgImgHor.Source = new BitmapImage(new Uri("ms-appx:///" + "Assets/learnToPlaylg.png", UriKind.Absolute));
                    msgNextButton.Content = "Next";
                    el2.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 2:
                    msgHeader.Text = "Select the first lesson";
                    msgImgHor.Visibility = Visibility.Collapsed;
                    msgImgVer.Source = new BitmapImage(new Uri("ms-appx:///" + "Assets/lessonsImg.png", UriKind.Absolute));
                    msgImgVer.Visibility = Visibility.Visible;
                    msgNextButton.Content = "Next";
                    el3.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 3:
                    msgHeader.Text = "You will see the chord you    need to press on";
                    msgImgVer.Source = new BitmapImage(new Uri("ms-appx:///" + "Assets/chordC.png", UriKind.Absolute));
                    msgNextButton.Content = "Next";
                    el3.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 4:
                    msgHeader.Text = "The right lights on the guitar will turn on";
                    msgImgVer.Visibility = Visibility.Collapsed;
                    msgImgHor.Visibility = Visibility.Visible;
                    msgImgHor.Source = new BitmapImage(new Uri("ms-appx:///" + "Assets/chordClights.jpg", UriKind.Absolute));
                    msgNextButton.Content = "Next";
                    el4.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 5:
                    msgHeader.Text = "Press the strings in the    lighted area";
                    msgImgHor.Source = new BitmapImage(new Uri("ms-appx:///" + "Assets/chordCfingers.jpg", UriKind.Absolute));
                    msgNextButton.Content = "Next";
                    el5.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 6:
                    msgHeader.Text = "Now you are ready to play\n\nGo On!!";
                    msgImgHor.Source = null;
                    msgNextButton.Content = "Finish";
                    el6.Fill = new SolidColorBrush(Colors.White);
                    msgWndw++;
                    break;
                case 7:
                    if (MainPage.firstUsage)
                    {
                        MainPage.firstUsage = false;
                        Frame.Navigate(typeof(welcomeMenu));
                    }
                    else
                    {
                        Frame.Navigate(typeof(messages));
                    }
                    break;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }


    }
}
