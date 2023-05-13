using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderApp.Databases
{
    public class ProviderDatabase
    {
        private static ProviderDataBaseEntities _databaseEntities;
        
        public static ProviderDataBaseEntities GetContext()
        {
            if (_databaseEntities == null)
            {
                _databaseEntities = new ProviderDataBaseEntities();
            }
            return _databaseEntities;
        }

    }

}
