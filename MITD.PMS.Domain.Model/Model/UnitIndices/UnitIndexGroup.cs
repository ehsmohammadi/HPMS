using System;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class UnitIndexGroup:AbstractUnitIndex
    {


       

        #region Fields


        #endregion

        #region Properties

        private string name;
        public virtual string Name { get { return name; } }

        private string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }
      
       
        private  UnitIndexGroup parent;
        public virtual UnitIndexGroup Parent { get { return parent; } }

       
        #endregion

        #region Constructors

        protected UnitIndexGroup()
        {

        }

        public UnitIndexGroup(AbstractUnitIndexId UnitIndexGroupId,Period period, UnitIndexGroup parent, string name,
                                string dictionaryName)
        {
            if (UnitIndexGroupId == null)
                throw new ArgumentNullException("UnitIndexGroupId");
            if (period == null)
                throw new ArgumentNullException("period");

            this.parent = parent;
            periodId = period.Id;
            id = UnitIndexGroupId;
            this.name = name;
            this.dictionaryName = dictionaryName;
        }

        public virtual void Update(UnitIndexGroup parent, string name, string dictionaryName)
        {
            this.name = name;
            this.dictionaryName = dictionaryName;
            this.parent = parent;
        }
       

        #endregion

        #region Public Methods
      
        #endregion

    }
}
