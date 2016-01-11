using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain
{
    public class Party : IEntity<Party>
    {
        #region Fields

        private readonly byte[] rowVersion;

        #endregion

        #region Properties

        protected IDictionary<int, bool> customActions = new Dictionary<int, bool>();
        public virtual IDictionary<int, bool> CustomActions { get { return customActions.ToDictionary(s => s.Key, s => s.Value); } }

        private readonly PartyId id;
        public virtual PartyId Id { get { return id; } }

        #endregion

        #region Constructors
       
        protected Party()
        {
           //For OR mapper
        }

        public Party(PartyId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
        }

        #endregion

        #region Public Methods

        public virtual void UpdateCustomActions(Dictionary<ActionType,bool> customActionsParam)
        {
            var actions = customActionsParam.Keys;
            int actValue;
            foreach (ActionType act in actions)
            {
                actValue = (int) act;
                if (customActions.Keys.Contains(actValue))
                    customActions[actValue] = customActionsParam[act];
                else
                    customActions.Add(actValue, customActionsParam[act]);
            }

            var actionIds = new List<int>(customActions.Keys);
            foreach (int actId in actionIds)
            {
                if (!customActionsParam.Keys.Select(a => ((int)a).ToString()).Contains(actId.ToString()))
                    customActions.Remove(actId);
            }
            
        }

        public virtual void AssignCustomAction(ActionType act, bool value)
        {
            if (act == null)
                throw new PMSSecurityException("نوع دسترسی  برای اختصاص  مناسب نیست");

            int actId = (int) act;
            if (CustomActions.Keys.Contains(actId))
                customActions.Remove(actId);
            customActions.Add(actId, value);
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