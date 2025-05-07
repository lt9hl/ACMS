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

namespace ACMS.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportEmployee.xaml
    /// </summary>
    public partial class ReportEmployee : Page
    {
        public ReportEmployee()
        {
            InitializeComponent();

            var emplList = AppConnect.modelOdb.Employees.ToList();

            string currentEmployeeName;
            foreach (var employee in emplList)
            {
                currentEmployeeName = employee.Secondname + " " + employee.Firstname.ToString()[0] + ". " + employee.Patronymic[0] + ".";
                employeesName.Items.Add(currentEmployeeName);
            }
        }
        private void SelectReport_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (employeesName.SelectedIndex != -1)
                {
                    string selectedEmployee = employeesName.Text;
                    string selectedEmployeeTrim = "";

                    for (int i = 0; i < selectedEmployee.Length; i++)
                    {
                        if (selectedEmployee[i] != ' ')
                            selectedEmployeeTrim += selectedEmployee[i];
                        else
                            break;
                    }

                    var currentEmployee = AppConnect.modelOdb.Employees.First(x => selectedEmployeeTrim == x.Secondname);

                    if (currentEmployee != null)
                    {
                        var report = AppConnect.modelOdb.DoorsEmployees.Where(x => x.idEmployee == currentEmployee.idEmployee);

                        listEmployees.ItemsSource = report.ToList();
                    }
                    else
                    {
                        employeesName.Background = Brushes.DarkRed;
                    }

                }
                else
                {
                    MessageBox.Show("Необходимо выбрать сотрудника","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }

                   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }

        }


    }
}
