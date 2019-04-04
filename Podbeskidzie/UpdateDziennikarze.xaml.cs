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
using System.Data.SqlClient;

namespace Podbeskidzie
{
    /// <summary>
    /// Interaction logic for UpdateDziennikarze.xaml
    /// </summary>
    public partial class UpdateDziennikarze : Page
    {
        SqlConnection connection;

        string query = "select * from Dziennikarze where ID_Dziennikarza = @ID";
        string update = @"update Dziennikarze 
                        set Imie = @imie, 
                        Nazwisko = @nazwisko, 
                        ID_Redakcji = @redakcja, 
                        Rodzaj = @rodzaj, 
                        Telefon = @telefon, 
                        Email = @email
                        where ID_Dziennikarza = @ID";
        SqlCommand command;
        SqlCommand updateCommand;
        SqlDataReader reader;

        string fillID = "select ID_Redakcji, Nazwa from Redakcje order by ID_Redakcji";
        SqlCommand command1;
        SqlDataReader reader1;

        //delegat i zdarzenie do przekazywania wiadomości
        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public UpdateDziennikarze()
        {
            InitializeComponent();
        }

        public UpdateDziennikarze(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ładowanie id redakcji i nazw do comboboxa
            try
            {
                try
                {
                    command1 = new SqlCommand(fillID, connection);
                    reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        tB3.Items.Add(reader1.GetInt16(0) + $" ({reader1.GetString(1)})");
                    }
                    reader1.Close();
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
                finally
                {
                    reader1.Close();
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn2.IsEnabled = true;
            try //jeśli nie ma połączenia wykonuje sie finally, gdzie zamykany jest reader, który nie został utworzony, dlatego potrzebny jeszcze jeden try catch
            {
                try
                {
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", tB0.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    tB1.Text = reader.GetString(1);
                    tB2.Text = reader.GetString(2);
                    tB3.Text = reader.GetInt16(3).ToString();
                    tB4.Text = reader.GetString(4);
                    tB5.Text = reader.GetString(5);
                    tB6.Text = reader.GetString(6);
                    reader.Close(); //zamknięcie readera
                    tB1.IsEnabled = true;
                    tB2.IsEnabled = true;
                    tB3.IsEnabled = true;
                    tB4.IsEnabled = true;
                    tB5.IsEnabled = true;
                    tB6.IsEnabled = true;

                }
                catch (Exception exc)
                {
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    tB1.Text = "";
                    tB2.Text = "";
                    tB3.Text = "";
                    tB4.Text = "";
                    tB5.Text = "";
                    tB6.Text = "";
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        reader.Close(); //zamknięcie readera jeśli wystąpi błąd
                    }
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string trimmedID = tB3.Text;
                for (int i = 0; i < tB3.Text.Length - 1; i++) //usuwanie nazwy redakcji, aby w poleceniu zostało tylko ID
                {
                    if (tB3.Text[i] == ' ')
                    {
                        trimmedID = tB3.Text.Remove(i); //usunięcie wszystkich znaków po pierwszej spacji (zostaje tylko ID)
                        break;
                    }
                }
                updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@ID", tB0.Text);
                updateCommand.Parameters.AddWithValue("@imie", tB1.Text);
                updateCommand.Parameters.AddWithValue("@nazwisko", tB2.Text);
                updateCommand.Parameters.AddWithValue("@redakcja", trimmedID);
                updateCommand.Parameters.AddWithValue("@rodzaj", tB4.Text);
                updateCommand.Parameters.AddWithValue("@telefon", tB5.Text);
                updateCommand.Parameters.AddWithValue("@email", tB6.Text);
                if (tB0.Text != String.Empty)
                {
                    updateCommand.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {tB0.Text} w tabeli Dziennikarze.");
                }
                else
                {
                    wyslaneInfo("Wprowadź ID");
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void tB0_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }

        private void tB0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn1_Click(this, new RoutedEventArgs());
        }
    }
}
