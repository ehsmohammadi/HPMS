using System;
using MITD.Domain.Model;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.UnitIndices
{
    public class UnitIndexCategory:AbstractUnitIndex
    {

        #region Fields

        #endregion

        #region Properties

       
        private UnitIndexCategory parent;
        public virtual UnitIndexCategory Parent { get { return parent; } }

       
        #endregion

        #region Constructors

        protected UnitIndexCategory()
        {

        }

        public UnitIndexCategory(AbstractUnitIndexId unitIndexCategoryId, UnitIndexCategory parent, string name,
                                string dictionaryName)
            : base(unitIndexCategoryId,  name, dictionaryName)
        {
            this.parent = parent;
        }

       

        #endregion

        #region Public Methods
        public virtual void Update(UnitIndexCategory parent, string name, string dictionaryName)
        {
            this.parent = parent;

            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex", "Name");
            this.name = name;
            if (string.IsNullOrWhiteSpace(name))
                throw new UnitIndexArgumentException("UnitIndex", "DictionaryName");
            this.dictionaryName = dictionaryName;
        }

        #endregion

    }
}
