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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxUsername.Text) || string.IsNullOrEmpty(PasswordBoxPassword.Password)) // если поля textBox1 и textBox2, что соответствует логину и паролю, то выводим сообщение об ошибке
            {
                MessageBox.Show("Введите логин и пароль!"); // вывод сообщение об ошибке и прерываем выполнение программы
                return; // прерываем выполнение программы
            }
            using (var db = new cvetyEntities()) // записываем в переменную db подключение к базе данных
            {
                try
                {
                    User user = db.User.AsNoTracking().FirstOrDefault(u => u.UserLogin == TextBoxUsername.Text && u.UserPassword == PasswordBoxPassword.Password);
                    MessageBox.Show("Добро пожаловать " + user.Role.RoleName + " " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic);
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Польователь с такими данными не найден!"); // вывод сообщение об ошибке и прерываем выполнение программы
                    return; // прерываем выполнение программы
                }      
            }
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            Windows.RegWindow regWindow = new Windows.RegWindow();
            regWindow.Show();
            this.Close();
        }
    }
}
