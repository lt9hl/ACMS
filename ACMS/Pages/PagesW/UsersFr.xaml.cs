
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
        CurrentUser currentUser = AppConnect.modelOdb.CurrentUser.OrderByDescending(x => x.idCurrentUser).ToList()[0];


        public UsersFr()
        {
            InitializeComponent();

            listUsers.ItemsSource = AppConnect.modelOdb.Users.ToList();

        }

        private void delSelected(object sender, RoutedEventArgs e)
        {
            funDelete();
            
        }
        public void funDelete()
        {
            var ObjForRemoving = listUsers.SelectedItems.Cast<Users>().ToList();
            if (ObjForRemoving.Contains(currentUser.Users))
                MessageBox.Show("Невозможно удалить текущего пользователя","Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                if (MessageBox.Show($"Вы точно хотите удалить {ObjForRemoving.Count} пользователей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    { 
                        AppConnect.modelOdb.Users.RemoveRange(ObjForRemoving);
                        AppConnect.modelOdb.SaveChanges();

                        MessageBox.Show("Пользователи успешно удалены","Уведомление",MessageBoxButton.OK, MessageBoxImage.Information);

                        listUsers.ItemsSource = AppConnect.modelOdb.Users.ToList();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            
        }
        

        private void unlockLockSelected(object sender, RoutedEventArgs e)
        {
            unlock(listUsers);
        }
        public void unlock(ListView curListView)
        {
            
            var ObjForBlackList = curListView.SelectedItems.Cast<Users>().ToList();
            
            if (ObjForBlackList.Count == 1)
            {
                var selectedUser = AppConnect.modelOdb.Users.ToList()[curListView.SelectedIndex].idUser;
                if (AppConnect.modelOdb.CurrentUser.ToList()[AppConnect.modelOdb.CurrentUser.Count()-1].idUser == selectedUser)
                {
                    MessageBox.Show("Невозможно заблокировать текущего пользователя");
                    return;
                }
                try
                    {
                    var currentBlackListUser = AppConnect.modelOdb.BlackList.FirstOrDefault(x => x.idUser == selectedUser);
                    if (currentBlackListUser == null)
                    {
                        var newBlackListUser = new BlackList()
                        {
                            idUser = selectedUser,
                            EnterDate = DateTime.Now,
                    };
                        AppConnect.modelOdb.BlackList.Add(newBlackListUser);

                        MessageBox.Show("Пользователь добавлен в черный список");
                        ImageLockUnlock.Source = new BitmapImage(new Uri(@"\Images\workIcons\unlock.png", UriKind.Relative));

                    }
                    else
                    {
                        AppConnect.modelOdb.BlackList.Remove(currentBlackListUser);

                        MessageBox.Show("Пользователь удален из черного списка");
                        ImageLockUnlock.Source = new BitmapImage(new Uri(@"\Images\workIcons\lock.png", UriKind.Relative));
                    }
                    AppConnect.modelOdb.SaveChanges();

                    curListView.ItemsSource = AppConnect.modelOdb.Users.ToList();

                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать один объект", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void listUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = AppConnect.modelOdb.Users.ToList()[listUsers.SelectedIndex];
            var blackListUser = AppConnect.modelOdb.BlackList.FirstOrDefault(x => x.idUser == selectedUser.idUser);
            if (blackListUser == null)
            {
                ImageLockUnlock.Source = new BitmapImage(new Uri(@"\Images\workIcons\lock.png", UriKind.Relative));
            }
            else
            {
                ImageLockUnlock.Source = new BitmapImage(new Uri(@"\Images\workIcons\unlock.png", UriKind.Relative));
            }
            
        }
    }
}
