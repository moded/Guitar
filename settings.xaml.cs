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
using System.Windows;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class settings : Page
    {
        public settings()
        {
            this.InitializeComponent();
            ComboBoxItem newItem = new ComboBoxItem();
            newItem.Content = "Option a";
            backgroundComboBox.Items.Add(newItem);
            ComboBoxItem newItem2 = new ComboBoxItem();
            newItem2.Content = "Option b";
            backgroundComboBox.Items.Add(newItem2);
            ComboBoxItem newItemLed = new ComboBoxItem();
            newItemLed.Content = "Ocean";
            ledTehmeComboBox.Items.Add(newItemLed);
            ComboBoxItem newItemLed2 = new ComboBoxItem();
            newItemLed2.Content = "Fire";
            ledTehmeComboBox.Items.Add(newItemLed2);
            updateButton.IsEnabled = false;
            ledThemeButton.IsEnabled = false;
            backgroundButton.IsEnabled = false;
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

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Credential c = new Credential();
            c.id = MainPage.userDetails.id;
            if (passwordBox.Password != "")
                c.Password = passwordBox.Password;
            else c.Password = MainPage.userDetails.Password;
            c.pathToPic = "pathToPic";
            c.lastLesson = MainPage.userDetails.lastLesson;
            c.unreadMsg = MainPage.userDetails.unreadMsg;
            await MainPage.updateCred(c);
            MainPage.userDetails = c;
            passwordBox.Password = "";
            updateButton.IsEnabled = false;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            updateButton.IsEnabled = true;
            
        }


        private void backgroundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {       
            backgroundButton.IsEnabled = true;
        }

        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundButton.IsEnabled = false;
        }

        private void ledThemeButton_Click(object sender, RoutedEventArgs e)
        {
            ledThemeButton.IsEnabled = false;
        }

        private void ledTehmeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ledThemeButton.IsEnabled = true;
        }
    }
}
