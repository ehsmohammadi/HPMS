using System.Collections.Generic;
using System.Data;
using System.Linq;
using MITD.Domain.Model;
using System;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.Units
{
    public class Unit : EntityWithDbId<long,UnitId>, IEntity<Unit>
    {
        #region Fields

        private readonly byte[] rowVersion;
        private readonly SharedUnit sharedUnit;

        #endregion

        #region Properties

        public virtual UnitId Id
        {
            get { return id; }

        }
       
        public virtual string Name { get { return sharedUnit.Name; } }

        public virtual string DictionaryName { get { return sharedUnit.DictionaryName; } }

        public virtual SharedUnit SharedUnit { get { return sharedUnit; } }

        private readonly Unit parent;
        public virtual Unit Parent
        {
            get { return parent; }
        }

        private readonly IList<UnitUnitIndex> unitIndexList;
        public virtual IReadOnlyList<UnitUnitIndex> UnitIndexList
        {
            get { return unitIndexList.ToList().AsReadOnly(); }
        }
        private readonly IList<UnitCustomField> customFields;
        public virtual IReadOnlyList<UnitCustomField> CustomFields
        {
            get { return customFields.ToList().AsReadOnly(); }
        }

        #endregion

        #region Constructors
        protected Unit()
        {
            //For OR mapper
        }

        public Unit(Period period, SharedUnit sharedUnit,Unit parent)
        {
            if (period == null || period.Id == null)
                throw new ArgumentNullException("period");
            if(sharedUnit==null || sharedUnit.Id==null)
                throw new ArgumentNullException("sharedUnit");
            period.CheckAssigningUnit();
            id = new UnitId(period.Id,sharedUnit.Id);
            this.sharedUnit = sharedUnit;
            this.parent = parent;
            customFields = new List<UnitCustomField>();
            unitIndexList = new List<UnitUnitIndex>();
        }
        public Unit(Period period, SharedUnit sharedUnit, IList<UnitCustomField> customFieldList,
            IList<UnitUnitIndex> unitIndexList,Unit parent)
            : this(period, sharedUnit,parent)
        {
            assignCustomFields(customFieldList);
            assignunitIndices(unitIndexList);

        }


        #endregion

        #region Public Methods

        public virtual void UpdateUnitIndices(IList<UnitUnitIndex> unitIndexList, IPeriodManagerService periodChecker)
        {
            if (isunitIndicesHaveChanged(unitIndexList))
            {

                foreach (var itm in unitIndexList)
                {
                    if (!this.UnitIndexList.Contains(itm))
                        assignunitIndex(itm);
                }

                IList<UnitUnitIndex> copyOfUnitIndexIdList = new List<UnitUnitIndex>(this.unitIndexList);
                foreach (var itm in copyOfUnitIndexIdList)
                {
                    if (!unitIndexList.Contains(itm))
                        removeunitIndex(itm);
                }
                periodChecker.CheckModifyingUnitIndices(this);
            }

        }
        private void assignunitIndices(IList<UnitUnitIndex> unitIndexList)
        {
            foreach (var itm in unitIndexList)
            {
                assignunitIndex(itm);
            }
        }

        private void assignCustomFields(IList<UnitCustomField> customFieldList)
        {

            foreach (var sharedunitCustomField in customFieldList)
            {
                AssignSharedCustomField(sharedunitCustomField);
            }

        }

        public virtual void UpdateCustomFields(IList<UnitCustomField> customFieldList, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckModifyingUnitCustomFields(this);
            foreach (var sharedunitCustomField in customFieldList)
            {
                if (!this.customFields.Contains(sharedunitCustomField))
                    AssignSharedCustomField(sharedunitCustomField);
            }

            IList<UnitCustomField> copyOfCustomFields = new List<UnitCustomField>(customFields);
            foreach (var itm in copyOfCustomFields)
            {
                if (!customFieldList.Contains(itm))
                    RemoveSharedCustomField(itm);
            }

        }

        public virtual void RemoveSharedCustomField(UnitCustomField sharedunitCustomField)
        {
            customFields.Remove(sharedunitCustomField);
        }

        public virtual void AssignSharedCustomField(UnitCustomField sharedunitCustomField)
        {
            customFields.Add(sharedunitCustomField);
        }

        public virtual void UpdateunitIndices(IList<UnitUnitIndex> unitIndexList, IPeriodManagerService periodChecker)
        {
            if (isunitIndicesHaveChanged(unitIndexList))
            {

                foreach (var itm in unitIndexList)
                {
                    if (!this.unitIndexList.Contains(itm))
                        assignunitIndex(itm);
                }

                IList<UnitUnitIndex> copyOfunitIndexIdList = new List<UnitUnitIndex>(this.unitIndexList);
                foreach (var itm in copyOfunitIndexIdList)
                {
                    if (!unitIndexList.Contains(itm))
                        removeunitIndex(itm);
                }
                periodChecker.CheckModifyingUnitIndices(this);
            }

        }

        private void removeunitIndex(UnitUnitIndex unitIndex)
        {
            unitIndexList.Remove(unitIndex);
        }

        private void assignunitIndex(UnitUnitIndex unitIndex)
        {
            unitIndexList.Add(unitIndex);
        }

        private bool isunitIndicesHaveChanged(IList<UnitUnitIndex> unitIndexList)
        {
            if (this.unitIndexList != null && this.unitIndexList.Count > 0 &&
                (!unitIndexList.All(j => this.unitIndexList.Contains(j)) ||
                 !this.unitIndexList.All(j => unitIndexList.Select(ji => ji).Contains(j))))
                return true;
            return false;
        }
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Unit other)
        {
            return (other != null) && Id.Equals(other.Id);
        }

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Unit)obj;
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
