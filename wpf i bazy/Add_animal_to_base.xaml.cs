using System.Data;
using MySql.Data.MySqlClient;
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

namespace wpf_i_bazy
{
    /// <summary>
    /// Logika interakcji dla klasy Add_animal_to_base.xaml
    /// </summary>
    public partial class Add_animal_to_base : Window
    {
        private int last_id;
        public Add_animal_to_base()
        {
            InitializeComponent();
            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {

                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"select * from zwierzeta";
                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    
                    last_id = i;
                }
                last_id++;
                last_id++;
                con.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Wystepowanie.Text) == false || String.IsNullOrEmpty(Animal.Text) == false)
            {
                string selected_value = combobox.SelectedItem.ToString();
                selected_value = selected_value.Substring(37);
                string connStr = "server=localhost;user=root;database=kramdrop;password=;";
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();
                    DataTable dataTable = new DataTable();
                    MySqlCommand secQuery = con.CreateCommand();
                    secQuery.CommandText = @"INSERT INTO zwierzeta (ID, Nazwa_zwierzecia, Gdzie_wystepuje, co_je) VALUES ('" + last_id + "','" + Animal.Text + "', '" + Wystepowanie.Text + "', '" + selected_value + "');";
                    MySqlDataReader secReader;
                    secReader = secQuery.ExecuteReader();
                    dataTable.Load(secReader);
                    MessageBox.Show("Dodano pomyslnie");
                    con.Close();
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Wpisz jakies wartosci");
            }
          

              
        }
    }
}
