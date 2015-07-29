using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Persistence.Migration.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var iridlSeedData=new IRISLSeedData();
            iridlSeedData.Up();
        }
    }
}
