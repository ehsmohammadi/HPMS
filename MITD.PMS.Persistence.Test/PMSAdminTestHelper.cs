using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jobPms = MITD.PMS.Domain.Model.Jobs;
using MITD.PMSAdmin.Persistence.NH;
using NHibernate;
using JobPmsAdmin = MITD.PMSAdmin.Domain.Model.Jobs;
namespace MITD.PMS.Persistence.Test
{
    public class PMSAdminTestHelper
    {
        public static jobPms.SharedJob AddJob(string jobName,string jobDicName)
        {
            using (var session = PMSAdminSession.GetSession())
            using (session.BeginTransaction())
            {
                long jobSeqId = GetJobSequence(session);
                var job = new JobPmsAdmin.Job(new JobPmsAdmin.JobId(jobSeqId), jobName, jobDicName);
                session.Save(job);
                session.Transaction.Commit();
                return new jobPms.SharedJob( new jobPms.SharedJobId(job.Id.Id),job.Name,job.DictionaryName );
            }
            
        }

       

        private static long GetJobSequence(ISession session)
        {
            return  session.CreateSQLQuery("Select next value for dbo.JobSeq").UniqueResult<long>();
        }
    }
}
