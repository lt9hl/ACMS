﻿using System;
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
    /// Логика взаимодействия для DoorsFr.xaml
    /// </summary>
    public partial class DoorsFr : Page
    {
        public DoorsFr()
        {
            InitializeComponent();


            listDoors.ItemsSource = AppConnect.modelOdb.Doors.ToList();
        }
    }
}
