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
using ACMS.Pages;
using ACMS.Pages.PagesW.AddEdit;

namespace ACMS.Pages.PagesW
{

    /// <summary>
    /// Логика взаимодействия для EmployeesFr.xaml
    /// </summary>
    public partial class EmployeesFr : Page
    {

        public EmployeesFr()
        {
            InitializeComponent();


            var getEmp = new getC();
            listEmpl.ItemsSource = getEmp.Emp();
        }

        private void delSelected(object sender, RoutedEventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listEmpl.ItemsSource = findEmpl();
        }
        Employees[] findEmpl()
        {
            try
            {
                var empAll = AppConnect.modelOdb.Employees.ToList();
                if(searchBox.Text.Length > 0)
                {
                    empAll = empAll.Where(x => x.Fistname.ToLower().Contains(searchBox.Text.ToLower()) || x.Secondname.ToLower().Contains(searchBox.Text.ToLower()) || 
                    x.Patronymic.ToLower().Contains(searchBox.Text.ToLower()) || x.Posts.TitleP.ToLower().Contains(searchBox.Text.ToLower()) ||
                    x.Organizations.OrgName.ToLower().Contains(searchBox.Text.ToLower())).ToList();

                }
                return empAll.ToArray();

            }
            catch
            {
                MessageBox.Show("Ошибка при работе приложения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void addButt_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new AddEmployee());
        }
    }
}
