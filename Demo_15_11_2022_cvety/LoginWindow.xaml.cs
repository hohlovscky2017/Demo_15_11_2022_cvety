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
            if (string.IsNullOrEmpty(TextBoxUsername.Text) || string.IsNullOrEmpty(TextBoxPassword.Text)) // если поля textBox1 и textBox2, что соответствует логину и паролю, то выводим сообщение об ошибке
            {
                MessageBox.Show("Введите логин и пароль!"); // вывод сообщение об ошибке и прерываем выполнение программы
                return; // прерываем выполнение программы
            }
            using (var db = new cvetyEntities()) // записываем в переменную db подключение к базе данных
            {
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.UserLogin == TextBoxUsername.Text && u.UserPassword == TextBoxPassword.Text);
                MessageBox.Show("Добро пожаловать " + user.Role.RoleName + " " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic);

                if (user == null) // если переменная users равна null, то есть ничему это означает, что пользователя с такими данными в базе данных не существует, значит выводим сообщение об ошибке
                {
                    MessageBox.Show("Польователь с такими данными не найден!"); // вывод сообщение об ошибке и прерываем выполнение программы
                    return; // прерываем выполнение программы
                }                
            }
        }
    }
}
