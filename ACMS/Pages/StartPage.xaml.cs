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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ACMS.ApplicationData;
using ACMS.Pages.PagesW;

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            AppFrame.FWork = FrStart;
        }

        private void BackToLogin(object sender, RoutedEventArgs e)
        {
            AppFrame.PMain.Navigate(new LoginI());
        }

        private void toDoorsFr(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new DoorsFr());
        }

        private void toRepFr(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new ReportFr());
        }

        private void toPayFr(object sender, RoutedEventArgs e)
        {

        }

        private void toEntFr(object sender, RoutedEventArgs e)
        {

        }

        private void toUserFr(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new UsersFr());
        }

        private void toEmpFr(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new EmployeesFr());
        }
    }
}
