using ACMS.ApplicationData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class LoginI : Page
    {
        private void toReg(object sender, RoutedEventArgs e)
        { 
            AppFrame.PMain.Navigate(new Reg());
        }
            

            public LoginI() {
                InitializeComponent();
                LoginButt.IsEnabled = false;
            }

            private void LoginButt_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                string logInp = LoginInp.Text;
                string passInp = PassInp.Password;

                var user = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Password == passInp && logInp == x.Login);

                if (user != null)
                {
                    NavigationService.Navigate(new StartPage());
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginInp.Text = "";
                    PassInp.Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка в работе приложения\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }



            private void toRegButt_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.Navigate(new Reg());
            }

            private void PassInp_PasswordChanged(object sender, RoutedEventArgs e)
            {

                if (PassInp.Password == "")
                    LoginButt.IsEnabled = false;
                else
                    LoginButt.IsEnabled = true;
            }


        private void LoginInp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginInp.Text == "")
                LoginButt.IsEnabled = false;
            else
                LoginButt.IsEnabled = true;
        }
    }
}

