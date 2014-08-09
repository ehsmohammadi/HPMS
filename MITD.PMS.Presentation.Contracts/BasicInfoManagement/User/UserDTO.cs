using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class UserDTO : PartyDTO
    {

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { this.SetField(p => p.FirstName, ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { this.SetField(p => p.LastName, ref lastName, value); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { this.SetField(p => p.Email, ref email, value); }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { this.SetField(p => p.IsActive, ref isActive, value); }
        }


        private List<UserGroupDescriptionDTO> groups = new List<UserGroupDescriptionDTO>();
        public List<UserGroupDescriptionDTO> Groups
        {
            get { return groups; }
            set { this.SetField(p => p.Groups, ref groups, value); }
        }

        private List<UserDescriptionDTO> workListPermittedUsers = new List<UserDescriptionDTO>();
        public List<UserDescriptionDTO> WorkListPermittedUsers
        {
            get { return workListPermittedUsers; }
            set { this.SetField(p => p.WorkListPermittedUsers, ref workListPermittedUsers, value); }
        }

 
    }
}
