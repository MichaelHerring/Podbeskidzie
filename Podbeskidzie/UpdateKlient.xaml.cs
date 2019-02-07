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
    /// Interaction logic for UpdateKlient.xaml
    /// </summary>
    public partial class UpdateKlient : Page
    {
        SqlConnection connection;

        string query = "select * from Klient where IDKlienta = @ID";
        string update;
        SqlCommand command;
        SqlCommand updateCommand;
        SqlDataReader reader;

        //delegat i zdarzenie do przekazywania wiadomości
        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public UpdateKlient()
        {
            InitializeComponent();
        }

        public UpdateKlient(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
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
                    tB3.Text = reader.GetDateTime(3).ToString();
                    tB4.Text = reader.GetString(4);
                    tB5.Text = reader.GetString(5);
                    tB6.Text = reader.GetString(6);
                    reader.Close(); //zamknięcie readera
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
                update = "update Klient set Imie = @imie, Nazwisko = @nazwisko, Data_Urodzenia = @data, Telefon = @telefon, Miasto = @miasto, Kraj = @kraj where IDKlienta = @ID";
                updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@ID", tB0.Text);
                updateCommand.Parameters.AddWithValue("@imie", tB1.Text);
                updateCommand.Parameters.AddWithValue("@nazwisko", tB2.Text);
                updateCommand.Parameters.AddWithValue("@data", tB3.Text);
                updateCommand.Parameters.AddWithValue("@telefon", tB4.Text);
                updateCommand.Parameters.AddWithValue("@miasto", tB5.Text);
                updateCommand.Parameters.AddWithValue("@kraj", tB6.Text);
                if (tB0.Text != String.Empty)
                {
                    updateCommand.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {tB0.Text} w tabeli Klient.");
                }
                else
                {
                    wyslaneInfo("Wprowadź ID");
                }
            }
            catch(Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void tB0_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }
    }
}
