using ACMS.ApplicationData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для ReportFr.xaml
    /// </summary>
    public partial class ReportFr : Page
    {
        public ReportFr()
        {
            InitializeComponent();

            AppFrame.ReportFrDay = ReportFrDay;
        }

        private void GoToAnotherReport(object sender, RoutedEventArgs e)
        {
            AppFrame.ReportFrDay.Navigate(new ReportEmployee());
        }

        private void GoBackReport(object sender, RoutedEventArgs e)
        {
            AppFrame.ReportFrDay.Navigate(new ReportDay());
        }
    }
}
