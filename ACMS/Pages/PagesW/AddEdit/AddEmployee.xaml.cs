using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
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
using iTextSharp.text;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace ACMS.Pages.PagesW.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public Employees currentEmployee;

        public AddEmployee(Employees selectedEmployee)
        {
            InitializeComponent();

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

            if (selectedEmployee == null)
            {
                labelEmpl.Content = "Создание";
                addNewEmplButt.Content = "Создать";
            }
            else if (selectedEmployee != null)
            {
                currentEmployee = selectedEmployee;
                labelEmpl.Content = "Изменение";
                addNewEmplButt.Content = "Изменить";
                secondNInp.Text = currentEmployee.Secondname;
                NameInp.Text = currentEmployee.Firstname;
                PatronymicInp.Text = currentEmployee.Patronymic;
                PostName.SelectedItem = currentEmployee.Posts.TitlePost;
                DepName.SelectedItem = currentEmployee.Departments.TitleDepartment;
                OrgName.SelectedItem = currentEmployee.Organizations.OrgName;
                

            }
        }
        Employees employeeAddEdit = new Employees();
        private void addNewEmpl(object sender, RoutedEventArgs e)
        {
            try
            {

                if (currentEmployee != null)
                    employeeAddEdit = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.idEmployee == currentEmployee.idEmployee);

                employeeAddEdit.Firstname = NameInp.Text;
                employeeAddEdit.Secondname = secondNInp.Text;
                employeeAddEdit.Patronymic = PatronymicInp.Text;
                employeeAddEdit.idPost = AppConnect.modelOdb.Posts.FirstOrDefault(x => x.TitlePost == PostName.Text).idPost;
                employeeAddEdit.idOrganization = AppConnect.modelOdb.Organizations.FirstOrDefault(x => x.OrgName == OrgName.Text).idOrganization;
                employeeAddEdit.idDepartment = AppConnect.modelOdb.Departments.FirstOrDefault(x => x.TitleDepartment == DepName.Text).idDepartment;

                if (currentEmployee != null)
                    MessageBox.Show($"Данные пользователя изменены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    AppConnect.modelOdb.Employees.Add(employeeAddEdit);
                    MessageBox.Show($"Пользователь  добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppConnect.modelOdb.SaveChanges();
                NavigationService.Navigate(new EmployeesFr());

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

        public string fullPathToFile;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            string directory;
            directory = dialog.FileName.Substring(dialog.FileName.LastIndexOf('\\'), dialog.FileName.Length - dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')).Length);

            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Images\\EmployeesPhoto\\Standart\\" + directory))
            {
                File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Images\\EmployeesPhoto\\Standart\\" + directory);
            }

            File.Copy(dialog.FileName, System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Images\\EmployeesPhoto\\Standart\\" + directory);

            employeeAddEdit = AppConnect.modelOdb.Employees.FirstOrDefault(x => x.idEmployee == currentEmployee.idEmployee);

            employeeAddEdit.photo = System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Images\\EmployeesPhoto\\Standart\\" + directory;
            AppConnect.modelOdb.SaveChanges();

            if (employeeAddEdit != null)
            {
                DataContext = null;
                DataContext = employeeAddEdit;
            }
            var namePicture = employeeAddEdit.photo;
            employeePhotoSelect.Source = new BitmapImage(new Uri(namePicture));


        }
    }
}
