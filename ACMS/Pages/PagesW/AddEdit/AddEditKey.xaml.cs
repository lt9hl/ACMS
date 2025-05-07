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

namespace ACMS.Pages.PagesW.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddEditKey.xaml
    /// </summary>
    public partial class AddEditKey : Page
    {
        public AddEditKey()
        {
            InitializeComponent();

            var allEmployees = AppConnect.modelOdb.Employees.ToList();
            foreach (var employee in allEmployees) 
                EmployeeName.Items.Add(employee.Secondname + " " + employee.Firstname[0] + ". " + employee.Patronymic[0] + ".");
            
            var allAccessLevels = AppConnect.modelOdb.AccessLevels.ToList();
            foreach (var accessLevel in allAccessLevels)
                AccessLevelInp.Items.Add(accessLevel.TitleLevel);

            var allWorkSchedules = AppConnect.modelOdb.WorkSchedules.ToList();
            foreach (var workSchedule in allWorkSchedules)
                WorkScheduleInp.Items.Add(workSchedule.TitleSchedule);

        }

        private void addNewKey(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = EmployeeName.SelectedIndex;
                var key = KeyInp.Text;
                var accessLevel = AccessLevelInp.SelectedIndex;
                var workSchedule = WorkScheduleInp.SelectedIndex;

                if (employee != -1 && key != "" && accessLevel != -1 && workSchedule != -1)
                {
                    var selectedEmployee = AppConnect.modelOdb.Employees.ToList()[employee];
                    var newKey = new Keys()
                    {
                        idEmployee = AppConnect.modelOdb.Employees.ToList()[employee].idEmployee,
                        KeyCard = key,
                        idAccessLevel = AppConnect.modelOdb.AccessLevels.ToList()[accessLevel].idAccessLevel,
                        idWorkSchedule = AppConnect.modelOdb.WorkSchedules.ToList()[workSchedule].idWorkSchedule,
                    };
                    AppConnect.modelOdb.Keys.Add(newKey);
                    AppConnect.modelOdb.SaveChanges();

                    NavigationService.Navigate(new KeysFr());
                }
                else
                {
                    MessageBox.Show("Необходимо заполнить все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageError();
            }
            
        }

        private void EmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedEmployee = AppConnect.modelOdb.Employees.ToList()[EmployeeName.SelectedIndex];

                DepName.Text = selectedEmployee.Departments.TitleDepartment;
                OrgName.Text = selectedEmployee.Organizations.OrgName;
            }
            catch
            {
                MessageError();
            }
        }
        private void MessageError()
        {
            MessageBox.Show("Произошла критическая ошибка в работе приложения", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
