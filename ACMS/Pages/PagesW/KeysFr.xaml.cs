using ACMS.ApplicationData;
using ACMS.Pages.PagesW.AddEdit;
using Aspose.BarCode.Generation;
using iTextSharp.text;
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

            sortSelect.Items.Add("Сортировка");
            sortSelect.SelectedIndex = 0;

            sortSelect.Items.Add("По имени");
            sortSelect.Items.Add("По фамилии");

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
            listAllKeys.ItemsSource = keysList();
        }


        int count = 0;
        private void SortButtClick(object sender, RoutedEventArgs e)
        {
            count++;
            listAllKeys.ItemsSource = keysList();
        }
        private void sortSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAllKeys.ItemsSource = keysList();
        }
        Keys[] keysList()
        {
            try
            {

                var allKeysList = AppConnect.modelOdb.Keys.ToList();

                if (searchBox.Text.Length > 0)
                {
                    allKeysList = allKeysList.Where(x => x.Employees.Firstname.ToLower().Contains(searchBox.Text.ToLower()) || x.Employees.Secondname.ToLower().Contains(searchBox.Text.ToLower()) || x.KeyCard.ToLower().Contains(searchBox.Text.ToLower())).ToList();
                }

                if (sortSelect.SelectedIndex > 0)
                {
                    switch (sortSelect.SelectedIndex)
                    {
                        case 1:
                            allKeysList = allKeysList.OrderBy(x => x.Employees.Firstname).ToList();
                            break;
                        case 2:
                            allKeysList = allKeysList.OrderBy(x => x.Employees.Secondname).ToList();
                            break;

                    }
                }


                if (count % 2 == 1)
                {
                    switch (sortSelect.SelectedIndex)
                    {
                        case 1:
                            allKeysList = allKeysList.OrderByDescending(x => x.Employees.Firstname).ToList();
                            break;
                        case 2:
                            allKeysList = allKeysList.OrderByDescending(x => x.Employees.Secondname).ToList();
                            break;
                    }

                }

                return allKeysList.ToArray();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при работе приложения {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


    }
}
