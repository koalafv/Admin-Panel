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
using System.Data;
using MySql.Data.MySqlClient;
namespace wpf_i_bazy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private int pasuje = 2;
        
        private void login_button_Click(object sender, RoutedEventArgs e)
        {

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


                foreach (DataRow row in dataTable.Rows)
                {
                    if ((string)row["Nick"]==txtNick.Text && (string)row["Haslo"]==txtpass.Password)
                    {
                        pasuje = 1;
                        break;
                    }
                    else
                    {
                        pasuje = 0;
                    }
                }
                if(pasuje ==1)
                {
                    MainPanel mainPanel = new MainPanel();
                    mainPanel.nick_string = txtNick.Text;

                    mainPanel.Show();
                    this.Close();
                }
                else if(pasuje==0)
                {
                    MessageBox.Show("bledne haslo lub bledny nick");
                    txtpass.Password = "";
                    txtNick.Text = "";
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {


            int count_RegistePage = 0;
            foreach (Window window in Application.Current.Windows)
            {
                 count_RegistePage++;
            }
            if(count_RegistePage == 2)
            {
                AddRow addRow = new AddRow();
                addRow.Show();
                
            }
            else
            {
                MessageBox.Show("Masz już otwata rejestacje");
            }

        }
    }
}
