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
    /// Interaction logic for InsertDziennikarze.xaml
    /// </summary>
    public partial class InsertDziennikarze : Page
    {
        SqlConnection connection;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        string query = "insert into Dziennikarze values(@imie, @nazwisko, @redakcja, @rodzaj, @telefon, @email)";
        SqlCommand command;

        string fillID = "select ID_Redakcji, Nazwa from Redakcje order by ID_Redakcji";
        SqlCommand command1;
        SqlDataReader reader;

        public InsertDziennikarze()
        {
            InitializeComponent();
        }

        public InsertDziennikarze(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    command1 = new SqlCommand(fillID, connection);
                    reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        tB3.Items.Add(reader.GetInt16(0) + $" ({reader.GetString(1)})");
                    }
                    reader.Close();
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void Insert() //metoda do dodawania danych
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
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@imie", tB1.Text);
                command.Parameters.AddWithValue("@nazwisko", tB2.Text);
                command.Parameters.AddWithValue("@redakcja", trimmedID);
                command.Parameters.AddWithValue("@rodzaj", tB4.Text);
                command.Parameters.AddWithValue("@telefon", tB5.Text);
                command.Parameters.AddWithValue("@email", tB6.Text);

                command.ExecuteNonQuery();
                wyslaneInfo("Dodano rekord do tabeli Dziennikarze.");
                tB1.Text = "";
                tB2.Text = "";
                tB3.Text = "";
                tB4.Text = "";
                tB5.Text = "";
                tB6.Text = "";
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            
            if (tB1.Text == "" || tB2.Text == "" || tB3.Text == "" || tB4.Text == "")
            {
                wyslaneInfo("Wypełnij wymagane pola: Imię, Nazwisko, ID Redakcji, Typ.");
            }
            else if (tB5.Text == "" || tB6.Text == "")
            {
                if (MessageBox.Show("Czy na pewno chcesz zostawić puste pola?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Insert();
                }
                else
                {
                    wyslaneInfo("Anulowano dodawanie danych.");
                }
            }
            else
            {
                Insert();
            }
        }
    }
}
