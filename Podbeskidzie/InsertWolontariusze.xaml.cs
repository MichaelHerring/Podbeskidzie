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
    /// Interaction logic for InsertWolontariusze.xaml
    /// </summary>
    public partial class InsertWolontariusze : Page
    {
        SqlConnection connection;

        string query = "Insert into Wolontariusze values(@imie, @nazwisko,@email,@telefon)";
        SqlCommand command;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public InsertWolontariusze()
        {
            InitializeComponent();
        }

        public InsertWolontariusze(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Insert() //metoda do dodawania danych
        {
            try
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@imie", tB1.Text);
                command.Parameters.AddWithValue("@nazwisko", tB2.Text);
                command.Parameters.AddWithValue("@email", tB3.Text);
                command.Parameters.AddWithValue("@telefon", tB4.Text);


                command.ExecuteNonQuery();
                wyslaneInfo("Dodano rekord do tabeli Wolontariusze.");
                tB1.Text = "";
                tB2.Text = "";
                tB3.Text = "";
                tB4.Text = "";


            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "")
            {
                wyslaneInfo("Wypełnij wymagane pola: Imię, Nazwisko.");
            }
            else if (tB3.Text == "" || tB4.Text== "")
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
