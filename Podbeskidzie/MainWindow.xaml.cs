using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;

namespace Podbeskidzie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        DoubleAnimation BorderAnimation1;
        DoubleAnimation BorderAnimation2;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //animacja menu
            btn1.Opacity = 0;
            btn2.Opacity = 0;
            btn3.Opacity = 0;
            btn4.Opacity = 0;            

            DoubleAnimation NavAnimation = new DoubleAnimation();
            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.4);
            NavAnimation.From = 0;
            NavAnimation.To = 1;
            NavAnimation.Duration = TimeSpan.FromSeconds(0.5);

            btn1.BeginAnimation(OpacityProperty, NavAnimation);
            Container.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.6);
            btn2.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.8);
            btn3.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(1);
            btn4.BeginAnimation(OpacityProperty, NavAnimation);

            //animacje podkreślenia głównych przycisków
            BorderAnimation1 = new DoubleAnimation();
            BorderAnimation1.From = 104;
            BorderAnimation1.To = 116;
            BorderAnimation1.Duration = TimeSpan.FromSeconds(0.25);

            BorderAnimation2 = new DoubleAnimation();
            BorderAnimation2.From = 116;
            BorderAnimation2.To = 104;
            BorderAnimation2.Duration = TimeSpan.FromSeconds(0.25);
        }

        //Główne przyciski Menu
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel1.Visibility == Visibility.Hidden)
            {
                StackPanel1.Visibility = Visibility.Visible;
            }
            else if (StackPanel1.Visibility == Visibility.Visible)
            {
                StackPanel1.Visibility = Visibility.Hidden;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel2.Visibility == Visibility.Hidden)
            {
                StackPanel2.Visibility = Visibility.Visible;
            }
            else if (StackPanel2.Visibility == Visibility.Visible)
            {
                StackPanel2.Visibility = Visibility.Hidden;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel3.Visibility == Visibility.Hidden)
            {
                StackPanel3.Visibility = Visibility.Visible;
            }
            else if (StackPanel3.Visibility == Visibility.Visible)
            {
                StackPanel3.Visibility = Visibility.Hidden;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel4.Visibility == Visibility.Hidden)
            {
                StackPanel4.Visibility = Visibility.Visible;
            }
            else if (StackPanel4.Visibility == Visibility.Visible)
            {
                StackPanel4.Visibility = Visibility.Hidden;
            }
        }

        //MouseEnter
        private void btn1_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Visible;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle1.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Visible;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle2.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Visible;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle3.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn4_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Visible;
            Rectangle4.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        //MouseLeave
        private void btn1_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle1.BeginAnimation(WidthProperty, BorderAnimation2);
        }

        private void btn2_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle2.BeginAnimation(WidthProperty, BorderAnimation2);
        }

        private void btn3_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle3.BeginAnimation(WidthProperty, BorderAnimation2);
        }

        private void btn4_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle4.BeginAnimation(WidthProperty, BorderAnimation2);
        }

        //Lista rozwijana dodawanie
        private void btnDodaj1_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertDziennikarze();
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertRedakcje();
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertWolontariusze();
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertPracownicy();
            StackPanel1.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana Usuwanie
        private void btnUsun1_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteDziennikarze();
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteRedakcje();
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteWolontariusze();
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeletePracownicy();
            StackPanel2.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana Aktualizacja
        private void btnAktualizuj1_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateDziennikarze();
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateRedakcje();
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateWolontariusze();
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdatePracownicy();
            StackPanel3.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana wyświetlanie
        private void btnWyswietl1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWyswietl2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWyswietl3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWyswietl4_Click(object sender, RoutedEventArgs e)
        {

        }

        //Wyszukiwanie
        private void ComboBoxTabela_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxWartosc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
        }

        private void ZamknijButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MiniBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
