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

namespace Arasaka_Employers.Windows
{
    /// <summary>
    /// Логика взаимодействия для Admin_profile.xaml
    /// </summary>
    public partial class Admin_profile : Window
    {
        private int _id;
        public Admin_profile(int id)
        {
            _id= id;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //tovar
        {
            MainWF mainWF = new MainWF(1,_id);
            mainWF.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //sotryd
        {
            MainWF mainWF = new MainWF(2,_id);
            mainWF.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //otgruzka
        {
            MainWF mainWF = new MainWF(3,_id);
            mainWF.Show();
            this.Close();
        }
    }
}
