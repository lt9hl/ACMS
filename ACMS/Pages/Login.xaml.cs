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
        getC userPermission = new getC();
        private void toReg(object sender, RoutedEventArgs e)
        { 
            AppFrame.PMain.Navigate(new Reg());
        }

        string[] remUser = { "" , "" };
        public LoginI() {
             InitializeComponent();
             LoginButton.IsEnabled = false;


        }
        
            private void LoginButt_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                string logInp = LoginInp.Text.Trim();
                string passInp = PassInp.Password;

                var user = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Password == passInp && logInp == x.Login);

                if (user != null)
                {

                    userPermission.currPermission = user.Permissions.TitlePersmission;

                    NavigationService.Navigate(new StartPage(userPermission.currPermission));
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
                LoginButton.IsEnabled = false;
                else
                LoginButton.IsEnabled = true;
            }


        private void LoginInp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginInp.Text == "")
                LoginButton.IsEnabled = false;
            else
                LoginButton.IsEnabled = true;
        }

        private void CheckRememMe_Checked(object sender, RoutedEventArgs e)
        {
            
        }


        private void LoginGuestButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage("Гость"));
        }
    }
}

