using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Security.Domain.Model
{
    public class User : Party
    {
        private string fName;
        public virtual string FName { get { return fName; } }

        private string lName;
        public virtual string LName { get { return lName; } }

        private string email;
        public virtual string Email { get { return email; } }

        private string password;
        public virtual string Password { get { return password; } }


        public override string Name { get { return (FName + " " + LName); } }

        public User() : base()
        {
        }

        public User(PartyId id,string fName,string lname, string email, string password):base(id,fName+" "+lname)
        {
            this.fName = fName;
            this.lName = lname;
            this.email = email;
            this.password = password;
        }




    }
}
