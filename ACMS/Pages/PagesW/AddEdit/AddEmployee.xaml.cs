using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using ACMS.Classes;

namespace ACMS.Pages.PagesW.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public Employees currEmployee;

        currentUserAndRemember currentUser = new currentUserAndRemember();
        public AddEmployee(Employees selecteEmployee, currentUserAndRemember userInp)
        {
            InitializeComponent();

            currentUser = userInp;

            addNewEmplButt.IsEnabled = false;

            var dep = AppConnect.modelOdb.Departments;
            var pos = AppConnect.modelOdb.Posts;
            var org = AppConnect.modelOdb.Organizations;

            foreach (var i in dep)
            {
                DepName.Items.Add(i.TitleDepartment);
            }
            foreach (var i in pos)
            {
                PostName.Items.Add(i.TitlePost);
            }
            foreach (var i in org)
            {
                OrgName.Items.Add(i.OrgName);
            }

            if (selecteEmployee == null)
            {
                labelEmpl.Content = "Создание";
                addNewEmplButt.Content = "Создать";
            }
            else if (selecteEmployee != null)
            {
                currEmployee = selecteEmployee;
                labelEmpl.Content = "Изменение";
                addNewEmplButt.Content = "Изменить";
                secondNInp.Text = currEmployee.Secondname;
                NameInp.Text = currEmployee.Firstname;
                PatronymicInp.Text = currEmployee.Patronymic;
                PostName.SelectedItem = currEmployee.Posts.TitlePost;
                DepName.SelectedItem = currEmployee.Departments.TitleDepartment;
                OrgName.SelectedItem = currEmployee.Organizations.OrgName;
            }
        }

        private void addNewEmpl(object sender, RoutedEventArgs e)
        {
            try
            {
                Employees employeeAddEdit = new Employees();

                if (currEmployee != null)
                    employeeAddEdit = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.idEmployee == currEmployee.idEmployee);

                employeeAddEdit.Firstname = NameInp.Text;
                employeeAddEdit.Secondname = secondNInp.Text;
                employeeAddEdit.Patronymic = PatronymicInp.Text;
                employeeAddEdit.idPost = AppConnect.modelOdb.Posts.FirstOrDefault(x => x.TitlePost == PostName.Text).idPost;
                employeeAddEdit.idOrganization = AppConnect.modelOdb.Organizations.FirstOrDefault(x => x.OrgName == OrgName.Text).idOrganization;
                employeeAddEdit.idDepartment = AppConnect.modelOdb.Departments.FirstOrDefault(x => x.TitleDepartment == DepName.Text).idDepartment;

                if (currEmployee != null)
                    MessageBox.Show($"Данные пользователя изменены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    AppConnect.modelOdb.Employees.Add(employeeAddEdit);
                    MessageBox.Show($"Пользователь  добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppConnect.modelOdb.SaveChanges();
                NavigationService.Navigate(new EmployeesFr(currentUser));

            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void secondN_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox();
        }
        private void checkTextBox()
        {
            if (NameInp.Text == "" || secondNInp.Text == "" || PatronymicInp.Text == "") addNewEmplButt.IsEnabled = false;
            else addNewEmplButt.IsEnabled = true;
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox();
        }

        private void Patron_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox();
        }
    }
}
