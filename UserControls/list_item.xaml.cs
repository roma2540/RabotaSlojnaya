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
using Arasaka_Employers.Classes;

namespace Arasaka_Employers.UserControls
{
    /// <summary>
    /// Логика взаимодействия для list_item.xaml
    /// </summary>
    public partial class list_item : UserControl
    {
        
        public list_item(Spisok spisok)
        {
            InitializeComponent();
            Tovar.Text = spisok.Name;
            Count.Text = spisok.Count.ToString();
        }
    }
}
