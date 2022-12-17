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

namespace Arasaka_Employers.Pages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        
        MySqlConnection con;
        public Orders()
        {
            con = new MySqlConnection(App.conString);
            InitializeComponent();

        }
        private void OrdersAdd()
        {
            con.Open();
            string qweryy = "Select Id_product from products";
            MySqlCommand cmd = new MySqlCommand(qweryy, con);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            foreach (var item in mySqlDataReader)
            {
                //Vitrina.Items.Add(new Order(Convert.ToInt32(item)));
            }
            con.Close();
        }
    }
}
