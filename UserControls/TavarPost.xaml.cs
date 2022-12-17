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
using Arasaka_Employers.Pages;
using System.Reflection;

namespace Arasaka_Employers.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TavarPost.xaml
    /// </summary>
    public partial class TavarPost : UserControl
    {
        
        MySqlConnection con;
        private int id;
        private string NameTov;
        public ListView view;
        private int cost;

        public TavarPost(Postavka postavka,int id_tovar)
        {
            InitializeComponent();
            view = postavka.Spisok;
            id=id_tovar;
            con = new MySqlConnection(App.conString);
            con.Open();
            string qewrry = $"Select * from products where Id_product='{id_tovar}'";
            MySqlCommand cmd = new MySqlCommand(qewrry, con);
            var appDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                NameTov = Convert.ToString(dr[1]);
                NameTovar.Text = Convert.ToString(dr[1]);
                img.DataContext = Convert.ToString(dr[6]);
                cost = Convert.ToInt32(dr[4]);
                img.Source = new BitmapImage(new Uri(appDir+ $"\\{System.IO.Path.GetFileName(dr[6].ToString())}", UriKind.RelativeOrAbsolute));
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //++
        {
            view.Items.Clear();
            if(MyLists.spisoks.Count == 0)
            {
                MyLists.spisoks.Add(new Spisok { ID = id, Name = NameTov, Count = 0, cost =cost });
            }          
            MyLists.spisoks.FindAll(x=>x.ID == id).ForEach(x=>x.Count+=1);
            if (Test() == true) 
            {
                MyLists.spisoks.Add(new Spisok { ID = id, Name = NameTov, Count = 1,cost= cost });
            }
            foreach(var item in MyLists.spisoks) 
            {
                view.Items.Add(new list_item(item));
            }
        }
        private bool Test() 
        {
            bool result = true;
            foreach(var item in MyLists.spisoks)
            {
                if(item.ID ==id)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //--
        {
            view.Items.Clear();
            MyLists.spisoks.FindAll(x=>x.ID==id).ForEach(x=>x.Count-=1);
            foreach(var spisok in MyLists.spisoks) 
            { 
                if(spisok.Count == 0) 
                { 
                    MyLists.spisoks.Remove(spisok);
                    break;
                }
            }
            foreach (var item in MyLists.spisoks)
            {
                view.Items.Add(new list_item(item));
            }
        }
    }
}
