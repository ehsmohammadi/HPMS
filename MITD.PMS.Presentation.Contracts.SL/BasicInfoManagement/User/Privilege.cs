using System;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public class Privilege : ViewModelBase
    {
        private ActionType actionType;
        public ActionType ActionType
        {
            get { return actionType; }
            set
            {
                this.SetField(p => p.ActionType, ref actionType, value);
            }
        }

        public string ActionName
        {
            get { return actionType.GetAttribute<ActionInfoAttribute>().Description; }
        }

        private bool  isGrant;
        public bool IsGrant
        {
            get { return isGrant; }
            set
            {
                this.SetField(p => p.IsGrant, ref isGrant, value);
            }
        }

        //private bool isDeny;
        //public bool IsDeny
        //{
        //    get { return isDeny; }
        //    set
        //    {
        //        this.SetField(p => p.IsDeny, ref isDeny, value);
        //    }
        //}

    }
}
