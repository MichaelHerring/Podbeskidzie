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
    /// Interaction logic for InsertDzialy.xaml
    /// </summary>
    public partial class InsertDzialy : Page
    {
        SqlConnection connection;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        string query = "insert into Dzialy values(@nazwa, @telefon)";
        SqlCommand command;

        public InsertDzialy()
        {
            InitializeComponent();
        }

        public InsertDzialy(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Insert() //metoda do dodawania danych
        {
            try
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nazwa", tB1.Text);
                command.Parameters.AddWithValue("@telefon", tB2.Text);

                command.ExecuteNonQuery();
                wyslaneInfo("Dodano rekord do tabeli Działy.");
                tB1.Text = "";
                tB2.Text = "";
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "")
            {
                wyslaneInfo("Wypełnij wymagane pole: Nazwa.");
            }
            else if (tB2.Text == "")
            {
                if (MessageBox.Show("Czy na pewno chcesz zostawić puste pole?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
