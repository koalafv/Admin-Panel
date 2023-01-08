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
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace wpf_i_bazy
{
    /// <summary>
    /// Logika interakcji dla klasy AddRow.xaml
    /// </summary>
    public partial class AddRow : Window
    {
        private int last_id;
        public AddRow()
        {
            InitializeComponent();
            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {

                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"select * from uzytkownicy";
                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    last_id = i;
                }
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
         
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            int Same_nick_OR_Email = 0;
           
            string connStr = $"server=localhost;user=root;database=kramdrop;password=;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open(); 
                DataTable dataTable = new DataTable();
                MySqlCommand secQuery = con.CreateCommand();
                secQuery.CommandText = @"select * from uzytkownicy";
                MySqlDataReader secReader;
                secReader = secQuery.ExecuteReader();
                dataTable.Load(secReader);

                foreach (DataRow row in dataTable.Rows)
                {
                    if ((string)row["Nick"] == Nick.Text || (string)row["Mail"] == Mail.Text)
                    {
                        Same_nick_OR_Email = 1;
                        break;
                    }
                    
                }


                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Mail.Text);
            if (String.IsNullOrEmpty(Nick.Text) == false || String.IsNullOrEmpty(Haslo.Password) == false  || String.IsNullOrEmpty(Mail.Text) == false)
            {
                if(match.Success)
                {
                        if(Haslo.Password == Haslov2.Password)
                        {
                           if(Same_nick_OR_Email!=1)
                            {
                                last_id++;
                                last_id++;
                                MySqlCommand myQuery = con.CreateCommand();
                                myQuery.CommandText = @"INSERT INTO uzytkownicy (ID, Nick, Haslo, Mail) VALUES ('" + last_id + "','" + Nick.Text + "', '" + Haslo.Password + "', '" + Mail.Text + "');";

                                MySqlDataReader myReader;
                                myReader = myQuery.ExecuteReader();

                                dataTable.Load(myReader);
                                MessageBox.Show("Dodano pomyslnie");
                                con.Close();
                                this.Close();
                            }
                                
                           else
                            {
                                MessageBox.Show("Juz jest taki nick lub mail");

                            }

                        }
                   
                        else
                        {
                            MessageBox.Show("Hasla się róznia");

                        }

                    }
                else
                {
                    MessageBox.Show("Podano blednego maila");
                }
               
            }
            else
            {
                MessageBox.Show("wprowadz poprawne dane");
            }
            }
        }


        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
         
            this.Close();
        }
    }
}
