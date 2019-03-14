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
    /// Interaction logic for UpdatePracownicy.xaml
    /// </summary>
    public partial class UpdatePracownicy : Page
    {
        SqlConnection connection;
        SqlCommand command, updatecomm,command1;

        

        string query = "select * from Pracownicy where ID_Pracownika like @ID";
        string update = @"update Pracownicy
                        set Imie = @imie, 
                        Nazwisko = @nazwisko, 
                        Telefon_Pracownika = @telefon, 
                        Email_Pracownika = @email,
                        Stanowisko = @stanowisko,
                        ID_Dzialu = @iddzialu
                        where ID_Pracownika = @ID";

        SqlDataReader reader;

        public delegate void WylijInfo(string komunikat);
        public static event WylijInfo wyslaneInfo;




        public UpdatePracownicy()
        {
            InitializeComponent();
        }
        public UpdatePracownicy(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

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
                    tB3.Text = reader.GetString(3);
                    tB4.Text = reader.GetString(4);
                    tB5.Text = reader.GetString(5);
                    tB6.Text = reader.GetInt16(6).ToString();
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
                updatecomm = new SqlCommand(update, connection);
                updatecomm.Parameters.AddWithValue("@ID", tB0.Text);
                updatecomm.Parameters.AddWithValue("@imie", tB1.Text);
                updatecomm.Parameters.AddWithValue("@nazwisko", tB2.Text);
                updatecomm.Parameters.AddWithValue("@telefon", tB3.Text);
                updatecomm.Parameters.AddWithValue("@email", tB4.Text);
                updatecomm.Parameters.AddWithValue("@stanowisko", tB5.Text);
                updatecomm.Parameters.AddWithValue("@iddzialu",Convert.ToInt16(tB6.Text));

                if (tB0.Text != String.Empty)
                {
                    updatecomm.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {tB0.Text} w tabeli Pracownicy.");
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
    }
}

