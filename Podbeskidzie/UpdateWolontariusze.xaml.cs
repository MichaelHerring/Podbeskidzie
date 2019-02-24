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
    /// Interaction logic for UpdateWolontariusze.xaml
    /// </summary>
    public partial class UpdateWolontariusze : Page
    {
        SqlConnection connection;
        SqlCommand command, updatecomm;

        string query = "select * from Wolontariusze where ID_Wolontariusza like @ID";
        string update = @"update Wolontariusze
                        set Imie = @imie, 
                        Nazwisko = @nazwisko, 
                        Pesel = @pesel, 
                        Telefon = @telefon
                        where ID_Wolontariusza = @ID";

        SqlDataReader reader;

        public delegate void WylijInfo(string komunikat);
        public static event WylijInfo wyslaneInfo;

        public UpdateWolontariusze()
        {
            InitializeComponent();
            
        }

        public UpdateWolontariusze(SqlConnection connection)
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
                    tB3.Text = reader.GetDecimal(3).ToString();
                    tB4.Text = reader.GetString(4);
                    
                    reader.Close(); //zamknięcie readera
                    tB1.IsEnabled = true;
                    tB2.IsEnabled = true;
                    tB3.IsEnabled = true;
                    tB4.IsEnabled = true;
                    
                }
                catch (Exception exc)
                {
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    tB1.Text = "";
                    tB2.Text = "";
                    tB3.Text = "";
                    tB4.Text = "";
      
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
                updatecomm.Parameters.AddWithValue("@pesel", Convert.ToDecimal(tB3.Text));
                updatecomm.Parameters.AddWithValue("@telefon", tB4.Text);


                if (tB0.Text != String.Empty)
                {
                    updatecomm.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {tB0.Text} w tabeli Wolontariusze.");
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
