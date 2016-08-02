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
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class signIn : Page
    {
        public signIn()
        {
            this.InitializeComponent();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                Frame.Navigate(typeof(welcomeMenu));
            }
        }

        private async void button_Click_1(object sender, RoutedEventArgs e)
        {
            WarningText.Text = "Validating Data...";
            WarningText.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            WarningText.FontStyle = Windows.UI.Text.FontStyle.Italic;

            if (NameTextBox.Text != "" /*&& passwordBox.Password != ""*/)
            {
                try
                {
                    await MainPage.fetchCred(NameTextBox.Text);
                    if (MainPage.userDetails.Password == passwordBox.Password)
                    {
                        Frame.Navigate(typeof(welcomeMenu));
                    }
                    else
                    {
                        WarningText.Text = "Username or Password is wrong!";
                        WarningText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    }
                }
                catch(Exception a)
                {
                    Debug.WriteLine(a);
                }

            }else
            {
                WarningText.Text = "fill all fields!";
            }
        }

        private void animation_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }
    }
}
