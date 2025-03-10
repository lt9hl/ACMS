
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
using ACMS.ApplicationData;

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для UsersFr.xaml
    /// </summary>
    public partial class UsersFr : Page
    {
        public UsersFr()
        {
            InitializeComponent();
            listUsers.ItemsSource = AppConnect.modelOdb.Users.ToList();
        }

        private void delSelected(object sender, RoutedEventArgs e)
        {
            funDelete();
            listUsers.ItemsSource = AppConnect.modelOdb.Users.ToList();
            
        }
        public void funDelete()
        {
            var ObjForRemoving = listUsers.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {ObjForRemoving.Count} пользователей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.modelOdb.Users.RemoveRange(ObjForRemoving);
                    AppConnect.modelOdb.SaveChanges();

                    listUsers.ItemsSource = AppConnect.modelOdb.Employees.ToList();

                    MessageBox.Show("Пользователи успешно удалены");
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            listUsers.ItemsSource = findEmpl();  
        }
        Employees[] findEmpl()
        {
            try
            {
                var empAll = AppConnect.modelOdb.Employees.ToList();
                if (searchBox.Text.Length > 0)
                {
                    empAll = empAll.Where(x => x.Firstname.ToLower().Contains(searchBox.Text.ToLower()) || x.Secondname.ToLower().Contains(searchBox.Text.ToLower()) ||
                    x.Patronymic.ToLower().Contains(searchBox.Text.ToLower()) || x.Posts.TitlePost.ToLower().Contains(searchBox.Text.ToLower()) ||
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
    }
}
