using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;

namespace MITD.Security.Domain.Model
{
    public class Party : IEntity<Party> 
    {

       #region Fields
       
       
        #endregion

        #region Properties
        
        protected readonly PartyId id;
        public virtual PartyId Id
        {
            get { return id; }
            
        }

       

        private string name;
        public virtual string Name { get { return name; } }

        

        #endregion

        #region Constructors
        protected Party()
        {
           //For OR mapper
        }

        public Party(PartyId partyId,string name)
        {
            if (partyId == null)
                throw new ArgumentNullException("partyId");
            id = partyId;
            this.name = name;
        }

       
       
        #endregion

        #region Public Methods

        public virtual void Update(string name)
        {
            this.name = name;
        }

       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Party other)
        {
            return (other != null) && Id.Equals(other.Id);
        }
      
        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Party)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        } 

        #endregion
    }
}
