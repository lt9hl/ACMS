using ACMS.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACMS
{
    public class getC
    {
        public Employees[] Emp()
        {
            try
            {
                List<Employees> empl = AppConnect.modelOdb.Employees.ToList();
                return empl.ToArray();
            }
            catch
            {
                Mess();
                return null;
            }

        }

        public Doors[] doors()
        {
            try
            {
                List<Doors> doo = AppConnect.modelOdb.Doors.ToList();
                return doo.ToArray();
            }
            catch
            {
                Mess();
                return null;
            }
        }
        public DoorDirections[] dooDir()
        {
            try
            {
                List<DoorDirections> dooDir = AppConnect.modelOdb.DoorDirections.ToList();
                return dooDir.ToArray();
            }
            catch
            {
                Mess();
                return null;
            }
        }
        public void Mess()
        {
            MessageBox.Show($"Произошла ошибка в работе приложения", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
   
}

