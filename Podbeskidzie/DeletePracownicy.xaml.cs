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
    /// Interaction logic for DeletePracownicy.xaml
    /// </summary>
    public partial class DeletePracownicy : Page
    {
        SqlConnection connection;
        string query = "Select * from Pracownicy where ID_Pracownika like @ID";
        string delete = "Delete from Pracownicy where ID_Pracownika like @ID";
        SqlCommand command, delcommand;
        SqlDataReader reader;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public DeletePracownicy()
        {
            InitializeComponent();
        }

        public DeletePracownicy(SqlConnection connection)
        {
            InitializeComponent();
            this.connection= connection;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn2.IsEnabled = true;
            try
            {
                try
                {
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID",tB0.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    tB1.Text = reader.GetString(1);
                    tB2.Text = reader.GetString(2);
                    tB3.Text = reader.GetString(3);
                    tB4.Text = reader.GetString(4);
                    tB5.Text = reader.GetString(5);
                    tB6.Text = reader.GetInt16(6).ToString();

                    reader.Close();
                }
                catch(Exception exc)
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
            catch(Exception exc)
            {
                wyslaneInfo(exc.Message);
            }

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten rekord ?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    string id = tB0.Text;
                    int i = 0;
                    delcommand = new SqlCommand(delete, connection);
                    delcommand.Parameters.AddWithValue("@ID", tB0.Text);
                    i = delcommand.ExecuteNonQuery();
                    tB0.Text = "";
                    tB1.Text = "";
                    tB2.Text = "";
                    tB3.Text = "";
                    tB4.Text = "";
                    tB5.Text = "";
                    tB6.Text = "";
                    

                    if (i != 0)
                    {
                        wyslaneInfo($"Usunięto rekord o numerze ID = {id} w tabeli Pracownicy.");
                    }
                    else
                    {
                        wyslaneInfo("Błąd podczas usuwania.");
                    }
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
            }
        }

        private void tB0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btn1_Click(this, new RoutedEventArgs());
        }

        private void tB0_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }
    }
}
