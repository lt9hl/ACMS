using ACMS.ApplicationData;
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

namespace ACMS.Pages.PagesW.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private Employees newEmp = new Employees();
        public AddEmployee()
        {
            InitializeComponent();
            var getEmp = new getC();
            DataContext = newEmp;

            addNewEmplButt.IsEnabled = false; 

            foreach(Departments  i in AppConnect.modelOdb.Departments)
            {
                DepName.Items.Add(i.TitleDep);
            }  
            foreach (Posts i in AppConnect.modelOdb.Posts)
            {
                PostName.Items.Add(i.TitleP);
            }
            foreach (Organizations i in AppConnect.modelOdb.Organizations)
            {
                OrgName.Items.Add(i.OrgName);
            }
        }

        private void addNewEmpl(object sender, RoutedEventArgs e)
        {
            try
            {

               var ddep = AppConnect.modelOdb.Departments.FirstOrDefault(x => x == newEmp.Departments);

                MessageBox.Show($"{newEmp.Fistname} {newEmp.Patronymic} {ddep.TitleDep.ToString()} {newEmp.idD} {newEmp.idO}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
                AppConnect.modelOdb.Employees.Add(newEmp);
                AppConnect.modelOdb.SaveChanges();
                
                MessageBox.Show($"Пользователь {newEmp.Secondname} {newEmp.Fistname} добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении пользователя","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void secondN_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTextBox();
        }
        private void checkTextBox()
        {
            if (Name.Text == "" || Patron.Text == "" || secondN.Text == "" ) addNewEmplButt.IsEnabled = false;
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
