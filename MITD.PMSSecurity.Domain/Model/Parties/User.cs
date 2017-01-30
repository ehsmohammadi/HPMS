using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMS.Common;
using MITD.PMS.Common.Utilities;
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

        private EmailStatusEnum emailStatus;
        public virtual EmailStatusEnum EmailStatus { get { return emailStatus; } }

        private string verificationCode;
        public virtual string VerificationCode { get { return verificationCode; } }



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

        public User(PartyId id, string firstname, string lastname, string email)
            : base(id)
        {
            setProperties(firstname, lastname, email, true);
        }
        public User(PartyId id, string firstname, string lastname, string email, bool isActive)
            : base(id)
        {

            setProperties(firstname, lastname, email, isActive);
        }

        #endregion

        #region Public Methods

        private void updateEmail(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    email = "";
                    verificationCode = ""; 
                }
                switch (EmailStatus)
                {
                    case EmailStatusEnum.NotEntered:
                        break;
                    case EmailStatusEnum.Unverified:
                        break;
                    case EmailStatusEnum.Verified:
                        break;
                    default:
                        emailStatus = EmailStatusEnum.NotEntered;
                        break;
                }
            }
            else
            {
                switch (EmailStatus)
                {
                    case EmailStatusEnum.NotEntered:
                        this.email = emailAddress;
                        setVerificationCode();
                        this.emailStatus = EmailStatusEnum.Unverified;
                        break;
                    case EmailStatusEnum.Unverified:
                        this.email = emailAddress;
                        setVerificationCode();
                        this.emailStatus = EmailStatusEnum.Unverified;
                        break;
                    case EmailStatusEnum.Verified:
                        break;
                    default:
                        this.email = emailAddress;
                        setVerificationCode();
                        this.emailStatus = EmailStatusEnum.Unverified;
                        break;
                }
            }

        }

        private void setVerificationCode()
        {
            if(string.IsNullOrWhiteSpace(verificationCode))
            this.verificationCode=Guid.NewGuid().ToString();
        }

        private void setProperties(string firstname, string lastname, string email, bool isActive)
        {
            this.firstName = firstname;
            this.lastName = lastname;
            this.active = isActive;
            updateEmail(email);
        }

        public virtual void Update(string firstname, string lastname, string email)
        {
            setProperties(firstname, lastname, email, this.active);
        }


        public virtual void Update(string firstname, string lastname, string email, bool isActive, Dictionary<ActionType, bool> customActions, List<Group> groups, List<User> permittedWorkListUsers)
        {
            Update(firstname, lastname, email);
            UpdateWorkListUsers(permittedWorkListUsers);
            UpdateCustomActions(customActions);
            UpdateGroups(groups);
        }

        public virtual void UpdateProfile(string emailAddress, IEmailManager emailManager)
        {
            updateEmail(emailAddress);
            if (EmailStatus != EmailStatusEnum.Unverified) return;
            emailManager.SendVerificationEmail(this.LastName,emailAddress,this.VerificationCode);
            //var emailContent = creatVerificationEmail();
            //emailManager.SendEmail("Email verification; PMS Team ",emailAddress, emailContent);
        }

        public virtual void VerifyEmail()
        {
            if(EmailStatus==EmailStatusEnum.Unverified)
                emailStatus=EmailStatusEnum.Verified;
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
                    if (!roleActionTypes.Exists(c => (int)c == d.ActionTypeId))
                    {
                        this.customActions.Add(d.ActionTypeId, d.IsGranted);
                    }
                }
                else
                {
                    if (roleActionTypes.Exists(c => (int)c == d.ActionTypeId))
                    {
                        this.customActions.Add(d.ActionTypeId, d.IsGranted);
                    }
                }
            });
        }

        #endregion

        
    }
}