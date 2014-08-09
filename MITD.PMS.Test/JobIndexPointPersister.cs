using MITD.Core;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Persistence.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MITD.PMS.Test
{
    public class JobIndexPointPersister : IEventHandler<JobIndexPointsReady>
    {
        public void Handle(JobIndexPointsReady eventData)
        {
            Task.Factory.StartNew(() =>
                {
                    using (var transaction = new TransactionScope())
                    using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
                    {
                        var rep = new JobIndexPointRepository(uow);
                        foreach (var item in eventData.PointsHolder.EmployeePointsForAdd)
                        {
                            rep.Add(item);
                        }
                        foreach (var item in eventData.PointsHolder.EmployeePointsForUpdate)
                        {
                            var employeePoint=rep.GetById(item.Key);
                            employeePoint.SetValue(item.Value);
                        }
                        uow.Commit();
                        transaction.Complete();
                    }

                });
        }
    }
}
