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
    /// Logika interakcji dla klasy MainPanel.xaml
    /// </summary>
    /// 

  
    public partial class MainPanel : Window
    {

        public void Refresh_ID()
        {
            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                int i = 0;
                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"select * from zwierzeta";
                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);
                con.Close();
                foreach (DataRow row in dataTable.Rows)
                {
                    i++;
                    con.Open();
                    MySqlCommand secQuery = con.CreateCommand();
                    secQuery.CommandText = @$"UPDATE zwierzeta SET ID = {i}  WHERE Nazwa_zwierzecia = '" + row["Nazwa_zwierzecia"] + "';";
                    MySqlDataReader secReader;
                    secReader = secQuery.ExecuteReader();
                    dataTable.Load(secReader);
                    con.Close();
                }

            }
        }

        public SolidColorBrush BrushFromHex(string hexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexColorString));
        }
        
        public MainPanel()
        {
            InitializeComponent();
            SolidColorBrush oneCOlor = BrushFromHex("#060E3A");
            SolidColorBrush Seccolor = BrushFromHex("#65B9B5");


            Refresh_ID();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }




        private void SideBar_MouseEnter(object sender, MouseEventArgs e)
        {
            //Admin Panel
            SideBar.SetValue(Grid.ColumnSpanProperty, 2);
            Image_admin_icon.Visibility = Visibility.Hidden;
            Admin_text.Visibility = Visibility.Visible;
            //Show tables
            Show_tables_text.Visibility = Visibility.Visible;

            but_image_show_tables.Margin = new Thickness(-75, -45, 3, 3);
            Manage_base_button.Margin = new Thickness(-80, 0, 0, 0);
            Print_Pdf_print.Margin = new Thickness(-80, 10, 0, 0);
            SQLUsers_but.Margin = new Thickness(-80, 10, 0, 0);
        }

        private void SideBar_MouseLeave(object sender, MouseEventArgs e)
        {
            //Admin Panel
            SideBar.SetValue(Grid.ColumnSpanProperty, 1);
            Image_admin_icon.Visibility = Visibility.Visible;
            Admin_text.Visibility = Visibility.Hidden;
            //Show tables
            Show_tables_text.Visibility = Visibility.Hidden;
            but_image_show_tables.Margin = new Thickness(0);
            Manage_base_button.Margin = new Thickness(0);
            Print_Pdf_print.Margin = new Thickness(0,10,0,0);
            SQLUsers_but.Margin = new Thickness(-0, 10, 0, 0);

        }
        private int last_id = 0;
        private bool klik = false;
        private bool restart = false;
        private void but_image_show_tables_Click(object sender, RoutedEventArgs e)
        {
            ShowTables_stackpanel.Visibility = Visibility.Visible;
            Show_manage_base.Visibility = Visibility.Hidden;
            Print_toPDF_stackpanel.Visibility = Visibility.Hidden;
            User_Controll_stack.Visibility = Visibility.Hidden;


            Refresh_ID();
            SolidColorBrush oneCOlor = BrushFromHex("#060E3A");
            SolidColorBrush Seccolor = BrushFromHex("#65B9B5");
            if (klik == false)
            {
                TextBlock textBlock1 = new TextBlock();
                database.Children.Add(textBlock1);
                textBlock1.Foreground = Brushes.BurlyWood;
                textBlock1.FontFamily = new FontFamily("Sylfaen");
                textBlock1.FontSize = 15;
                textBlock1.FontWeight = FontWeights.UltraBold;
                textBlock1.Text = $"ID\tZwierze\t\tWystepowanie\t\tTyp";
            }

            klik = true;

            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            int i = 0;
            using (MySqlConnection con = new MySqlConnection(connStr))
            {

                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"select * from zwierzeta";
                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);


               
                for (i = last_id; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    if (i % 2 == 0)
                    {
                        TextBlock textBlock = new TextBlock();
                        database.Children.Add(textBlock);


                        textBlock.Foreground = Seccolor;
                        textBlock.Background = oneCOlor;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["ID"].ToString()}\t{row["Nazwa_zwierzecia"].ToString()}\t\t{row["Gdzie_wystepuje"].ToString()}\t\t{row["co_je"].ToString()}";

                    }
                    else
                    {

                        TextBlock textBlock = new TextBlock();
                        database.Children.Add(textBlock);

                        textBlock.Foreground = oneCOlor;
                        textBlock.Background = Seccolor;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["ID"].ToString()}\t{row["Nazwa_zwierzecia"].ToString()}\t\t{row["Gdzie_wystepuje"].ToString()}\t\t{row["co_je"].ToString()}";

                    }
                    last_id = i + 1;
                   
                }

      



            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) // Zamyka program
        {
            this.Close();
        }
        private int last_id_mange = 0;
        public void Manage_base_button_Click(object sender, RoutedEventArgs e)
        {
            Refresh_ID();
            Show_manage_base.Visibility = Visibility.Visible;
            ShowTables_stackpanel.Visibility = Visibility.Hidden;
            Print_toPDF_stackpanel.Visibility = Visibility.Hidden;

            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            int i = 0;
            SolidColorBrush oneCOlor = BrushFromHex("#060E3A");
            SolidColorBrush Seccolor = BrushFromHex("#65B9B5");
            using (MySqlConnection con = new MySqlConnection(connStr))
            {

                MySqlCommand myQuery = con.CreateCommand();
                myQuery.CommandText = @"select * from zwierzeta";
                MySqlDataReader myReader;
                con.Open();
                myReader = myQuery.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myReader);



                for (i = last_id_mange; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    if (i % 2 == 0)
                    {

                        TextBlock textBlock = new TextBlock();
                        dane_z_bazy.Children.Add(textBlock);


                        textBlock.Foreground = Seccolor;
                        textBlock.Background = oneCOlor;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["ID"].ToString()} \t{row["Nazwa_zwierzecia"].ToString()} \t\t{row["Gdzie_wystepuje"].ToString()} \t\t{row["co_je"].ToString()}";

                    }
                    else
                    {

                        TextBlock textBlock = new TextBlock();
                        dane_z_bazy.Children.Add(textBlock);

                        textBlock.Foreground = oneCOlor;
                        textBlock.Background = Seccolor;
                        textBlock.FontFamily = new FontFamily("Sylfaen");
                        textBlock.FontSize = 15;
                        textBlock.Text = $"{row["ID"].ToString()} \t{row["Nazwa_zwierzecia"].ToString()} \t\t{row["Gdzie_wystepuje"].ToString()} \t\t{row["co_je"].ToString()}";

                    }
                    last_id_mange = i + 1;

                  
                    
                }

            }
        }
        public int count_RegistePage = 0;
        private void Add_row_Click(object sender, RoutedEventArgs e)
        {
            Add_animal_to_base A = new Add_animal_to_base();
            
            
         
                A.Show();
           
         
           
        }

        private void Delete_row_Click(object sender, RoutedEventArgs e)
        {
           Regex regex = new Regex("^[0-9]{1,5}$");
            int i = 0;
            if (regex.IsMatch(id_to_remove.Text))
            {
                string connStr = "server=localhost;user=root;database=kramdrop;password=;";
                using (MySqlConnection con = new MySqlConnection(connStr))
                {

                    MySqlCommand myQuery = con.CreateCommand();
                    myQuery.CommandText = @"DELETE FROM zwierzeta WHERE ID = " + id_to_remove.Text + ";";
                    MySqlDataReader myReader;
                    con.Open();
                    myReader = myQuery.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(myReader);
                    Refresh_ID();
                    MessageBox.Show("Usunieto pomyslnie");
                    con.Close();
                    last_id = 0;
                    last_id_mange = 0;
                    dane_z_bazy.Children.Clear();
                    database.Children.Clear();
                    klik = false;
                }
            }
            else
            {
                MessageBox.Show("Podaj poprawna wartość");
            }
           
        }

        public int id;
       
        public void Edit_row_Click(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("^[0-9]{1,5}$");
            EditRow editwindow = new EditRow();
      
            int i = 0;
            if (regex.IsMatch(id_to_edit.Text))
            {
                editwindow.iden = int.Parse(id_to_edit.Text) - 1;
                id = int.Parse(id_to_edit.Text) - 1;
                string connStr = "server=localhost;user=root;database=kramdrop;password=;";
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    editwindow.Show();
                    MySqlCommand myQuery = con.CreateCommand();
                    myQuery.CommandText = @"select * from zwierzeta";
                    MySqlDataReader myReader;
                    con.Open();
                    myReader = myQuery.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(myReader);
                   
                    DataRow row = dataTable.Rows[id];
                    editwindow.Animal.Text = (string)row["Nazwa_zwierzecia"];
                    editwindow.Wystepowanie.Text = (string)row["Gdzie_wystepuje"];
                    string coje = (string)row["co_je"];
                    last_id = 0;
                    last_id_mange = 0;
                    dane_z_bazy.Children.Clear();
                    database.Children.Clear();
                    klik = false;
                    if (coje == "Mięsożerny")
                    {
                        editwindow.combobox.SelectedIndex = 0;

                    }
                    else if (coje == "Roślinożerny")
                    {
                        editwindow.combobox.SelectedIndex = 1;//do naprawy te co mboboxy

                    }
                    else if(coje== "Wszystkożerny")
                    {
                        editwindow.combobox.SelectedIndex = 2;

                    }
                }
            }
            else
            {
                MessageBox.Show("Podaj poprawna wartość");
            }
        }

        private void Print_Pdf_print_Click(object sender, RoutedEventArgs e)
        {
            Show_manage_base.Visibility = Visibility.Hidden;
            ShowTables_stackpanel.Visibility = Visibility.Hidden;
            Print_toPDF_stackpanel.Visibility = Visibility.Visible;
            User_Controll_stack.Visibility = Visibility.Hidden;


        }
        public string nick_string;
        private void Create_filePDF_Click(object sender, RoutedEventArgs e)
        {
            Font Cell_color = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.RED);
            Font Tittle_color = new Font(Font.FontFamily.HELVETICA, 30, Font.BOLD, BaseColor.CYAN);
            Font Tit_color = new Font(Font.FontFamily.HELVETICA, 13, Font.BOLD, BaseColor.ORANGE);
            Font Time_color = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.DARK_GRAY);


            string connStr = "server=localhost;user=root;database=kramdrop;password=;";
            int i = 0;
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                string sSelectedFolder;
                sSelectedFolder = fbd.SelectedPath;
                if (sSelectedFolder !="")
                {
                    MySqlCommand myQuery = con.CreateCommand();
                    myQuery.CommandText = @"select * from zwierzeta";
                    MySqlDataReader myReader;
                    con.Open();
                    myReader = myQuery.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(myReader);


                    System.IO.FileStream fs = new FileStream(@$"{sSelectedFolder}\{PDF_File_name.Text}.pdf", FileMode.Create);
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    SolidColorBrush Seccolor = BrushFromHex("#65B9B5");

                    document.Open();
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(@"C:\Users\koala\source\repos\wpf i bazy\wpf i bazy\Images\SideBar\adminicon.png");
                    jpg.ScaleToFit(140f, 120f);
                    jpg.Alignment = Element.ALIGN_CENTER;
                    jpg.SpacingBefore = 10f;
                    
                    iTextSharp.text.Paragraph tittle = new iTextSharp.text.Paragraph("ADMIN PANEL", Tittle_color);
                    tittle.SpacingAfter = 20f;
                    tittle.Alignment = Element.ALIGN_CENTER;

                    
                    PdfPTable PdfTable = new PdfPTable(dataTable.Columns.Count);
                    PdfPCell Id_cell = new PdfPCell(new Phrase(new Chunk("ID", Tit_color)));
                    Id_cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    Id_cell.BackgroundColor = BaseColor.BLACK;

                    PdfPCell Id_cell1 = new PdfPCell(new Phrase(new Chunk("Nazwa", Tit_color)));
                    Id_cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    Id_cell1.BackgroundColor = BaseColor.BLACK;

                    PdfPCell Id_cell2 = new PdfPCell(new Phrase(new Chunk("Występowanie", Tit_color)));
                    Id_cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    Id_cell2.BackgroundColor = BaseColor.BLACK;

                    PdfPCell Id_cell3 = new PdfPCell(new Phrase(new Chunk("Typ", Tit_color)));
                    Id_cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    Id_cell3.BackgroundColor = BaseColor.BLACK;

                    PdfTable.AddCell(Id_cell);
                    PdfTable.AddCell(Id_cell1);
                    PdfTable.AddCell(Id_cell2);
                    PdfTable.AddCell(Id_cell3);
                    PdfPCell PdfCell = null;
                    
                    for (int rows = 0; rows < dataTable.Rows.Count; rows++)
                    {
                        for (int column = 0; column < dataTable.Columns.Count; column++)
                        {
                            PdfCell = new PdfPCell(new Phrase(new Chunk(dataTable.Rows[rows][column].ToString(), Cell_color)));
                            PdfCell.HorizontalAlignment=Element.ALIGN_CENTER;    
                            PdfCell.BorderColor = iTextSharp.text.BaseColor.CYAN;
                            PdfTable.AddCell(PdfCell);
                        }
                    }
                    //PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                    PdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

                    DateTime saveNow = DateTime.Now;


                    iTextSharp.text.Paragraph Time = new iTextSharp.text.Paragraph($"Uzytkownik: {nick_string}\nCzas utworzenia PDF: {saveNow}", Time_color);
                    Time.Alignment = Element.ALIGN_BOTTOM;
                    Time.Alignment = Element.ALIGN_RIGHT;
                    Time.SpacingBefore = 25f;


                    document.Add(jpg);
                    document.Add(tittle);
                    document.Add(PdfTable);
                    document.Add(Time);
                   
                    // Close the document  
                    document.Close();
                    // Close the writer instance  
                    writer.Close();
                    // Always close open filehandles explicity  
                    fs.Close();

                    if (Start_pdf_file.IsChecked == true)
                    {
                        Process.Start(new ProcessStartInfo { FileName = @$"{sSelectedFolder}\{PDF_File_name.Text}.pdf", UseShellExecute = true });
                    }
                    sSelectedFolder = "";
                    PDF_File_name.Text = "";
                }
                  
               
        
            }
        }

        private void Show_Users_Click(object sender, RoutedEventArgs e)
        {
            SQLUsers sQLUsers = new SQLUsers();
            sQLUsers.Show();
        }

        private void SQLUsers_but_Click(object sender, RoutedEventArgs e)
        {
            User_Controll_stack.Visibility = Visibility.Visible;
            Print_toPDF_stackpanel.Visibility = Visibility.Hidden;


            ShowTables_stackpanel.Visibility = Visibility.Hidden;
            Show_manage_base.Visibility = Visibility.Hidden;
            Print_toPDF_stackpanel.Visibility = Visibility.Hidden;
        }

        private void All_Checked(object sender, RoutedEventArgs e)
        {
            if(All.IsChecked==true)
            {
                Create.IsEnabled = false;
                Delete.IsEnabled = false;
                Insert.IsEnabled = false;
                Select.IsEnabled = false;
                Update.IsEnabled = false;
            }

        }

        private void All_Unchecked(object sender, RoutedEventArgs e)
        {
            Create.IsEnabled = true;
            Delete.IsEnabled = true;
            Insert.IsEnabled = true;
            Select.IsEnabled = true;

            Update.IsEnabled = true;
        }

        private void Add_user_Click(object sender, RoutedEventArgs e)
        {
     
            if(All.IsChecked==true)
            {
                string connStr = "server=localhost;user=root;database=kramdrop;password=;";
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();
                    MySqlCommand Query1 = con.CreateCommand();
                    Query1.CommandText = @$"CREATE USER '{User_name.Text}'@'localhost' IDENTIFIED BY '{User_pass.Text}';";
                    MySqlDataReader myReader1;
                    myReader1 = Query1.ExecuteReader();
                    con.Close();

                }
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();

                    MySqlCommand Query2 = con.CreateCommand();
                    Query2.CommandText = @$"GRANT ALL PRIVILEGES ON * . * TO '{User_name.Text}'@'localhost' ;";
                    MySqlDataReader myReader2;
                    myReader2 = Query2.ExecuteReader();
                    con.Close();
                }
              /*  using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();

                    MySqlCommand Query3 = con.CreateCommand();
                    Query3.CommandText = @$"Flush PRIVILEGES;";
                    MySqlDataReader myReader3;
                    myReader3 = Query3.ExecuteReader();
                    con.Close();
                }*/
                All.IsChecked = false;
                MessageBox.Show("Dodano pomyślnie");
            }
            else if(Create.IsChecked==true|| Delete.IsChecked == true || Insert.IsChecked == true || Select.IsChecked == true|| Update.IsChecked ==true || Select.IsChecked == true)
            {

                string connStr = "server=localhost;user=root;database=kramdrop;password=;";
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();
                    MySqlCommand Query1 = con.CreateCommand();
                    Query1.CommandText = @$"CREATE USER '{User_name.Text}'@'localhost' IDENTIFIED BY '{User_pass.Text}';";
                    MySqlDataReader myReader1;
                    myReader1 = Query1.ExecuteReader();
                    con.Close();

                }

                if (Create.IsChecked == true)
                {
                  
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query2 = con.CreateCommand();
                        Query2.CommandText = @$"GRANT CREATE  ON * . * TO '{User_name.Text}'@'localhost';";
                        MySqlDataReader myReader2;
                        myReader2 = Query2.ExecuteReader();
                        con.Close();
                    }
                /*    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query3 = con.CreateCommand();
                        Query3.CommandText = @$"Flush PRIVILEGES;";
                        MySqlDataReader myReader3;
                        myReader3 = Query3.ExecuteReader();
                        con.Close();
                    }*/
                }
                if (Delete.IsChecked == true)
                {
                   
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query2 = con.CreateCommand();
                        Query2.CommandText = @$"GRANT DELETE  ON * . * TO '{User_name.Text}'@'localhost';";
                        MySqlDataReader myReader2;
                        myReader2 = Query2.ExecuteReader();
                        con.Close();
                    }
                 /*   using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query3 = con.CreateCommand();
                        Query3.CommandText = @$"Flush PRIVILEGES;";
                        MySqlDataReader myReader3;
                        myReader3 = Query3.ExecuteReader();
                        con.Close();
                    }*/
                }
                if(Insert.IsChecked==true)
                {
                    
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query2 = con.CreateCommand();
                        Query2.CommandText = @$"GRANT INSERT  ON * . * TO '{User_name.Text}'@'localhost';";
                        MySqlDataReader myReader2;
                        myReader2 = Query2.ExecuteReader();
                        con.Close();
                    }
                /*    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query3 = con.CreateCommand();
                        Query3.CommandText = @$"Flush PRIVILEGES;";
                        MySqlDataReader myReader3;
                        myReader3 = Query3.ExecuteReader();
                        con.Close();
                    }*/
                }
                if(Select.IsChecked == true)
                {
                   
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query2 = con.CreateCommand();
                        Query2.CommandText = @$"GRANT SELECT  ON * . * TO '{User_name.Text}'@'localhost';";
                        MySqlDataReader myReader2;
                        myReader2 = Query2.ExecuteReader();
                        con.Close();
                    }
                  /*  using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query3 = con.CreateCommand();
                        Query3.CommandText = @$"Flush PRIVILEGES;";
                        MySqlDataReader myReader3;
                        myReader3 = Query3.ExecuteReader();
                        con.Close();
                    }*/
                }
                if(Update.IsChecked==true)
                {
                    
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query2 = con.CreateCommand();
                        Query2.CommandText = @$"GRANT UPDATE  ON * . * TO '{User_name.Text}'@'localhost';";
                        MySqlDataReader myReader2;
                        myReader2 = Query2.ExecuteReader();
                        con.Close();
                    }
               /*     using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();

                        MySqlCommand Query3 = con.CreateCommand();
                        Query3.CommandText = @$"Flush PRIVILEGES;";
                        MySqlDataReader myReader3;
                        myReader3 = Query3.ExecuteReader();
                        con.Close();
                    }*/
                }
                Create.IsChecked =false;
                Delete.IsChecked = false;
                Insert.IsChecked = false;
                Select.IsChecked = false;
                Update.IsChecked = false;
                Select.IsChecked = false;
                MessageBox.Show("Dodano pomyślnie");

            }
        }
    }
}
