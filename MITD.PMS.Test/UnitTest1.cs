using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MITD.PMS.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            long x = 11;
            long y = 1000;
            decimal z = (x * 100) / y;
        }
    }
}
