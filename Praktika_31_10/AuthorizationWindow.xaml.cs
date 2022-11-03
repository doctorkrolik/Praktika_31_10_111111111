using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Praktika_31_10
{
    /// <summary>
    /// Логика взаимодействия для AddUsersWindow.xaml
    /// </summary>
    public partial class AddUsersWindow : Window
    {
        public AddUsersWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var users = PaymentDBEntities.GetContext().users.ToList();

            string password = passwordTextBox.Text;
            string login = loginTextBox.Text;
            string hash;

            using(SHA256 sha256hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

            users user = users.Where(p => p.login == loginTextBox.Text && p.password.ToLower() == hash.ToLower()).FirstOrDefault();
            if(user != null)
            {
                MainWindow main = new MainWindow();
                main.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверно введен логин или пароль");
            }
        }
    }
}
