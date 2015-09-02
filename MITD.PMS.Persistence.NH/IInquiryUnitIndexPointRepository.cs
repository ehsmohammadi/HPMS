using MITD.Data.NH;
using MITD.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.UnitIndices;

using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;

namespace MITD.PMS.Persistence.NH
{
    public class InquiryUnitIndexPointRepository : NHRepository, IInquiryUnitIndexPointRepository
    {
        private NHRepository<InquiryUnitIndexPoint> rep;

        public InquiryUnitIndexPointRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public InquiryUnitIndexPointRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<InquiryUnitIndexPoint>(unitOfWork);
        }

        public IList<InquiryUnitIndexPoint> Find(Expression<Func<InquiryUnitIndexPoint, bool>> predicate)
        {
            return rep.Find(predicate);
        }

        public void Find(Expression<Func<InquiryUnitIndexPoint, bool>> predicate, ListFetchStrategy<InquiryUnitIndexPoint> fs)
        {
            rep.Find(predicate,fs);
        }


        public void Add(InquiryUnitIndexPoint inquiryUnitIndexPoint)
        {
            rep.Add(inquiryUnitIndexPoint);
        }

        public long GetNextId()
        {
            return session.CreateSQLQuery("select next value for [dbo].[Inquiry_UnitIndexPointsSeq]").UniqueResult<long>();
        }

        public InquiryUnitIndexPoint GetBy(UnitInquiryConfigurationItemId configurationItemId)
        {
            //return  rep.Find(i => i.ConfigurationItemId.InquirerId.EmployeeNo == configurationItemId.InquirerId.EmployeeNo &&
            //     i.ConfigurationItemId.InquirySubjectId.EmployeeNo == configurationItemId.InquirySubjectId.EmployeeNo &&
            //     i.ConfigurationItemId.InquirySubjectUnitId.SharedUnitId.Id == configurationItemId.InquirySubjectUnitId.SharedUnitId.Id &&
            //     i.ConfigurationItemId.InquirerId.PeriodId == configurationItemId.InquirerId.PeriodId &&
            //    i.UnitIndexId == unitInd exId).Single();

            var res = rep.Find(
                i => i.ConfigurationItemId.InquirerId.EmployeeNo == configurationItemId.InquirerId.EmployeeNo &&
                     i.ConfigurationItemId.InquirySubjectUnitId.SharedUnitId.Id ==
                     configurationItemId.InquirySubjectUnitId.SharedUnitId.Id &&
                     i.ConfigurationItemId.InquirerId.PeriodId == configurationItemId.InquirerId.PeriodId &&
                     i.ConfigurationItemId.UnitIndexIdUintPeriod == configurationItemId.UnitIndexIdUintPeriod
                );
            return res.SingleOrDefault();

        }

        public bool IsAllInquiryUnitIndexPointsHasValue(Period period)
        {
            return rep.GetQuery()
                .Count(p => p.ConfigurationItemId.InquirerId.PeriodId == period.Id && (p.UnitIndexValue.Trim() == "" || p.UnitIndexValue.Trim() == string.Empty || p.UnitIndexValue.Trim()==null)) == 0;
        }

        public void Delete(InquiryUnitIndexPoint inquiryUnitIndexPoint)
        {
            rep.Delete(inquiryUnitIndexPoint);
        }

      //  public List<InquiryUnitIndexPoint> GetAllBy(UnitInquiryConfigurationItemId configurationItemId)
        public List<InquiryUnitIndexPoint> GetAllBy(EmployeeId employeeId,UnitId unitId)  
    {
            return rep.Find(
                     i => i.ConfigurationItemId.InquirerId.EmployeeNo == employeeId.EmployeeNo &&
                          i.ConfigurationItemId.InquirySubjectUnitId.SharedUnitId.Id ==unitId.SharedUnitId.Id &&
                          i.ConfigurationItemId.InquirerId.PeriodId == unitId.PeriodId 
                         ).ToList();
            
            //return rep.Find(i => i.ConfigurationItemId == unitPositionInquiryConfigurationItemId).ToList();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new InquiryUnitIndexPointDuplicateException("InquiryUnitIndexPoint", "UnitIndexPoint");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new InquiryUnitIndexPointDeleteException("InquiryUnitIndexPoint", "Id");
            throw new Exception();
        }

        public Exception TryConvertException(Exception exp)
        {
            Exception res = null;
            try
            {
                res = ConvertException(exp);
            }
            catch (Exception e)
            {

            }
            return res;
        }
    }
}
