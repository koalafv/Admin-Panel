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
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
namespace wpf_i_bazy
{
    /// <summary>
    /// Logika interakcji dla klasy EditRow.xaml
    /// </summary>
    public partial class EditRow : Window
    {
        public EditRow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        public int iden;
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainPanel mainPanel = new MainPanel();
            string selected_value = combobox.SelectedItem.ToString();
            selected_value = selected_value.Substring(37);
            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {

                MySqlCommand myQuery = con.CreateCommand();
                iden += 1;
                myQuery.CommandText = @$"UPDATE zwierzeta SET Nazwa_zwierzecia = '{Animal.Text}',Gdzie_wystepuje = '{Wystepowanie.Text}',co_je='{selected_value}' WHERE ID = {iden} ";

                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);
              
                MessageBox.Show("Edytowano pomyslnie");
                con.Close();
                mainPanel.count_RegistePage = 0;
                this.Close();
              

            }
        }

        private void anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
