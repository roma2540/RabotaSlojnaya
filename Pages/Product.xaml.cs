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
using Arasaka_Employers.UserControls;
using Arasaka_Employers.Windows;

namespace Arasaka_Employers.Pages
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        
        MySqlConnection con;
        private int id;
        public Product(int ID)
        {
            id = ID;
            con = new MySqlConnection(App.conString);
            InitializeComponent();
            ProductAdd();
        }
        private void ProductAdd() 
        { 
            con.Open();
            string qweryy = "Select Id_product from products";
            MySqlCommand cmd = new MySqlCommand(qweryy, con);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read()) 
            {
                Vitrina.Items.Add(new Tovar(mySqlDataReader.GetInt32(0)));
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //category
        {
            RedactCategorys categorys = new RedactCategorys();
            categorys.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //add tovar
        {
            NavigationService.Navigate(new CreateTovar(null));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //redact tovar
        {
            
            
            NavigationService.Navigate(new CreateTovar(Vitrina.SelectedIndex));
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //postavka
        {
            NavigationService.Navigate(new Postavka(id));
        }
    }
}
