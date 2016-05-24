using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMS.Common;
using MITD.PMSSecurity.Domain;
using NHibernate.Linq;

namespace MITD.PMSSecurity.Persistence.NH
{
    public class UserRepository : NHRepository, IUserRepository
    {
        private NHRepository<Party> rep;
        private NHRepository<User> repUser;

        public UserRepository(NHUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            init();
        }

        public UserRepository(IUnitOfWorkScope unitOfWorkScope)
            : base(unitOfWorkScope)
        {
            init();
        }

        private void init()
        {
            rep =  new NHRepository<Party>(unitOfWork);
            repUser = new NHRepository<User>(unitOfWork);
        }

        public void FindUsers(Expression<Func<User, bool>> predicate, ListFetchStrategy<User> fs)
        {
            //var a=rep.GetQuery<User>().Where(predicate);
            //a.ToList();
            repUser.Find<User>(predicate, fs);
           // rep.GetAll<User>(fs);
        }

        //public List<User> FindUsers(Expression<Func<User, bool>> predicate, ListFetchStrategy<User> fs, string frname, string lsName, string username, int pageSize, int pageIndex)
        //{

        //    var q = this.Context.CreateObjectSet<Party>().OfType<User>().Where(c =>

        //        (c.FirstName.Contains(frname) || string.IsNullOrEmpty(frname)) &&
        //        (c.LastName.Contains(lsName) || string.IsNullOrEmpty(lsName)) &&
        //        (c.PartyName.Contains(username) || string.IsNullOrEmpty(username))
        //        ).OrderBy(c => c.Id).AsQueryable();

        //    pageIndex = (pageIndex == 0) ? 1 : pageIndex;
        //    fs.PageCriteria.PageResult.Result = q.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

        //    fs.PageCriteria.PageResult.TotalCount = q.Count();
        //    fs.PageCriteria.PageResult.TotalPages = Convert.ToInt32(Math.Ceiling(decimal.Divide(fs.PageCriteria.PageResult.TotalCount, pageSize)));
        //    fs.PageCriteria.PageResult.CurrentPage = pageIndex;

        //    return fs.PageCriteria.PageResult.Result.ToList();
        //}

        public User GetUserById(PartyId userId)
        {
            //return rep.Find<User>(u => u.Id.PartyName.ToLower() == userId.PartyName.ToLower(), new ListFetchStrategy<User>().Include(u => u.CustomActions)).SingleOrDefault();
            return rep.Find<User>(u=>u.Id.PartyName.ToLower() == userId.PartyName.ToLower()).SingleOrDefault();
        }

        public IList<Group> GetAllUserGroup()
        {
            return rep.GetAll<Group>();
        }

        public Group GetUserGroupById(PartyId partyId)
        {
            return rep.Find<Group>(g => g.Id.PartyName.ToLower() == partyId.PartyName.ToLower()).SingleOrDefault();
        }

        public void GetAllUsers(ListFetchStrategy<User> fs)
        {
            rep.GetAll<User>(fs);
        }

        public IList<User> GetAllUsers()
        {
            return rep.GetAll<User>();
        }

        public IList<User> FindUsers(Expression<Func<User, bool>> predicate)
        {
            return rep.Find<User>(predicate);
        }

      

        public void Delete(Party party)
        {
            rep.Delete(party);
        }

        public void Add(Party party)
        {
            rep.Add(party);
        }

        public Party GetById(PartyId partyId)
        {
            return rep.Find<Party>(g => g.Id.PartyName.ToLower() == partyId.PartyName.ToLower()).SingleOrDefault();
        }

        public List<User> GetPermittedWorkListFor(User user)
        {
            return  rep.Find<User>(f => f.WorkListUserList.Contains(user)).ToList();
        }

        public Exception ConvertException(Exception exp)
        {
            string typeName = "";
            string keyName = "";
            if (NHExceptionDetector.IsDublicateException(exp, out typeName, out keyName))
                return new PartyDuplicateException(typeName, keyName);
            if (NHExceptionDetector.IsDeleteHasRelatedDataException(exp, out typeName, out keyName))
                return new PartyDeleteException(typeName,keyName);
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
