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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class signUp : Page
    {
        private String picPath;

        public signUp()
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

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != "" && passwordBox.Password !="" && confirmPass.Password != "")
            {
                if (passwordBox.Password != confirmPass.Password)
                {
                    PicText.Text = "Password entered are different!";
                    PicText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    return;
                }
                Credential item = new Credential
                {
                    id = NameTextBox.Text,
                    Password = passwordBox.Password,
                    pathToPic = picPath,
                    lastLesson = 1,
                    unreadMsg = 3
                };
                MainPage.userDetails = item;
                MainPage.generateCredential(item);
                MainPage.firstUsage = true;
                Frame.Navigate(typeof(firstUsage));
                //App.MobileService.
            } else
            {
                //picTextBox.Text="fill all fields!";
               // picTextBox.Foreground= Bru.gr;
                PicText.Text = "fill all fields!";
                PicText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                // PicText.Text.Replace(PicText.Text,"please fill all requierd fields!");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void galleryButton_Click(object sender, RoutedEventArgs e)
        {
            //App.MobileService.GetTable<Credential>()
            iconSelectVisibity(true);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/duck.gif");
        }

        private void button_Copy0_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/bugsBunny.png");
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/lola2.png");
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/looney16.gif");
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/porky_Pig.png");
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/Roadrunner.png");
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/Sambia.gif");
        }

        private void button_Copy6_Click(object sender, RoutedEventArgs e)
        {
            setProfileImage("Assets/loonyToons/tazzT.jpg");
        }

        private void setProfileImage(String imgPath)
        {
            picPath = imgPath;
            iconSelectVisibity(false);
        }

        private void iconSelectVisibity(bool isVisiable)
        {
            if (isVisiable)
            {
                Rack.Visibility = Visibility.Visible;
                button_Copy.Visibility = Visibility.Visible;
                button_Copy0.Visibility = Visibility.Visible;
                button_Copy1.Visibility = Visibility.Visible;
                button_Copy2.Visibility = Visibility.Visible;
                button_Copy3.Visibility = Visibility.Visible;
                button_Copy4.Visibility = Visibility.Visible;
                button_Copy5.Visibility = Visibility.Visible;
                button_Copy6.Visibility = Visibility.Visible;
            }
            else
            {
                Rack.Visibility = Visibility.Collapsed;
                button_Copy.Visibility = Visibility.Collapsed;
                button_Copy0.Visibility = Visibility.Collapsed;
                button_Copy1.Visibility = Visibility.Collapsed;
                button_Copy2.Visibility = Visibility.Collapsed;
                button_Copy3.Visibility = Visibility.Collapsed;
                button_Copy4.Visibility = Visibility.Collapsed;
                button_Copy5.Visibility = Visibility.Collapsed;
                button_Copy6.Visibility = Visibility.Collapsed;
            }
        }
    }
}
