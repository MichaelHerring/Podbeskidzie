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
    /// Interaction logic for InsertPracownicy.xaml
    /// </summary>
    public partial class InsertPracownicy : Page
    {
        SqlConnection connection;
        string query = "Insert into Pracownicy values(@Imie, @Nazwisko, @Pesel, @Telefon, @Email)";
        SqlCommand command;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;


        public InsertPracownicy()
        {
            InitializeComponent();
        }

        public InsertPracownicy(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }



        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "" || tB3.Text == "" || tB4.Text == "" || tB5.Text=="")
            {
                wyslaneInfo("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Imie", tB1.Text);
                command.Parameters.AddWithValue("@Nazwisko", tB2.Text);
                command.Parameters.AddWithValue("@Pesel",Convert.ToDecimal(tB3.Text));
                command.Parameters.AddWithValue("@Telefon", tB4.Text);
                command.Parameters.AddWithValue("@Email", tB5.Text);
                
            }
            try
            {
                command.ExecuteNonQuery();
                wyslaneInfo("Dodano rekord do tabeli Pracownicy.");
                tB1.Text = "";
                tB2.Text = "";
                tB3.Text = "";
                tB4.Text = "";
                tB5.Text = "";
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }
    }
}
