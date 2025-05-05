using ACMS.ApplicationData;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using System.Windows.Shapes;

using iTextSharp.text.pdf;
using iTextSharp.text;
using Image = iTextSharp.text.Image;
using System.Windows.Markup;
using Paragraph = iTextSharp.text.Paragraph;
using Microsoft.Win32;

namespace ACMS.Pages.PagesW
{
    /// <summary>
    /// Логика взаимодействия для PaymentsFrame.xaml
    /// </summary>
    public partial class PaymentsFrame : Page
    {
        public PaymentsFrame()
        {
            InitializeComponent();

            listPayment.ItemsSource = AppConnect.modelOdb.Payments.ToList();
        }

        private void addButt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEdit.AddPayment());
        }

        private void SavePdf_Click(object sender, RoutedEventArgs e)
        {
            if (listPayment.SelectedItem != null)
            {
                var currentPayment = listPayment.SelectedItem;
                CreatePDF(currentPayment as Payments);
            }
            else
            {
                MessageBox.Show("Выберите чек","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            
        }
        private void CreatePDF(Payments pay)
        {
            Document doc = new Document();

            try
            {
                

                PdfWriter.GetInstance(doc, new FileStream("D:\\output.pdf", FileMode.Create));

                doc.Open();
                BaseFont basefont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf",BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Font font = new Font(basefont,12);
                Font fontImportant = new Font(basefont,12,1,BaseColor.RED);
                Font fontBig = new Font(basefont, 25, 3, BaseColor.BLACK);
                Paragraph paragraphMain = new Paragraph("Список товаров", fontBig);
                paragraphMain.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraphMain);
                decimal sum = 0;

                doc.Add(new Paragraph("Номер чека: " + pay.idPayment.ToString(),fontImportant));
                doc.Add(new Paragraph("Тип оплаты: " + pay.TypesPayments.Title.ToString(), font));
                doc.Add(new Paragraph("Название: " + pay.Products.Title.ToString(), font));
                doc.Add(new Paragraph("Стоимость: " + pay.Products.Cost, font));
                sum += pay.Products.Cost;

                Paragraph paragraph = new Paragraph("Сумма = " + sum.ToString(), font);
                paragraph.Alignment = Element.ALIGN_RIGHT;
                doc.Add(paragraph);

                MessageBox.Show("Документ сохранен","Уведомление",MessageBoxButton.OK,MessageBoxImage.None);
            }
            catch( DocumentException de)
            {
                MessageBox.Show(de.Message);
            }
            catch(IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            finally
            {
                doc.Close();
            }
        }

        private void delButt_Click(object sender, RoutedEventArgs e)
        {
            funDelete(listPayment);
        }
        public void funDelete(ListView curListView)
        {
            var ObjForRemoving = curListView.SelectedItems.Cast<Payments>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {ObjForRemoving.Count} чека(ов)", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.modelOdb.Payments.RemoveRange(ObjForRemoving);
                    AppConnect.modelOdb.SaveChanges();

                    curListView.ItemsSource = AppConnect.modelOdb.Payments.ToList();

                    MessageBox.Show("Чеки успешно удалены");
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
