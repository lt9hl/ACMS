
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

using ACMS.Classes;
using ACMS.ApplicationData;

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class LoginI : Page
    {
        currentUserAndRemember currentUser = new currentUserAndRemember();
        Users user = new Users();

        public LoginI(currentUserAndRemember userInp) {
             InitializeComponent();

             LoginButton.IsEnabled = false;

            user = AppConnect.modelOdb.Users.First(x => x.idUser == userInp.currentUserId);
            currentUser = userInp;

            if(currentUser.rememberUser) {
                LoginInp.Text = user.Login.ToString();
                PassInp.Password = user.Password;
            }

        }
        private void toReg(object sender, RoutedEventArgs e)
        {
            AppFrame.PMain.Navigate(new Reg());
        }

        private void LoginButt_Click(object sender, RoutedEventArgs e)
            {
            try
            {
                string logInp = LoginInp.Text.Trim();
                string passInp = PassInp.Password;
                var inputUser = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Password == passInp && logInp == x.Login);

                if (inputUser != null)
                {
                    if (CheckRememMe.IsChecked == true)
                        currentUser.rememberUser = true;

                     currentUser.currentUserId = inputUser.idUser;

                    NavigationService.Navigate(new StartPage(currentUser));
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
            checkInp();
            }
            private void LoginInp_TextChanged(object sender, TextChangedEventArgs e)
            {
            checkInp();
            }

        public void checkInp() {
            if (PassInp.Password.Length > 0 && LoginInp.Text.Length > 0)
                LoginButton.IsEnabled = true;
            else
                LoginButton.IsEnabled = false;
        }



        private void CheckRememMe_Checked(object sender, RoutedEventArgs e)
        {
            
        }


        private void LoginGuestButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage(currentUser));
        }

    }
}

