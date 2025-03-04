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

            var dep = AppConnect.modelOdb.Departments;
            var pos = AppConnect.modelOdb.Posts;
            var org = AppConnect.modelOdb.Organizations;

            foreach(var i in dep)
            {
                DepName.Items.Add(i.TitleDep);
            }  
            foreach (var i in pos)
            {
                PostName.Items.Add(i.TitleP);
            }
            foreach (var i in org)
            {
                OrgName.Items.Add(i.OrgName);
            }
        }

        private void addNewEmpl(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelOrg = AppConnect.modelOdb.Organizations.FirstOrDefault(x => x.OrgName == newEmp.Organizations.OrgName);
                var SelPost = AppConnect.modelOdb.Posts.FirstOrDefault(x => x.TitleP == newEmp.Posts.TitleP);
                var SelDep = AppConnect.modelOdb.Departments.FirstOrDefault(x => x.TitleDep == newEmp.Departments.TitleDep);


                Employees epnObd = new Employees()
                {
                    Fistname = newEmp.Fistname,
                    Secondname = newEmp.Secondname,
                    Patronymic = newEmp.Patronymic,
                    idD = Convert.ToInt32(SelDep.idD),
                    idO = Convert.ToInt32(SelOrg.idO),
                    idP = Convert.ToInt32(SelPost.idP)
                };

                MessageBox.Show($"Пользователь  добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);


                AppConnect.modelOdb.Employees.Add(epnObd);
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
