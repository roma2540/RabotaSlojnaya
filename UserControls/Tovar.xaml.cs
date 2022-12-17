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
namespace Arasaka_Employers.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Tovar.xaml
    /// </summary>
    public partial class Tovar : UserControl
    {
        private string conString = "server=localhost;user=root;" +
            "database=arasaka;password=1234";
        MySqlConnection con;
        public Tovar(int id_tovar)
        {
            InitializeComponent();
            
            con = new MySqlConnection(conString);
            con.Open();
            string qewrry = $"Select * from products where Id_product='{id_tovar}'";
            var appDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MySqlCommand cmd = new MySqlCommand(qewrry, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                NameTov.Text = Convert.ToString(dr[1]);
                Discount.Text = Convert.ToString(dr[2]);
                cost.Text = Convert.ToString(dr[4]);
                Count.Text = Convert.ToString(dr[5]);
                img.DataContext = Convert.ToString(dr[6]);      
                img.Source = new BitmapImage(new Uri(appDir+ $"\\{System.IO.Path.GetFileName(dr[6].ToString())}", UriKind.RelativeOrAbsolute));
            }
            con.Close();       

        }
    }
}
