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

namespace ACMS.Pages.PagesW.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для AddPayment.xaml
    /// </summary>
    public partial class AddPayment : Page
    {
        public AddPayment()
        {
            InitializeComponent();

            var allTypesPayment = AppConnect.modelOdb.TypesPayments.ToList();
            var allProducts = AppConnect.modelOdb.Products.ToList();

            foreach (var type in allTypesPayment)
                TypePaymentName.Items.Add(type.Title);
                
            foreach (var product in allProducts)
                ProductName.Items.Add(product.Title);


        }

        private void addNewPayment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProductName.SelectedIndex != -1 && TypePaymentName.SelectedIndex != -1)
                {
                    var newPayment = new Payments
                    {
                        idProduct = AppConnect.modelOdb.Products.First(x => x.Title == ProductName.SelectedItem.ToString()).idProduct,
                        idTypePayment = AppConnect.modelOdb.TypesPayments.First(x => x.Title == TypePaymentName.SelectedItem.ToString()).idTypePayment
                    };

                    AppConnect.modelOdb.Payments.Add(newPayment);
                    AppConnect.modelOdb.SaveChanges();

                    MessageBox.Show("Оплата успешно проведена","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);

                    NavigationService.Navigate(new PaymentsFrame());
                }
                else
                {
                    MessageBox.Show("Необходимо заполнить все поля","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Information);
                }
            
            }catch
            {
                MessageBox.Show("Возникла ошибка при заполнении полей","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
           
            
        }

        private void ProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = AppConnect.modelOdb.Products.First(x => x.Title == ProductName.SelectedItem.ToString());
            CostInp.Text = product.Cost.ToString();
        }
    }
}
