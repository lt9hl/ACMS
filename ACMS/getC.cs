using ACMS.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMS
{
    internal class getC
    {
        private static ACMSEntities _context;

        public static ACMSEntities GetContext()
        {
            if (_context == null)
                _context = new ACMSEntities();
            return _context;

        }
    }
}
