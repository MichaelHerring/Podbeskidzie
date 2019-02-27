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
    /// Interaction logic for WynikiWyszukiwania.xaml
    /// </summary>
    public partial class WynikiWyszukiwania : Page
    {
        SqlConnection connection;
        string query;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public WynikiWyszukiwania()
        {
            InitializeComponent();
        }

        public WynikiWyszukiwania(string query, SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.query = query;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                command = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter(command);
                table = new DataTable();
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
