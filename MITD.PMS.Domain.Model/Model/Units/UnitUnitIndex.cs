using MITD.Core.Builders;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.UnitIndices;

namespace MITD.PMS.Domain.Model.Units
{
    public class UnitUnitIndex : IValueObject<UnitUnitIndex>,IUnitUnitIndex
    {

        #region Properties

        private readonly AbstractUnitIndexId unitIndexId;
        public virtual AbstractUnitIndexId UnitIndexId { get { return unitIndexId; } }

        private bool showforTopLevel;
        public virtual bool ShowforTopLevel
        {
            get { return showforTopLevel; }
        }

        private bool showforSameLevel;
        public virtual bool ShowforSameLevel
        {
            get { return showforSameLevel; }
        }

        private bool showforLowLevel;
        public virtual bool ShowforLowLevel
        {
            get { return showforLowLevel; }
        }


        #endregion

        #region Constructors
        // for Or mapper
        protected UnitUnitIndex()
        {

        }

        public UnitUnitIndex(AbstractUnitIndexId unitIndexId, bool showforTopLevel, bool showforSameLevel, bool showforLowLevel)
        {
            this.unitIndexId = unitIndexId;
            this.showforTopLevel = showforTopLevel;
            this.showforSameLevel = showforSameLevel;
            this.showforLowLevel = showforLowLevel;
        }
        #endregion

        #region IValueObject Member
        public bool SameValueAs(UnitUnitIndex other)
        {
            var builder = new EqualsBuilder();
            builder.Append(UnitIndexId, other.UnitIndexId);
            builder.Append(ShowforTopLevel, other.ShowforTopLevel);
            builder.Append(ShowforSameLevel, other.ShowforSameLevel);
            builder.Append(showforLowLevel, other.showforLowLevel);
            return builder.IsEquals();
        }
        #endregion

        #region Object Override
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitUnitIndex)obj;
            return SameValueAs(other);
        }
        public override int GetHashCode()
        {
            return
                new HashCodeBuilder().Append(UnitIndexId)
                    .Append(ShowforTopLevel)
                    .Append(ShowforSameLevel)
                    .Append(ShowforLowLevel)
                    .ToHashCode();

        }

        public override string ToString()
        {
            return "UnitIndexId:" + UnitIndexId + ";ShowforTopLevel:" + ShowforTopLevel + ";ShowforSameLevel:" +
                   ShowforSameLevel + ";ShowforLowLevel:" + ShowforLowLevel;
        }

        public static bool operator ==(UnitUnitIndex left, UnitUnitIndex right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitUnitIndex left, UnitUnitIndex right)
        {
            return !(left == right);
        }

        #endregion
    }



    #region Interface 
    public interface IUnitUnitIndex
    {
        AbstractUnitIndexId UnitIndexId { get; }
        bool ShowforTopLevel {get;}
        bool ShowforSameLevel { get; }
        bool ShowforLowLevel { get; }

    } 
    #endregion
}
