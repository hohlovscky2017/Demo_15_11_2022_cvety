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

namespace Demo_15_11_2022_cvety.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private cvetyEntities context = new cvetyEntities();
        public AddProductWindow()
        {
            InitializeComponent();
            ComboBoxProductCategory.ItemsSource = context.Product.GroupBy(product => product.ProductCategory).ToList();
            ComboBoxProductManufacturer.ItemsSource = context.Product.GroupBy(product => product.ProductManufacturer).ToList();
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
        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            context.Product.Add(new Product() {
                ProductArticleNumber = TextBoxProductArticleNumber.Text,
                ProductName = TextBoxProductName.Text,
                ProductDescription = TextBoxProductDescription.Text,
                ProductCategory = ComboBoxProductCategory.Text,
                ProductManufacturer = ComboBoxProductManufacturer.Text,
                ProductCost = Convert.ToDecimal(TextBoxProductCost.Text),
                ProductDiscountAmount = Convert.ToInt32(TextBoxProductDiscountAmount.Text),
                ProductQuantityInStock = Convert.ToInt32(TextBoxProductQuantityInStock.Text),
                ProductStatus = "Новый"
            });
            context.SaveChanges();
            MessageBox.Show("Продукт <<" + TextBoxProductName.Text + ">> успешно добавлен в систему!");
            clearFelds();
            //this.Close();
        }
    }
}
