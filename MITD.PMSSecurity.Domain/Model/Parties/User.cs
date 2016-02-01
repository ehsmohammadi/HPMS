using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain
{
    public class User : Party
    {
        #region Fields


        #endregion

        #region Properties

        public virtual List<ActionType> Actions { get; set; }
        
        private string firstName;
        public virtual string FirstName { get { return firstName; } }

        private string lastName;
        public virtual string LastName { get { return lastName; } }

        private string email;
        public virtual string Email { get { return email; } }

        private bool active;
        public virtual bool Active { get { return active; } }

        private IList<Group> groupList = new List<Group>();
        public virtual IReadOnlyList<Group> GroupList
        {
            get { return groupList.ToList().AsReadOnly(); }
        }

        private IList<User> workListUserList = new List<User>();
        public virtual IReadOnlyList<User> WorkListUserList
        {
            get { return workListUserList.ToList().AsReadOnly(); }
        }
               
        #endregion

        #region Constructors
       
        protected User()
        {
           //For OR mapper
        }

        public User(PartyId id, string firstname, string lastname, string email) : base(id)
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
            this.active = true;
        }
        public User(PartyId id, string firstname, string lastname, string email, bool isActive)
            : base(id)
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
            this.active = isActive;
        }

        #endregion

        #region Public Methods

        public virtual void Update(string firstname, string lastname, string email)
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
        }


        public virtual void Update(string firstname, string lastname, string email, bool isActive, Dictionary<ActionType, bool> customActions, List<Group> groups,List<User> permittedWorkListUsers)
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
            UpdateWorkListUsers(permittedWorkListUsers);
            UpdateCustomActions(customActions);
            UpdateGroups(groups);
        }

        public virtual void UpdateWorkListUsers(List<User> permittedWorkListUsers)
        {
            if (permittedWorkListUsers == null)
                return;
            foreach (var u in permittedWorkListUsers)
            {
                if (!workListUserList.Contains(u))
                    AssignWorkListUser(u);
            }

            for (int i = 0; i < workListUserList.Count; i++)
            {
                if (!permittedWorkListUsers.Contains(workListUserList[i]))
                    RemoveWorkListUser(workListUserList[i]);
            }
        }


        public virtual void UpdateGroups(List<Group> groups)
        {
            if (groups == null)
                return;
            foreach (var group in groups)
            {
                if (!groupList.Contains(group))
                    AssignGroup(group);
            }

            for (int i = 0; i < groupList.Count; i++)
            {
                if (!groups.Contains(groupList[i]))
                    RemoveGroup(groupList[i]);
            }
        }

        public virtual void AssignGroup(Group group)
        {
            if (group == null)
                throw new PMSSecurityException("گروه کاربر برای اختصاص مناسب نیست");
            groupList.Add(group);
        }

        public virtual void RemoveGroup(Group group)
        {
            groupList.Remove(group);
        }

        public virtual void AssignWorkListUser(User workListUser)
        {
            if (workListUser == null)
                throw new PMSSecurityException(" کاربر برای اختصاص مناسب نیست");
            workListUserList.Add(workListUser);
        }

        public virtual void RemoveWorkListUser(User user)
        {
            workListUserList.Remove(user);
        }
        #endregion

        public virtual void UpdateCustomActions(Dictionary<int, bool> actions, PartyId userId, List<ActionType> roleActionTypes)
        {
            var newList = new List<PartyCustomAction>();
            actions.ToList().ForEach(c =>
            {
                newList.Add(new PartyCustomAction(userId, c.Key, c.Value));
            });

            this.customActions = new Dictionary<int, bool>();
            newList.ForEach(d =>
            {
                if (d.IsGranted)
                {
                     if (!roleActionTypes.Exists(c => (int)c == d.ActionTypeId ))
                    {
                        this.customActions.Add(d.ActionTypeId, d.IsGranted);
                    }
                }
                else
                {
                    if (roleActionTypes.Exists(c => (int)c == d.ActionTypeId ))
                    {
                        this.customActions.Add(d.ActionTypeId, d.IsGranted);
                    }
                }
            });
        }
        
    }
}