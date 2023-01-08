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
using System.Text.RegularExpressions;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using MessageBox = System.Windows.MessageBox;
using System.Diagnostics;

namespace wpf_i_bazy
{
    /// <summary>
    /// Logika interakcji dla klasy SQLUsers.xaml
    /// </summary>
    public partial class SQLUsers : Window
    {
        private int last_id = 0;

        public SQLUsers()
        {
            InitializeComponent();
            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            int i = 0;
            SolidColorBrush oneCOlor = Brushes.Blue;
            SolidColorBrush Seccolor = Brushes.Pink;
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                con.Open();
                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"Select user from mysql.user;";
                MySqlDataReader myReader;
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);
                con.Close();
                for (i = last_id; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    if (i % 2 == 0)
                    {
                        TextBlock textBlock = new TextBlock();
                        Users.Children.Add(textBlock);
                        textBlock.Foreground = Seccolor;
                        textBlock.Background = oneCOlor;
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["User"]}";

                    }
                    else
                    {
                        TextBlock textBlock = new TextBlock();
                        Users.Children.Add(textBlock);
                        textBlock.Foreground = oneCOlor;
                        textBlock.Background = Seccolor;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.TextAlignment = TextAlignment.Center;
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["User"]}";

                    }
                    last_id = i + 1;



                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
    }
}
