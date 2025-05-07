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

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для ReportDay.xaml
    /// </summary>
    public partial class ReportDay : Page
    {
        public ReportDay()
        {
            InitializeComponent();

            var emplList = AppConnect.modelOdb.Employees.ToList();
            


        }
        private void SelectReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dateEployees.SelectedDate != null)
                {
                    var selectedDate = dateEployees.SelectedDate;
                    var listDoors = AppConnect.modelOdb.DoorsEmployees.Where(x => x.EnterDateTime == selectedDate.Value).ToList();

                    listEmployees.ItemsSource = listDoors;
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать дату","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }
    }
}
