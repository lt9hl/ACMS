using ACMS.ApplicationData;
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

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для KeysFr.xaml
    /// </summary>
    public partial class KeysFr : Page
    {
        public KeysFr()
        {
            InitializeComponent();

            var listKeys = AppConnect.modelOdb.Keys.ToList();

            listAllKeys.ItemsSource = listKeys;
        }

        private void delSelected(object sender, RoutedEventArgs e)
        {

        }

        private void editButt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
