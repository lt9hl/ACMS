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

            countEmpPa.Content = AppConnect.modelOdb.Keys.ToArray().Length;
            countUsers.Content = AppConnect.modelOdb.Users.ToArray().Length;
            countEmp1.Content = AppConnect.modelOdb.Employees.ToArray().Length;

            listNewEmployees.ItemsSource = AppConnect.modelOdb.Employees.ToList();
        }


    }
}
