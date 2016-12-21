using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;

namespace MITD.PMSSecurity.Domain
{
    public interface IUserRepository : IRepository
    {
        //List<User> FindUsers(Expression<Func<User, bool>> predicate, ListFetchStrategy<User> fs, string frname,
        //    string lsName, string username, int pageSize, int pageIndex);

        IList<User> GetAllUsers();
        void GetAllUsers(ListFetchStrategy<User> fs);
        IList<User> FindUsers(Expression<Func<User, bool>> predicate);
        void FindUsers(Expression<Func<User, bool>> predicate, ListFetchStrategy<User> fs);
        User GetUserById(PartyId userId);
        IList<Group> GetAllUserGroup();
        Group GetUserGroupById(PartyId partyId);
        void Delete(Party party);
        void Add(Party party);
        Party GetById(PartyId partyId);
        List<User> GetPermittedWorkListFor(User user);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
        User GetUserByVerificationCode(string veriCode);
    }
}
