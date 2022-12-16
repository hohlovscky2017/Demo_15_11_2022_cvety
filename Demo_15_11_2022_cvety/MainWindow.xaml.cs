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
    

        public MainWindow()
        {
            InitializeComponent();
            ListProduct.ItemsSource = _context.Product.OrderBy(product => product.ProductName).ToList();
            
        }
    }
}
