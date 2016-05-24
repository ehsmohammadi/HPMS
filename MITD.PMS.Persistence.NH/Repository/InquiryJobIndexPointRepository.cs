using MITD.Data.NH;
using MITD.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Persistence.NH
{
    public class InquiryJobIndexPointRepository : NHRepository, IInquiryJobIndexPointRepository
    {
        private NHRepository<InquiryJobIndexPoint> rep;

        public InquiryJobIndexPointRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public InquiryJobIndexPointRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep = new NHRepository<InquiryJobIndexPoint>(unitOfWork);
        }

        public IList<InquiryJobIndexPoint> Find(Expression<Func<InquiryJobIndexPoint, bool>> predicate)
        {
            return rep.Find(predicate);
        }

        public void Find(Expression<Func<InquiryJobIndexPoint, bool>> predicate, ListFetchStrategy<InquiryJobIndexPoint> fs)
        {
            rep.Find(predicate,fs);
        }


        public void Add(InquiryJobIndexPoint inquiryJobIndexPoint)
        {
            rep.Add(inquiryJobIndexPoint);
        }

        public long GetNextId()
        {
            return session.CreateSQLQuery("select next value for [dbo].[Inquiry_JobIndexPointsSeq]").UniqueResult<long>();
        }

        public InquiryJobIndexPoint GetBy(JobPositionInquiryConfigurationItemId configurationItemId, AbstractJobIndexId jobIndexId)
        {
            return  rep.Find(i => i.ConfigurationItemId.InquirerId.EmployeeNo == configurationItemId.InquirerId.EmployeeNo &&
                i.ConfigurationItemId.InquirerJobPositionId.SharedJobPositionId.Id == configurationItemId.InquirerJobPositionId.SharedJobPositionId.Id &&
                 i.ConfigurationItemId.InquirySubjectId.EmployeeNo == configurationItemId.InquirySubjectId.EmployeeNo &&
                 i.ConfigurationItemId.InquirySubjectJobPositionId.SharedJobPositionId.Id == configurationItemId.InquirySubjectJobPositionId.SharedJobPositionId.Id &&
                 i.ConfigurationItemId.InquirerId.PeriodId == configurationItemId.InquirerId.PeriodId &&
                i.JobIndexId == jobIndexId).Single();
        }

        public bool IsAllInquiryJobIndexPointsHasValue(Period period)
        {
            return rep.GetQuery()
                .Count(p => p.ConfigurationItemId.InquirerId.PeriodId == period.Id && (p.JobIndexValue.Trim() == "" || p.JobIndexValue.Trim() == string.Empty || p.JobIndexValue.Trim()==null)) == 0;
        }

        public void Delete(InquiryJobIndexPoint inquiryJobIndexPoint)
        {
            rep.Delete(inquiryJobIndexPoint);
        }

        public List<InquiryJobIndexPoint> GetAllBy(JobPositionInquiryConfigurationItemId jobPositionInquiryConfigurationItemId)
        {
            return rep.Find(i => i.ConfigurationItemId == jobPositionInquiryConfigurationItemId).ToList();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new InquiryJobIndexPointDuplicateException("InquiryJobIndexPoint", "JobIndexPoint");
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new InquiryJobIndexPointDeleteException("InquiryJobIndexPoint", "Id");
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
