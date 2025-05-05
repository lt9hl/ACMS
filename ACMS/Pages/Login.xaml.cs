
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using ACMS;

using ACMS.ApplicationData;
using ACMS.Properties;

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class LoginI : Page
    {
        public LoginI() {
             InitializeComponent();
            var currentUser = AppConnect.modelOdb.CurrentUser.OrderByDescending(x => x.idCurrentUser).ToList()[0];

            LoginButton.IsEnabled = false;

            if (currentUser.SaveOrNo == 1)
            {
                LoginInp.Text = currentUser.Users.Login.ToString();
                PassInp.Password = currentUser.Users.Password;
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
                

                if (inputUser != null )
                {
                    var whiteList = AppConnect.modelOdb.BlackList.FirstOrDefault(x => x.idUser == inputUser.idUser);
                    if (whiteList == null)
                    {
                        var newCurrentUser = new CurrentUser()
                        {
                            idUser = inputUser.idUser,
                            SaveOrNo = 0
                        };

                        if (CheckRememMe.IsChecked == true)
                        {
                            newCurrentUser = new CurrentUser
                            {
                                idUser = inputUser.idUser,
                                SaveOrNo = 1
                            };

                        }

                        AppConnect.modelOdb.CurrentUser.Add(newCurrentUser);
                        AppConnect.modelOdb.SaveChanges();

                        NavigationService.Navigate(new StartPage());
                    }
                    else
                    {
                        MessageBox.Show("Пользователь заблокирован","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                    
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

            var newCurrentUser = new CurrentUser()
            {
                idUser = 6,
                SaveOrNo = 0
            };
           
            AppConnect.modelOdb.CurrentUser.Add(newCurrentUser);
            AppConnect.modelOdb.SaveChanges();

            NavigationService.Navigate(new StartPage());
        }

    }
}

