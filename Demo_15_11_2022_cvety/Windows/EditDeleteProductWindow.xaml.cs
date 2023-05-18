using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Demo_15_11_2022_cvety.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditDeleteProductWindow.xaml
    /// </summary>
    public partial class EditDeleteProductWindow : Window
    {
        private cvetyEntities context = new cvetyEntities();

        public EditDeleteProductWindow()
        {
            
        }
        public string Key;
        public EditDeleteProductWindow(string key)
        {
            InitializeComponent();
            Key = key;            
            LoadData();
        }

        public void clearFelds()
        {
            TextBoxProductArticleNumber.Text = "";
            TextBoxProductName.Text = "";
            TextBoxProductDescription.Text = "";
            ComboBoxProductCategory.Text = "";
            ComboBoxProductManufacturer.Text = "";
            TextBoxProductCost.Text = "";
            TextBoxProductDiscountAmount.Text = "";
            TextBoxProductQuantityInStock.Text = "";
        }

        public void LoadData()
        {
            var query = from product in context.Product where product.ProductArticleNumber == Key select product;

            ComboBoxProductCategory.ItemsSource = context.Product.GroupBy(product => product.ProductCategory).ToList();
            ComboBoxProductManufacturer.ItemsSource = context.Product.GroupBy(product => product.ProductManufacturer).ToList();
            ComboBoxProductStatus.ItemsSource = context.Product.GroupBy(product => product.ProductStatus).ToList();

            foreach (Product product in query)
            {
                TextBoxProductArticleNumber.Text = product.ProductArticleNumber;
                TextBoxProductName.Text = product.ProductName;
                TextBoxProductDescription.Text = product.ProductDescription;
                ComboBoxProductCategory.Text = product.ProductCategory;
                ComboBoxProductManufacturer.Text = product.ProductManufacturer;
                TextBoxProductCost.Text = product.ProductCost.ToString();
                TextBoxProductDiscountAmount.Text = product.ProductDiscountAmount.ToString();
                TextBoxProductQuantityInStock.Text = product.ProductQuantityInStock.ToString();
                ComboBoxProductStatus.Text = product.ProductStatus;

                if (product.ProductStatus == "Новый")
                {
                    ComboBoxProductStatus.SelectedIndex = 1;
                }
                if (product.ProductStatus == "Завершен")
                {
                    ComboBoxProductStatus.SelectedIndex = 0;
                }
            }
        }

        private void ButtonEditProduct_Click(object sender, RoutedEventArgs e)
        {
            var query = from product in context.Product where product.ProductArticleNumber == Key select product;

            foreach (Product product in query)
            {
                product.ProductArticleNumber = TextBoxProductArticleNumber.Text;
                product.ProductName = TextBoxProductName.Text;
                product.ProductDescription = TextBoxProductDescription.Text;
                product.ProductCategory = ComboBoxProductCategory.Text;
                product.ProductManufacturer = ComboBoxProductManufacturer.Text;
                product.ProductCost = Convert.ToDecimal(TextBoxProductCost.Text);
                product.ProductDiscountAmount = Convert.ToInt32(TextBoxProductDiscountAmount.Text);
                product.ProductQuantityInStock = Convert.ToInt32(TextBoxProductQuantityInStock.Text);
                product.ProductStatus = ComboBoxProductStatus.Text;
                MessageBox.Show("Продукт <<" + product.ProductName + ">> успешно изменён!");
            }
            try
            {

                context.SaveChanges();
                clearFelds();
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void ButtonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            using (context)
            {
                Product product = context.Product.Where(p => p.ProductArticleNumber == Key).FirstOrDefault();
                var result = MessageBox.Show("Хотите удалить товар <<" + product.ProductName.ToString() + ">>?", "Удаление товара", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                }
                this.Close();
            }
        }
    }
}
