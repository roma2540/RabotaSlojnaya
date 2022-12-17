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
using Arasaka_Employers.Pages;

namespace Arasaka_Employers.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWF.xaml
    /// </summary>
    public partial class MainWF : Window
    {
        public MainWF(int page,int id)
        {
            InitializeComponent();
            switch (page) 
            { 
                case 1:
                    MainFrame.Content = new Product(id);
                    break;

                case 2:
                    MainFrame.Content = new Employees();
                    break;

                case 3:
                    //MainFrame.Content = new Product(); ЗАмени на историю 
                    break;
                case 4:
                    MainFrame.Content = new Postavka(id);
                    break;
            }
            
        }
    }
}
