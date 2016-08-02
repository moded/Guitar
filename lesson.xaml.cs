
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
    public sealed partial class lesson : Page
    {
        static int lessonNumber;
        static singleLesson sl;
        static int i;
        static int lessonSize;

        public lesson()
        {
            this.InitializeComponent();
            MainPage.BT.messageRecieved += new messageReciever(handleMessage);
            init();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void init()
        {
            //MainPage.BT.Write("$B");
            this.Rack.Visibility = Visibility.Collapsed;
            this.finishedTextBox.Visibility = Visibility.Collapsed;
            this.finishedTextBox1.Visibility = Visibility.Collapsed;
            this.repeatButton.Visibility = Visibility.Collapsed;
            this.nextLesoonButton.Visibility = Visibility.Collapsed;
            this.lessonsButton.Visibility = Visibility.Collapsed;
            i = 0;
            lessonNumber = MainPage.userDetails.lastLesson;
            sl = chooseLesson.DB.getLesson(lessonNumber);
            if (sl.getType() == "chords")
            {
                chordBlock.Text = sl.getchordsList()[0].ToString();
                writeChord(sl.getchordsList()[0].ToString());
                lessonSize = sl.getchordsList().Count();
                nextButtun.Visibility = Visibility.Visible;
                prevButtun.Visibility = Visibility.Visible;
            }
            else    //its a song
            {
                showMenu();
            }
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
                Frame.Navigate(typeof(chooseLesson));
            }
        }

        private async void popUp(String str)
        { 
            MessageDialog msgbox = new MessageDialog(str);
            await msgbox.ShowAsync();
        } 

        private void nextButtun_Click(object sender, RoutedEventArgs e)
        {
            if (i < lessonSize-1)
            {
                ++i;
                chordBlock.Text = sl.getchordsList()[i].ToString();
                writeChord(sl.getchordsList()[i].ToString());
            }
            else {     //finished lesson
                MainPage.BT.Write("$B");
                this.chordBlock.Text = "";
                this.nextButtun.Visibility = Visibility.Collapsed;
                this.prevButtun.Visibility = Visibility.Collapsed;
                this.Rack.Visibility = Visibility.Visible;
                this.finishedTextBox.Visibility = Visibility.Visible;
                this.finishedTextBox1.Visibility = Visibility.Visible;
                this.repeatButton.Visibility = Visibility.Visible;
                this.nextLesoonButton.Visibility = Visibility.Visible;
                this.lessonsButton.Visibility = Visibility.Visible;
            }
        }

        private void prevButtun_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0)
            {
                i--;
                chordBlock.Text = sl.getchordsList()[i].ToString();

                writeChord(sl.getchordsList()[i].ToString());
            }
        }

        private void repeatButton_Click(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void nextLesoonButton_Click(object sender, RoutedEventArgs e)
        {
            if (lessonNumber < 6)
            {
                MainPage.userDetails.lastLesson++;
                init();
            } else
            {
                showMenu(true);
            }
            
        }

        private void lessonsButton_Click(object sender, RoutedEventArgs e)
        {
            init();
            Frame.Navigate(typeof(chooseLesson));
        }

        private void showMenu(bool lastOne= false)
        {
            this.chordBlock.Text = "";
            this.nextButtun.Visibility = Visibility.Collapsed;
            this.prevButtun.Visibility = Visibility.Collapsed;
            this.Rack.Visibility = Visibility.Visible;
            this.finishedTextBox.Visibility = Visibility.Visible;
            this.finishedTextBox1.Visibility = Visibility.Visible;
            this.lessonsButton.Visibility = Visibility.Visible;
            if (lastOne)
            {
                this.finishedTextBox.Text = "Congratulations!";
                this.finishedTextBox1.Text = "Now you are a\nSuper User!";
                this.repeatButton.Visibility = Visibility.Collapsed;
                this.nextLesoonButton.Visibility = Visibility.Collapsed;
            } else
            {
                this.repeatButton.Visibility = Visibility.Visible;
                this.nextLesoonButton.Visibility = Visibility.Visible;
            }
        }

        private void writeChord(String s)
        {
            switch (s)
            {
                case "A": MainPage.BT.Write("#A"); break;
                case "Am": MainPage.BT.Write("#a"); break;
                case "B": MainPage.BT.Write("#B"); break;
                case "Bm": MainPage.BT.Write("#b"); break;
                case "C": MainPage.BT.Write("#C"); break;
                case "D": MainPage.BT.Write("#D"); break;
                case "Dm": MainPage.BT.Write("#d"); break;
                case "E": MainPage.BT.Write("#E"); break;
                case "Em": MainPage.BT.Write("#e"); break;
                case "F": MainPage.BT.Write("#F"); break;
                case "Fm": MainPage.BT.Write("#f"); break;
                case "G": MainPage.BT.Write("#G"); break;
            }
        }

        public void handleMessage(object sender ,String str)
        {
            if (chordBlock.Text == sl.getchordsList()[i].ToString())
            {
                if (i < lessonSize - 1)
                {
                    ++i;
                    chordBlock.Text = sl.getchordsList()[i].ToString();
                    writeChord(sl.getchordsList()[i].ToString());
                }
                else
                {     //finished lesson
                    this.chordBlock.Text = "";
                    this.nextButtun.Visibility = Visibility.Collapsed;
                    this.prevButtun.Visibility = Visibility.Collapsed;
                    this.Rack.Visibility = Visibility.Visible;
                    this.finishedTextBox.Visibility = Visibility.Visible;
                    this.finishedTextBox1.Visibility = Visibility.Visible;
                    this.repeatButton.Visibility = Visibility.Visible;
                    this.nextLesoonButton.Visibility = Visibility.Visible;
                    this.lessonsButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (chordBlock.Text == sl.getchordsList()[i].ToString())
            {
                if (i < lessonSize - 1)
                {
                    ++i;
                    chordBlock.Text = sl.getchordsList()[i].ToString();
                    writeChord(sl.getchordsList()[i].ToString());
                }
                else
                {     //finished lesson
                    MainPage.BT.Write("$B");
                    this.chordBlock.Text = "";
                    this.nextButtun.Visibility = Visibility.Collapsed;
                    this.prevButtun.Visibility = Visibility.Collapsed;
                    this.Rack.Visibility = Visibility.Visible;
                    this.finishedTextBox.Visibility = Visibility.Visible;
                    this.finishedTextBox1.Visibility = Visibility.Visible;
                    this.repeatButton.Visibility = Visibility.Visible;
                    this.nextLesoonButton.Visibility = Visibility.Visible;
                    this.lessonsButton.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
