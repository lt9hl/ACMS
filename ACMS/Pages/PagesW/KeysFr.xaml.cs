using ACMS.ApplicationData;
using ACMS.Pages.PagesW.AddEdit;
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

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для KeysFr.xaml
    /// </summary>
    public partial class KeysFr : Page
    {
        public KeysFr()
        {
            InitializeComponent();

            listAllKeys.ItemsSource = AppConnect.modelOdb.Keys.ToList();

        }

        private void delSelected(object sender, RoutedEventArgs e)
        {
            funDelete(listAllKeys);
        }

        public void funDelete(ListView curListView)
        {
            var ObjForRemoving = curListView.SelectedItems.Cast<Keys>().ToList();
            if(ObjForRemoving.Count > 0)
            {
                if (MessageBox.Show($"Вы точно хотите удалить {ObjForRemoving.Count} ключей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.modelOdb.Keys.RemoveRange(ObjForRemoving);
                        AppConnect.modelOdb.SaveChanges();

                        curListView.ItemsSource = AppConnect.modelOdb.Employees.ToList();

                        MessageBox.Show("Ключи успешно удалены");
                        listAllKeys.ItemsSource = AppConnect.modelOdb.Keys.ToList();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать объекты для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void addButt_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FWork.Navigate(new AddEditKey());
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sortSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SortButtClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
