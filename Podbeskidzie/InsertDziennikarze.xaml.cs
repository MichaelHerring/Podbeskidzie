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
    /// Interaction logic for InsertDziennikarze.xaml
    /// </summary>
    public partial class InsertDziennikarze : Page
    {
        SqlConnection connection;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        string query = "insert into Dziennikarze values(@imie, @nazwisko, @redakcja, @rodzaj, @telefon, @email)";
        SqlCommand command;

        public InsertDziennikarze()
        {
            InitializeComponent();
        }

        public InsertDziennikarze(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
