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
using MySql.Data.MySqlClient;
using Arasaka_Employers.Windows;

namespace Arasaka_Employers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private MySqlConnection con;
        public MainWindow()
        {
            con = new MySqlConnection(App.conString);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Avtoriz()==true)
            {
                string MainQuerry = $"SELECT Id_User,Role FROM arasaka.users where login ='{logi_tb.Text}'and Password='{passord_pb.Password}'";
                con.Open();
                MySqlCommand command = new MySqlCommand(MainQuerry, con);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read() != false)
                { 
                    if (Convert.ToInt32(reader[1]) == 1) 
                    { Admin_profile admin_Profile = new Admin_profile(Convert.ToInt32(reader[0]));
                        admin_Profile.Show();
                    }                         
                    else
                    {
                        MainWF mainWF = new MainWF(4, Convert.ToInt32(reader[0]));
                    }                       
                    this.Close();
                }
                else { MessageBox.Show("Не верный логин или пароль"); }
                con.Close();
            }
        }
        private bool Avtoriz() 
        {
            bool test = true;
            if (logi_tb.Text == "" && passord_pb.Password == "")
            {
                MessageBox.Show("Заполните поля");
                test = false;
            }
            else if (logi_tb.Text == "" && passord_pb.Password != "") 
            { MessageBox.Show("Заполните логин");
                test = false;
            }
            else if (passord_pb.Password == "" && logi_tb.Text != "") 
            { MessageBox.Show("Заполните пароль");
                test = false;
            }
            return test;
            
        }
    }
}
