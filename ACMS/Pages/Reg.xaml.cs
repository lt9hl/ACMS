using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Media.Animation;
using ACMS.ApplicationData;
using System.Windows.Media.Media3D;

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
            RegistrDone.IsEnabled = false;
        }


        private void toLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegistrDone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string inpPass = Pass.Password;
                string inpRepPass = RepPass.Password;
                string inpLogin = LoginI.Text.Trim();
                string inpEmail = Email.Text.Trim();

                var user = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Login == inpLogin);

                Pass.Background = Brushes.Transparent;
                RepPass.Background = Brushes.Transparent;
                LoginI.Background = Brushes.Transparent;
                Email.Background = Brushes.Transparent;

                if (user != null)
                {
                    LoginI.Background = new SolidColorBrush(Color.FromRgb(202, 63, 40));
                    MessageBox.Show("Имя пользователя занято", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (inpPass.Length < 8 || inpRepPass.Length < 8)
                {
                    Pass.Background = Brushes.DarkRed;
                    RepPass.Background = Brushes.DarkRed;

                    MessageBox.Show("Длинна пароля меньше 8 символов", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(!inpEmail.Contains("@") || !inpEmail.Contains("."))
                {
                    MessageBox.Show("Почта заполнена неправильно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    Email.Background = Brushes.DarkRed;
                }
                else
                {
                    Users userOdb = new Users()
                    {
                        Login = inpLogin,
                        Password = inpPass,
                        email = inpEmail
                    };
                    AppConnect.modelOdb.Users.Add(userOdb);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show($"Пользователь {inpLogin} добавлен","Уведомление", MessageBoxButton.OK,MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Критическая ошибка в работе приложения\n{ex.Message}","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        private void RepPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkPass();
        }
           
        void checkPass()
        {
            if (RepPass.Password != Pass.Password || RepPass.Password == null || Pass.Password == null )
                RegistrDone.IsEnabled = false;
            else
                RegistrDone.IsEnabled = true;
        }

        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkPass();
        }

        private void LoginI_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginI.Text != null) RegistrDone.IsEnabled = false;
            else RegistrDone.IsEnabled = true;
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Email.Text != null) RegistrDone.IsEnabled = false;
            else RegistrDone.IsEnabled = true;
        }
    }
}
