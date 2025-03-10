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
using System.Runtime.Serialization;

namespace ACMS.Pages.PagesW
{

    /// <summary>
    /// Логика взаимодействия для EmployeesFr.xaml
    /// </summary>
    public partial class EmployeesFr : Page
    {
        int count = 0;
        public EmployeesFr(string permission)
        {
            InitializeComponent();

            if (permission == "Гость")
            {
                addButt.IsEnabled = false;
                delButt.IsEnabled = false;
                editButt.IsEnabled = false;
            }
            if (permission == "Пользователь")
            {
                addButt.IsEnabled = false;
            }

            sortSelect.Items.Add("Сортировка");
            sortSelect.SelectedIndex = 0;

            sortSelect.Items.Add("По имени");
            sortSelect.Items.Add("По фамилии");
            sortSelect.Items.Add("По должности");

            postSelect.Items.Add("Должность");
            postSelect.SelectedIndex = 0;

            var posts = AppConnect.modelOdb.Posts.ToList();
            foreach (var post in posts)
                postSelect.Items.Add(post.TitlePost);


            listAllEmpl.ItemsSource = AppConnect.modelOdb.Employees.ToList();

        }

        private void delSelected(object sender, RoutedEventArgs e)
        {
            funDelete(listAllEmpl);
        }
        public void funDelete(ListView curListView)
        {
            var ObjForRemoving = curListView.SelectedItems.Cast<Employees>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {ObjForRemoving.Count} сотрудников", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.modelOdb.Employees.RemoveRange(ObjForRemoving);
                    AppConnect.modelOdb.SaveChanges();

                    curListView.ItemsSource = AppConnect.modelOdb.Employees.ToList();

                    MessageBox.Show("Сотрудники успешно удалены");
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listAllEmpl.ItemsSource = employeesList();
        }


        private void addButt_Click(object sender, RoutedEventArgs e)
        {
                AppFrame.FWork.Navigate(new AddEmployee(null));
        }

        private void editButt_Click(object sender, RoutedEventArgs e)
        {
            int selectedItem = listAllEmpl.SelectedIndex;
            if(selectedItem != -1)
            { 
                AppFrame.FWork.Navigate(new AddEmployee(listAllEmpl.SelectedItem as Employees));
            }
            
        }

        private void SortButtClick(object sender, RoutedEventArgs e)
        { 
            count++;
            listAllEmpl.ItemsSource = employeesList();
        }

        private void sortSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAllEmpl.ItemsSource = employeesList();
        }
        Employees[] employeesList()
        {
            try
            {
                var empList = AppConnect.modelOdb.Employees.ToList();
               
                if (searchBox.Text.Length > 0)
                {
                    empList = empList.Where(x => x.Firstname.ToLower().Contains(searchBox.Text.ToLower()) || x.Secondname.ToLower().Contains(searchBox.Text.ToLower()) ||
                    x.Patronymic.ToLower().Contains(searchBox.Text.ToLower()) || x.Posts.TitlePost.ToLower().Contains(searchBox.Text.ToLower()) ||
                    x.Organizations.OrgName.ToLower().Contains(searchBox.Text.ToLower())).ToList();

                }

            if (sortSelect.SelectedIndex > 0)
            {
                switch (sortSelect.SelectedIndex) {
                    case 1:
                        empList = empList.OrderBy(x => x.Firstname).ToList();
                        break;
                    case 2:
                        empList = empList.OrderBy(x => x.Secondname).ToList();
                        break;
                    case 3:
                        empList = empList.OrderBy(x => x.Posts.idPost).ToList();
                        break;
                }
            }

            if(postSelect.SelectedIndex > 0)
                {
                    var selectedd = AppConnect.modelOdb.Posts.FirstOrDefault(x => x.TitlePost == postSelect.Text).idPost;

                    empList = empList.Where(x => x.idPost == selectedd).ToList();
                }

            if (count % 2 == 1)
            {
                switch (sortSelect.SelectedIndex)
                {
                    case 1:
                        empList = empList.OrderByDescending(x => x.Firstname).ToList();
                        break;
                    case 2:
                        empList = empList.OrderByDescending(x => x.Secondname).ToList();
                        break;
                    case 3:
                        empList = empList.OrderByDescending(x => x.Posts.idPost).ToList();
                        break;
                }
                
            }

            return empList.ToArray();

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при работе приложения {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
