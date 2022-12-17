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
using Arasaka_Employers.Classes;
namespace Arasaka_Employers.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateTovar.xaml
    /// </summary>
    public partial class CreateTovar : Page
    {
        MySqlConnection con;
        private List<Category> categories  = new List<Category>();
        private int? _id;
        private string photo="";
        public CreateTovar(int? id)
        {
            
            con= new MySqlConnection(App.conString);
            InitializeComponent();
          
            if (id != null)
            {
                _id = id;
                con.Open();
                string querry = $"Select * from products where Id_product='{id}'";                
                MySqlCommand mySql = new MySqlCommand(querry, con);
                MySqlDataReader dr = mySql.ExecuteReader();
                while (dr.Read()) 
                {
                    NameTovar.Text = dr[1].ToString();
                    Opisanie.Text = dr[2].ToString();
                    Cost.Text = dr[4].ToString();
                    Count.Text = dr[5].ToString();
                    //img.Source = dr[6].ToString();
                    
                }
                con.Close();
            }
            AddCategoryToCB();
        }
        private void AddCategoryToCB() 
        {
            string querry = "Select * from categorys";
            con.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(querry, con);
            MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                categories.Add(new Category { Name = dataReader[1].ToString()});
            }
            this.Category.ItemsSource = categories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_id == null) 
            {
                string querry = $"INSERT INTO products(Name_product,Description,Category,Cost,Count_product,photo) VALUES('{NameTovar.Text}','{Opisanie.Text}',{Category.SelectedIndex+1},{Convert.ToInt32(Cost.Text)},{Convert.ToInt32(Count.Text)},''); ";
                MySqlCommand mySql = new MySqlCommand(querry, con);
            }          
        }
    }
}
