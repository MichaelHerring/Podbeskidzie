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
        DoubleAnimation BorderAnimation1;
        DoubleAnimation BorderAnimation2;
        DoubleAnimation DropDownAnimation;
        DoubleAnimation DropDownOpacityAnimation;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            InsertDziennikarze.wyslaneInfo += WyswietlKomunikat;
            InsertRedakcje.wyslaneInfo += WyswietlKomunikat;
            InsertPracownicy.wyslaneInfo += WyswietlKomunikat;
            InsertWolontariusze.wyslaneInfo += WyswietlKomunikat;
            InsertDzialy.wyslaneInfo += WyswietlKomunikat;
            DeleteDziennikarze.wyslaneInfo += WyswietlKomunikat;
            DeleteRedakcje.wyslaneInfo += WyswietlKomunikat;
            DeletePracownicy.wyslaneInfo += WyswietlKomunikat;
            DeleteWolontariusze.wyslaneInfo += WyswietlKomunikat;
            DeleteDzialy.wyslaneInfo += WyswietlKomunikat;
            UpdateDziennikarze.wyslaneInfo += WyswietlKomunikat;
            UpdateRedakcje.wyslaneInfo += WyswietlKomunikat;
            UpdatePracownicy.wyslaneInfo += WyswietlKomunikat;
            UpdateWolontariusze.wyslaneInfo += WyswietlKomunikat;
            UpdateDzialy.wyslaneInfo += WyswietlKomunikat;
            ShowTable.wyslaneInfo += WyswietlKomunikat;
            WynikiWyszukiwania.wyslaneInfo += WyswietlKomunikat;
        }

        void WyswietlKomunikat(string komunikat)
        {
            MessageViewer.Content += komunikat + "\n";
            MessageViewer.ScrollToEnd();

            //animacja żarówki
            DoubleAnimation FadeOut = new DoubleAnimation();
            FadeOut.From = 1;
            FadeOut.To = 0;
            FadeOut.Duration = TimeSpan.FromSeconds(0.2);

            RepeatBehavior Repeat = new RepeatBehavior(8.0);
            FadeOut.RepeatBehavior = Repeat;
            ImageBulbBlack.BeginAnimation(OpacityProperty, FadeOut);
            System.Media.SystemSounds.Beep.Play();
        }

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

            DropDownAnimation = new DoubleAnimation();
            DropDownAnimation.From = 0;
            DropDownAnimation.To = 154;
            DropDownAnimation.EasingFunction = new CubicEase();
            DropDownAnimation.Duration = TimeSpan.FromSeconds(0.2);

            DropDownOpacityAnimation = new DoubleAnimation();
            DropDownOpacityAnimation.From = 0;
            DropDownOpacityAnimation.To = 1;
            DropDownOpacityAnimation.EasingFunction = new CubicEase();
            DropDownOpacityAnimation.Duration = TimeSpan.FromSeconds(0.2);
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
            if (StackPanel1.Visibility == Visibility.Hidden)
            {
                StackPanel1.BeginAnimation(HeightProperty, DropDownAnimation);
                StackPanel1.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
                StackPanel1.Effect.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
            }
            StackPanel1.Visibility = Visibility.Visible;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle1.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (StackPanel2.Visibility == Visibility.Hidden)
            {
                StackPanel2.BeginAnimation(HeightProperty, DropDownAnimation);
                StackPanel2.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
                StackPanel2.Effect.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
            }
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Visible;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle2.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (StackPanel3.Visibility == Visibility.Hidden)
            {
                StackPanel3.BeginAnimation(HeightProperty, DropDownAnimation);
                StackPanel3.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
                StackPanel3.Effect.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
            }
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Visible;
            StackPanel4.Visibility = Visibility.Hidden;
            Rectangle3.BeginAnimation(WidthProperty, BorderAnimation1);
        }

        private void btn4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (StackPanel4.Visibility == Visibility.Hidden)
            {
                StackPanel4.BeginAnimation(HeightProperty, DropDownAnimation);
                StackPanel4.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
                StackPanel4.Effect.BeginAnimation(OpacityProperty, DropDownOpacityAnimation);
            }
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
            Container.Content = new InsertDziennikarze(connection);
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertRedakcje(connection);
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertWolontariusze(connection);
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertPracownicy(connection);
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void btnDodaj5_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertDzialy(connection);
            StackPanel1.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana Usuwanie
        private void btnUsun1_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteDziennikarze(connection);
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteRedakcje(connection);
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteWolontariusze(connection);
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeletePracownicy(connection);
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void btnUsun5_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteDzialy(connection);
            StackPanel2.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana Aktualizacja
        private void btnAktualizuj1_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateDziennikarze(connection);
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj2_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateRedakcje(connection);
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj3_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateWolontariusze(connection);
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj4_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdatePracownicy(connection);
            StackPanel3.Visibility = Visibility.Hidden;
        }

        private void btnAktualizuj5_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateDzialy(connection);
            StackPanel3.Visibility = Visibility.Hidden;
        }

        //Lista rozwijana wyświetlanie
        private void btnWyswietl1_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Dziennikarze");
            StackPanel4.Visibility = Visibility.Hidden;
        }

        private void btnWyswietl2_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Redakcje");
            StackPanel4.Visibility = Visibility.Hidden;
        }

        private void btnWyswietl3_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Wolontariusze");
            StackPanel4.Visibility = Visibility.Hidden;
        }

        private void btnWyswietl4_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Pracownicy");
            StackPanel4.Visibility = Visibility.Hidden;
        }

        private void btnWyswietl5_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Dzialy");
            StackPanel4.Visibility = Visibility.Hidden;
        }

        //Wyszukiwanie
        private void ComboBoxTabela_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //czyszczenie comboboxa z kolumnami
                ComboBoxKolumna.Items.Clear();
                //dodawanie nazw kolumn
                if (ComboBoxTabela.SelectedItem != null)
                {
                    string selectedTable = ComboBoxTabela.Text;
                    string query = $"select * from {selectedTable}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataColumn c in table.Columns)
                    {
                        ComboBoxKolumna.Items.Add(c.ToString());
                    }
                }
            }
            catch (Exception exc)
            {
                WyswietlKomunikat(exc.Message);
            }
        }

        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tabela = ComboBoxTabela.Text;
                string kolumna = ComboBoxKolumna.Text;
                string wartosc = TextBoxWartosc.Text;
                if (ComboBoxTabela.SelectedItem == null || ComboBoxKolumna.SelectedItem == null || wartosc == String.Empty)
                {
                    WyswietlKomunikat("Wybierz tabelę i kolumnę oraz wprowadź wartość.");
                }
                else
                {
                    string query;
                    if (tabela == "Dziennikarze")
                    {
                        query = $@"select d.ID_Dziennikarza as 'ID Dziennikarza', d.Imie, d.Nazwisko, d.ID_Redakcji as 'ID Redakcji', r.Nazwa as 'Nazwa Redakcji', d.Rodzaj, d.Telefon, d.Email
                        from Dziennikarze d join Redakcje r on d.ID_Redakcji = r.ID_Redakcji
                        where d.{kolumna} like '%{wartosc}%'";
                    }
                    else if (tabela == "Pracownicy")
                    {
                        query = $@"select p.ID_Pracownika as 'ID Pracownika', p.Imie, p.Nazwisko, p.Telefon_Pracownika as 'Telefon Pracownika', p.Email_Pracownika as 'Email Pracownika', p.Stanowisko, p.ID_Dzialu as 'ID Dzialu', d.Nazwa_Dzialu as 'Nazwa Dzialu'
                        from Pracownicy p join Dzialy d on p.ID_Dzialu = d.ID_Dzialu
                        where p.{kolumna} like '%{wartosc}%'";
                    }
                    else
                    {
                        query = $"select * from {tabela} where {kolumna} like '%{wartosc}%'";
                    }
                    Container2.Content = new WynikiWyszukiwania(query, connection);
                }
            }
            catch (Exception exc)
            {
                WyswietlKomunikat(exc.Message);
            }
        }

        private void TextBoxWartosc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnWyszukaj_Click(this, new RoutedEventArgs());
            }
        }

        //Działanie okna
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

        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void WindowBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove(); //"chwytanie okna i przesuwanie"
        }
    }
}
