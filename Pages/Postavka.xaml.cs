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
using Arasaka_Employers.Classes;
using Word = Microsoft.Office.Interop.Word;
namespace Arasaka_Employers.Pages
{
    /// <summary>
    /// Логика взаимодействия для Postavka.xaml
    /// </summary>
    public partial class Postavka : Page
    {

        List<DocDb> docs = new List<DocDb>();
        MySqlConnection con;
        private int Id;
        private int Id_D;
        public Postavka(int id)
        {
            Id = id;
            con = new MySqlConnection(App.conString);
            InitializeComponent();
            AddTovar();

        }
        private void AddTovar()
        {
            con.Open();
            string qweryy = "Select Id_product from products";
            MySqlCommand cmd = new MySqlCommand(qweryy, con);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                TavarPost tavarPost = new TavarPost(this, mySqlDataReader.GetInt32(0));
                tavarPost.view = this.Spisok;
                Vitrina.Items.Add(tavarPost);
            }
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Оформляй я заебался 
        {
            Zapisat();
            UpdateprodPost();
            CreateDocPost();
        }
        private void Zapisat() 
        {
            con.Open();
            string querry = $"INSERT INTO delivery_history(delivery_date,Id_user,info) VALUES( {DateTime.UtcNow.ToString("MM-dd-yyyy")},{Id},'Прием')";
            MySqlCommand cmd = new MySqlCommand(querry, con);
            cmd.ExecuteNonQuery();
            con.Close();
            string querry2 =$"Select * from delivery_history";
            con.Open();
             MySqlCommand mySql = new MySqlCommand(querry2,con);
             MySqlDataReader dr = mySql.ExecuteReader();
            while(dr.Read())
            {
                Id_D=dr.GetInt32(0);
            }
             con.Close();

        }
        private void Zapisat2()
        {
            con.Open();
            string querry = $"INSERT INTO delivery_history(delivery_date,Id_user,info) VALUES( {DateTime.UtcNow.ToString("MM-dd-yyyy")},{Id},'Отгруз')";
            MySqlCommand cmd = new MySqlCommand(querry, con);
            cmd.ExecuteNonQuery();
            con.Close();
            string querry2 = $"Select * from delivery_history";
            con.Open();
            MySqlCommand mySql = new MySqlCommand(querry2, con);
            MySqlDataReader dr = mySql.ExecuteReader();
            while (dr.Read())
            {
                Id_D = dr.GetInt32(0);
            }
            con.Close();

        }
        private bool TestsCount() 
        {
            bool result = true;            
            con.Open();
            string qweryy = "Select Id_product,Count_product from products";
            MySqlCommand cmd = new MySqlCommand(qweryy, con);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                foreach(var item in MyLists.spisoks)
                {
                    if (item.ID == Convert.ToInt32(mySqlDataReader[0]))
                    {
                        if (item.Count > Convert.ToInt32(mySqlDataReader[1]))
                        { result = false; break; }
                    }
                }
            }
            con.Close();
            return result;
        }
        private void UpdateprodPost() 
        {
          
                foreach (var items in MyLists.spisoks)
                {
                    con.Open();
                    string querry = $"UPDATE products SET Count_product =Count_product+{items.Count} WHERE Id_product = {items.ID}";
                    MySqlCommand mySqlCommand = new MySqlCommand(querry, con);
                    mySqlCommand.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    string querry2 = $"INSERT INTO delivery_products(Id_product,Count,Id_delivery) VALUES( {items.ID},{items.Count},{Id_D})";
                    MySqlCommand mySqlCommand2 = new MySqlCommand(querry2, con);
                    mySqlCommand2.ExecuteNonQuery();
                    con.Close();
                }

               
        }
        private void UpdateprodOtgruz() 
        {
            if (TestsCount() == true)
            {
                foreach (var items in MyLists.spisoks)
                {
                    con.Open();
                    string querry = $"UPDATE products SET Count_product =Count_product-{items.Count} WHERE Id_product = {items.ID}";
                    MySqlCommand mySqlCommand = new MySqlCommand(querry, con);
                    mySqlCommand.ExecuteNonQuery();
                    con.Close();
                    con.Open();
                    string querry2 = $"INSERT INTO delivery_products(Id_product,Count,Id_delivery) VALUES( {items.ID},{items.Count},{Id_D})";
                    MySqlCommand mySqlCommand2 = new MySqlCommand(querry2, con);
                    mySqlCommand2.ExecuteNonQuery();
                    con.Close();
                }
            } 
        }
        private void CreateDocOtgruz() 
        {
            int sumCost = 0;
            int sumCount = 0;
            
            var application = new Word.Application();
            application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;
            range.Text = ($"Отгрузка                                                                                                       Оформлена:{DateTime.Today}");
            range.InsertParagraphAfter();

            Word.Paragraph paragraph1 = document.Paragraphs.Add();

            Word.Paragraph table = document.Paragraphs.Add();
            Word.Range tableReng = table.Range;
            tableReng.InsertParagraphAfter();
            Word.Table tables = document.Tables.Add(tableReng, MyLists.spisoks.Count() + 3, 4);
            tables.Borders.InsideLineStyle = tables.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tables.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range CellRanges;
            CellRanges = tables.Cell(1, 1).Range;
            CellRanges.Text = "Артикул";
            CellRanges = tables.Cell(1, 2).Range;
            CellRanges.Text = "Наименование";
            CellRanges = tables.Cell(1, 3).Range;
            CellRanges.Text = "Цена за еденицу товарв";
            CellRanges = tables.Cell(1, 4).Range;
            CellRanges.Text = "Количество";
            tables.Rows[1].Range.Bold = 1;
            tables.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            for (int i = 0; i < MyLists.spisoks.Count(); i++)
            {
                var chek = MyLists.spisoks[i];
                CellRanges = tables.Cell(i + 2, 1).Range;
                CellRanges.Text = Convert.ToString(chek.ID);
                CellRanges = tables.Cell(i + 2, 2).Range;
                CellRanges.Text = chek.Name;
                CellRanges = tables.Cell(i + 2, 3).Range;
                CellRanges.Text = Convert.ToString(Convert.ToInt32(chek.cost) + " руб.");
                CellRanges = tables.Cell(i + 2, 4).Range;
                CellRanges.Text = Convert.ToString(chek.Count) + " шт.";
                sumCost += chek.cost * chek.Count;
                sumCount += chek.Count;
            }
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 2, 1).Range;
            CellRanges.Text = "Общая стоймость:";
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 3, 1).Range;
            CellRanges.Text = "Ощее количество:";
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 2, 2).Range;
            CellRanges.Text = Convert.ToString(Convert.ToInt32(sumCost) + " руб.");
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 3, 2).Range;
            CellRanges.Text = Convert.ToString(sumCount + " шт.");

            tables.Cell(MyLists.spisoks.Count() + 3, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 3, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 2, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 2, 3).Delete();



            application.Visible = true;

            document.SaveAs($"Otgruzka{Id_D}.docx");
            MyLists.spisoks.Clear();
            Spisok.Items.Clear();
        }
        private void CreateDocPost() 
        {
            int sumCost = 0;
            int sumCount = 0;
            
            var application = new Word.Application();
            application = new Word.Application();
            Word.Document document = application.Documents.Add();

            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;
            range.Text = ($"Поставка                                                                                                       Оформлена:{DateTime.Today}");
            range.InsertParagraphAfter();          

            Word.Paragraph paragraph1 = document.Paragraphs.Add();

            Word.Paragraph table = document.Paragraphs.Add();
            Word.Range tableReng = table.Range;
            tableReng.InsertParagraphAfter();
            Word.Table tables = document.Tables.Add(tableReng, MyLists.spisoks.Count() + 3, 4);
            tables.Borders.InsideLineStyle = tables.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            tables.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range CellRanges;
            CellRanges = tables.Cell(1, 1).Range;
            CellRanges.Text = "Артикул";
            CellRanges = tables.Cell(1, 2).Range;
            CellRanges.Text = "Наименование";
            CellRanges = tables.Cell(1, 3).Range;
            CellRanges.Text = "Цена за еденицу товарв";
            CellRanges = tables.Cell(1, 4).Range;
            CellRanges.Text = "Количество";
            tables.Rows[1].Range.Bold = 1;
            tables.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            for (int i = 0; i < MyLists.spisoks.Count(); i++)
            {
                var chek = MyLists.spisoks[i];
                CellRanges = tables.Cell(i + 2, 1).Range;
                CellRanges.Text = Convert.ToString(chek.ID);
                CellRanges = tables.Cell(i + 2, 2).Range;
                CellRanges.Text = chek.Name;
                CellRanges = tables.Cell(i + 2, 3).Range;
                CellRanges.Text = Convert.ToString(Convert.ToInt32(chek.cost) + " руб.");
                CellRanges = tables.Cell(i + 2, 4).Range;
                CellRanges.Text = Convert.ToString(chek.Count) + " шт.";
                sumCost += chek.cost * chek.Count;
                sumCount += chek.Count;
            }
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 2, 1).Range;
            CellRanges.Text = "Общая стоймость:";
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 3, 1).Range;
            CellRanges.Text = "Ощее количество:";
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 2, 2).Range;
            CellRanges.Text = Convert.ToString(Convert.ToInt32(sumCost) + " руб.");
            CellRanges = tables.Cell(MyLists.spisoks.Count() + 3, 2).Range;
            CellRanges.Text = Convert.ToString(sumCount + " шт.");

            tables.Cell(MyLists.spisoks.Count() + 3, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 3, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 2, 3).Delete();
            tables.Cell(MyLists.spisoks.Count() + 2, 3).Delete();



            application.Visible = true;

            document.SaveAs($"Postavka{Id_D}.docx");
            MyLists.spisoks.Clear();
            Spisok.Items.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //otgruzka
        {
            if (TestsCount() == true)
            {
                Zapisat2();
                UpdateprodOtgruz();
                CreateDocOtgruz();
            }
            else
            {
                MessageBox.Show("Привышено количество");
            }
        }
    }
    public class DocDb 
    {
        public int id;
        public string name;
        public int cost;
        public int count;
    }
}
