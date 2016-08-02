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
using System.Threading;
using Windows.UI.Popups;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Guitar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class chooseLesson : Page
    {
        public static lessonDB DB= new lessonDB();
        public static int lastLesson;

        public chooseLesson()
        {
            this.InitializeComponent();

            textBox.IsEnabled = false;
            textBox_Copy.IsEnabled = false;
            textBox_Copy1.IsEnabled = false;
            textBox_Copy2.IsEnabled = false;
            textBox_Copy3.IsEnabled = false;
            textBox_Copy4.IsEnabled = false;
            try
            {
                DB.addLesson(1, new singleLesson("chords", new List<Chord> {Chord.D, Chord.Am, Chord.C , Chord.D}));
                DB.addLesson(2, new singleLesson("chords", new List<Chord> { Chord.G, Chord.C, Chord.A }));
                DB.addLesson(3, new singleLesson("chords", new List<Chord> { Chord.Em, Chord.Am, Chord.D, Chord.G, Chord.C, Chord.E }));
                DB.addLesson(4, new singleLesson("song", "Orot- Avraham Tal", new List<Chord> { Chord.Am, Chord.C, Chord.D, Chord.F }, new List<double> { 2, 2, 2, 2 }));
                DB.addLesson(5, new singleLesson("chords", new List<Chord> { Chord.F, Chord.Dm }));
                DB.addLesson(6, new singleLesson("song", "House of Rising Sun- Animals", new List<Chord> { Chord.Am, Chord.C, Chord.D, Chord.F }, new List<double> { 2, 2, 2, 2 }));
                // popUp("good!!");
            }
            catch (Exception)
            {
                //popUp("Error!!");
            }

        }

        private async void popUp(String str)
        {

            //Creating instance for the MessageDialog Class  
            //and passing the message in it's Constructor  
            MessageDialog msgbox = new MessageDialog(str);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
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

        private async void updateLessonNumAndNav(int lessonNum)
        {
            MainPage.userDetails.lastLesson = lessonNum;
            Credential c = MainPage.userDetails;
            await MainPage.updateCred(c);
            //Debug.WriteLine();
            Frame.Navigate(typeof(lesson));
        }


        private void lessonButton_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(1);
        }

        private void lesson1Button_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(2);

        }
        private void lesson2Button_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(3);

        }
        private void lesson3Button_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(4);

        }
        private void lesson4Button_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(5);

        }
        private void lesson5Button_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(6);

        }

        private void chooseLessonButton_Click(object sender, RoutedEventArgs e)
        {
            updateLessonNumAndNav(MainPage.userDetails.lastLesson);
        }

    }
}
