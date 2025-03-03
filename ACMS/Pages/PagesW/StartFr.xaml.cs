using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
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
using System.Data.SqlClient;
using ACMS.Properties;
using System.Diagnostics.Eventing.Reader;

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для StartFr.xaml
    /// </summary>
    public partial class StartFr : Page
    {
 
        public StartFr()
        {

            InitializeComponent();

            int cP = AppConnect.modelOdb.Keys.ToArray().Length;
            int cU = AppConnect.modelOdb.Users.ToArray().Length;
            int cE = AppConnect.modelOdb.Employees.ToArray().Length;

            countEmpPa.Content = cP;
            countUsers.Content = cU;
            countEmp1.Content = cE;

            listNewEmployees.ItemsSource = Emp();
        }
        Employees[] Emp()
        {
            try
            {
                List<Employees> empl = AppConnect.modelOdb.Employees.ToList();
                return empl.ToArray();
            }
            catch
            {
                MessageBox.Show($"dfdf", "", MessageBoxButton.OK, MessageBoxImage.Information);

                return null;
            }

        }

    }
}
