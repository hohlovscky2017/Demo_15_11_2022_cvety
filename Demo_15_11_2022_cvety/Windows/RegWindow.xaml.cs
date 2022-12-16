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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private cvetyEntities context = new cvetyEntities();
        public RegWindow()
        {
            InitializeComponent();
            Role.ItemsSource = context.Role.Select(Role => Role.RoleName).ToList();

        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {            
            using (var db = new cvetyEntities()) // записываем в переменную db подключение к базе данных
            {
                try
                {
                    string Password = "";
                    if (PasswordBoxPassword.Password == PasswordBoxFirstPassword.Password)
                    {
                        Password = PasswordBoxFirstPassword.Password;
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");                        
                        return;
                    }
                    db.User.Add(new User() {
                        UserSurname = TextBoxUserSurname.Text,
                        UserName = TextBoxUserName.Text,
                        UserPatronymic = TextBoxUserPatronymic.Text,
                        UserLogin = TextBoxUsername.Text,
                        UserPassword = Password,
                        UserRole = Role.SelectedIndex + 1
                    });
                    db.SaveChanges();                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка"); // вывод сообщение об ошибке и прерываем выполнение программы
                    return; // прерываем выполнение программы
                }
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
