using System;
using MITD.PMS.Domain.Model.Job;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Interface.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MITD.PMS.Interface.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBaseMapper()
        {
            var jipMap = new JobInPeriodMapper();
            var job = new Job(new Period(new PeriodId(1),"pr","prDic" )
                ,new SharedJob(new SharedJobId(1),"jobnam","jobdic" ) );
            var jdto = jipMap.MapToModel(job, new string[] {"Name","DictionaryName"});

        }
    }
}
