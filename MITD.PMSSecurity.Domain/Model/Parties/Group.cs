using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain
{
    public class Group : Party
    {
        #region Fields


        #endregion

        #region Properties

        
        private string description;
        public virtual string Description { get { return description; } }

       
        #endregion

        #region Constructors
       
        protected Group()
        {
           //For OR mapper
        }

        public Group(PartyId id,string description):base(id)
        {
            this.description= description;
        }

        #endregion

        #region Public Methods

        public virtual void Update(string description,Dictionary<ActionType,bool> customActions )
        {
            this.description = description;
            UpdateCustomActions(customActions);
        }

        

        #endregion

       
    }
}