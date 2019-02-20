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
    /// Interaction logic for InsertRedakcje.xaml
    /// </summary>
    public partial class InsertRedakcje : Page
    {
        SqlConnection connection;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        string query = "insert into Redakcje values(@nazwa, @telefon, @email, @strona)";
        SqlCommand command;

        public InsertRedakcje()
        {
            InitializeComponent();
        }

        public InsertRedakcje(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "" || tB3.Text == "" || tB4.Text == "")
            {
                wyslaneInfo("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nazwa", tB1.Text);
                command.Parameters.AddWithValue("@telefon", tB2.Text);
                command.Parameters.AddWithValue("@email", tB3.Text);
                command.Parameters.AddWithValue("@strona", tB4.Text);

                try
                {
                    command.ExecuteNonQuery();
                    wyslaneInfo("Dodano rekord do tabeli Redakcje.");
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
        }
    }
}
