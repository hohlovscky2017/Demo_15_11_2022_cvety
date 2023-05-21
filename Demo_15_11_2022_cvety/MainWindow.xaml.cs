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

namespace Demo_15_11_2022_cvety
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private cvetyEntities _context = new cvetyEntities();
        private User LoginUser;
        private bool GuestUser;

        public MainWindow()
        {
            InitializeComponent();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
        }
        public MainWindow(User user = null, bool guest = false)
        {
            InitializeComponent();
            LoginUser = user;
            GuestUser = guest;
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
            Refresh();
        }
        private void Refresh()
        {
            _context = null;
            _context = new cvetyEntities();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
        }
        private void ListProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LoginUser != null)
            {
                if (LoginUser.Role.RoleName == "Администратор")
                {
                    string Key = ((Product)ListProduct.SelectedItem).ProductArticleNumber;
                    Windows.EditDeleteProductWindow editDeleteProductWindow = new Windows.EditDeleteProductWindow(Key);
                    editDeleteProductWindow.ShowDialog();
                    Refresh();
                }
                if (LoginUser.Role.RoleName == "Менеджер" || LoginUser.Role.RoleName == "Клиент")
                {
                    MessageBox.Show("У Вас недостаточно прав для выполнения этой операции.");
                }
            }
            if (GuestUser == true)
            {
                MessageBox.Show("Вы зарегистрирована как гость. У Вас недостаточно прав для выполнения этой операции.");
            }
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (LoginUser != null)
            {
                if (LoginUser.Role.RoleName == "Администратор")
                {
                    Windows.AddProductWindow addProductWindow = new Windows.AddProductWindow();
                    addProductWindow.ShowDialog();
                    Refresh();
                }
                if (LoginUser.Role.RoleName == "Менеджер" || LoginUser.Role.RoleName == "Клиент")
                {
                    MessageBox.Show("У Вас недостаточно прав для выполнения этой операции.");
                }
            }
            if (GuestUser == true)
            {
                MessageBox.Show("Вы зарегистрирована как гость. У Вас недостаточно прав для выполнения этой операции.");
            }
        }
    }
}
