﻿
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

using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

using ACMS.Pages;

using ACMS.ApplicationData;
using System.Reflection;

namespace ACMS
{

    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            

            AppConnect.modelOdb = new ACMSEntities1();
            AppFrame.PMain = frMain;

            AppFrame.PMain.Navigate(new LoginI());
        }

        private void CloseWin(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimWin(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void frMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
