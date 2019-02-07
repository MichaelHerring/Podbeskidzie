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
using System.Data;

namespace Podbeskidzie
{
    /// <summary>
    /// Interaction logic for Tables.xaml
    /// </summary>
    public partial class ShowTable : Page
    {
        SqlConnection connection;
        string tableName;
        string ifrr="";

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public ShowTable()
        {
            InitializeComponent();
        }

        public ShowTable(SqlConnection conn, string x)
        {
            InitializeComponent();
            this.connection = conn;
            this.tableName = x;
        }
        public ShowTable(SqlConnection conn, string x,string d)
        {
            InitializeComponent();
            this.connection = conn;
            this.tableName = x;
            this.ifrr = d;
        }


        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        string query;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ifrr == "")
            {

                query = $"select * from {tableName}";

            }
            else if (ifrr=="ilosc")
            {
                query = "select count(w.IDWypozyczenia) as 'Ilosc wypozyczen',w.IDSprzetu,s.Nazwa_Sprzetu,s.Opis,s.Meski_Damski" +
                    " from Wypozyczenie w join Sprzet s on w.IDSprzetu = s.IDSprzetu " +
                    "group by w.IDSprzetu,s.Nazwa_Sprzetu,s.Opis,s.Meski_Damski";


                ifrr = String.Empty;
            }

            else if (ifrr == "srednia")
            {
                query = "select AVG(DATEDIFF(minute, w.Data_Od, w.Data_Do)) / 60.0 as 'Sredni czas wypozyczenia (h)',w.IDSprzetu,s.Nazwa_Sprzetu,s.Opis,s.Meski_Damski" +
                    " from Wypozyczenie w join Sprzet s on w.IDSprzetu = s.IDSprzetu" +
                    " group by w.IDSprzetu,s.Nazwa_Sprzetu,s.Opis,s.Meski_Damski";
                
                ifrr = String.Empty;
            }
            else if (ifrr == "l.kategoria")
            {
                query = "select count(w.IDWypozyczenia) as 'Ilosc wypozyczen w kategorii',s.IDKategorii,r.Nazwa_Kategorii" +
                    " from Wypozyczenie w join Sprzet s on w.IDSprzetu = s.IDSprzetu" +
                    " join Rodzaj_Sprzetu r on s.IDKategorii = r.IDKategorii" +
                    " group by s.IDKategorii,r.Nazwa_Kategorii";
                
                ifrr = String.Empty;
            }
            else if (ifrr == "s.kategoria")
            {
                query = "select AVG(DATEDIFF(minute, w.Data_Od, w.Data_Do)) / 60.0 as 'Sredni czas wypozyczenia (h)',s.IDKategorii,r.Nazwa_Kategorii " +
                    "from Wypozyczenie w join Sprzet s on w.IDSprzetu = s.IDSprzetu" +
                    " join Rodzaj_Sprzetu r on s.IDKategorii = r.IDKategorii" +
                    " group by s.IDKategorii,r.Nazwa_Kategorii";
                
                ifrr = String.Empty;
            }


            command = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter(command);
                table = new DataTable();

                try
                {
                    adapter.Fill(table);
                    DataGr.ItemsSource = table.DefaultView;
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);

                }
           
        }
    }
}
