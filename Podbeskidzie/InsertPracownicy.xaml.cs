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
        string query = "Insert into Pracownicy values(@Imie, @Nazwisko," +
            " @Telefon_Pracownika, @Email_Pracownika, @Stanowisko, @ID_Dzialu)";
        string loadquery = "select ID_Dzialu, Nazwa_Dzialu from Dzialy order by ID_Dzialu";
        SqlCommand command,command1;
        SqlDataReader reader;

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


        private void Insert() //metoda do dodawania danych
        {
            try {
                string trimmed = tB6.Text;

                for(int i=0;i<tB6.Text.Length;i++)
                {
                    if(tB6.Text[i]==' ')
                    {
                        trimmed = tB6.Text.Remove(i);
                        break;
                    }
                }

            
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Imie", tB1.Text);
                command.Parameters.AddWithValue("@Nazwisko", tB2.Text);
                command.Parameters.AddWithValue("@Telefon_Pracownika", tB3.Text);
                command.Parameters.AddWithValue("@Email_Pracownika", tB4.Text);
                command.Parameters.AddWithValue("@Stanowisko", tB5.Text);
                command.Parameters.AddWithValue("@ID_Dzialu", Convert.ToInt16(trimmed));
                

                command.ExecuteNonQuery();
                wyslaneInfo("Dodano rekord do tabeli Pracownicy.");
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                command1 = new SqlCommand(loadquery, connection);
                reader=command1.ExecuteReader();
                
                while(reader.Read())
                {
                    tB6.Items.Add(reader.GetInt16(0) + $" ({reader.GetString(1)})");
                }
                reader.Close();

            }
            catch(Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "" || tB6.Text == "")
            {
                wyslaneInfo("Wypełnij wymagane pola: Imię, Nazwisko, ID Działu.");
            }
            else if (tB4.Text == "" || tB5.Text == "" || tB3.Text == "")
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
