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

        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        string query;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (tableName == "Dziennikarze")
            {
                query = @"select d.ID_Dziennikarza as 'ID Dziennikarza', d.Imie, d.Nazwisko, d.ID_Redakcji as 'ID Redakcji', r.Nazwa as 'Nazwa Redakcji', d.Rodzaj, d.Telefon, d.Email
                        from Dziennikarze d join Redakcje r on d.ID_Redakcji = r.ID_Redakcji";
            }
            else if (tableName == "Pracownicy")
            {
                query = @"select p.ID_Pracownika as 'ID Pracownika', p.Imie, p.Nazwisko, p.Telefon_Pracownika as 'Telefon Pracownika', p.Email_Pracownika as 'Email Pracownika', p.Stanowisko, p.ID_Dzialu as 'ID Dzialu', d.Nazwa_Dzialu as 'Nazwa Dzialu'
                        from Pracownicy p join Dzialy d on p.ID_Dzialu = d.ID_Dzialu";
            }
            else
            {
                query = $"select * from {tableName}";
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

        private void DataGr_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //otwarcie linku w przeglądarce
            try
            {
                if (DataGr.CurrentCell.Column.Header.ToString() == "Strona")
                {
                    int i = DataGr.SelectedIndex;
                    string link = table.Rows[i]["Strona"].ToString();
                    System.Diagnostics.Process.Start("http://" + link);
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }
    }
}
