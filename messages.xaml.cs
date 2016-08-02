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
using Windows.Phone.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
//using Microsoft.phone.tasks;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class messages : Page
    {
        bool txtMsgOn;
        bool playFlag;
        bool itemClicked;
        bool item2Clicked;
        bool item3Clicked;
        ListBoxItem item;
        ListBoxItem item2;
        ListBoxItem item3;

        public messages()
        {
            this.InitializeComponent();
            showMsg(false);
            txtMsgOn = false;
            itemClicked = false;
            item2Clicked = false;
            item3Clicked = false;
            playFlag = false;
            //vid messegs
            item = new ListBoxItem();
            item.Content = "First use in app";
            item.FontSize = 25; 
            item.Foreground= new SolidColorBrush(Windows.UI.Colors.Black);
            item2 = new ListBoxItem();
            item2.Content = "Several tips";
            item2.FontSize = 25;
            item2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            listBox.Items.Add(item);
            listBox.Items.Add(item2);
            //text messeges
            item3 = new ListBoxItem();
            item3.Content = "Welcome to STAR";
            item3.FontSize = 25;
            item3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            listBox2.Items.Add(item3);
            //double tapped handlers
            item.DoubleTapped += new DoubleTappedEventHandler(item_DoubleTapped);
            item2.DoubleTapped += new DoubleTappedEventHandler(item2_DoubleTapped);
            item3.DoubleTapped += new DoubleTappedEventHandler(item3_DoubleTapped);
            
        }

        private void item_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement.Play();
            playFlag = true;
            listBox.Visibility = Visibility.Collapsed;
            if (!itemClicked)
            {
                MainPage.userDetails.unreadMsg--;
                itemClicked = true;
                item.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        private async void item2_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement.Play();
            playFlag = true;
            listBox.Visibility = Visibility.Collapsed;
            if (!item2Clicked)
            {
                MainPage.userDetails.unreadMsg--;
                await MainPage.updateCred(MainPage.userDetails);
                item2Clicked = true;
                item2.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        private async void item3_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (!item3Clicked)
            {
                MainPage.userDetails.unreadMsg--;
                await MainPage.updateCred(MainPage.userDetails);
                item3Clicked = true;
                item3.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
            }
            txtMsgOn = true;
            listBox2.Visibility = Visibility.Collapsed;
            showMsg(true);


        }

        private void showMsg(bool show)
        {
            if (show)
            {
                msgBorder.Visibility = Visibility.Visible;
                msgButton.Visibility = Visibility.Visible;
                msgHdrTextBlock.Visibility = Visibility.Visible;
                msgRack.Visibility = Visibility.Visible;
                msgTextBlock.Visibility = Visibility.Visible;
            } else
            {
                msgBorder.Visibility = Visibility.Collapsed;
                msgButton.Visibility = Visibility.Collapsed;
                msgHdrTextBlock.Visibility = Visibility.Collapsed;
                msgRack.Visibility = Visibility.Collapsed;
                msgTextBlock.Visibility = Visibility.Collapsed;
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
                Frame.Navigate(typeof(welcomeMenu));
                }
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myMediaElement.Visibility = Visibility.Visible;
            myMediaElement.Play();
            playFlag = true;
            //yossi.Visibility = Visibility.Collapsed;
            /*myMediaElement.Source = new Uri("ms-appx:///" + "Assets/Frozen.wmv", UriKind.Absolute);
            myMediaElement.Play();*/
        }

        private void Media_Tapped(object sender, RoutedEventArgs e)
        {
            if (playFlag)
            {
                myMediaElement.Pause();
                playFlag = false;
            }
            else
            {
                myMediaElement.Play();
                playFlag = true;
            }
        }

        private void msgButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(firstUsage));
        }
    }
}
