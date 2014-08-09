using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimTypeEnum : Enumeration, IValueObject<ClaimTypeEnum>
    {
        public static readonly ClaimTypeEnum ClaimToBasicData = new ClaimTypeEnum("1", "ClaimToBasicData");
        public static readonly ClaimTypeEnum ClaimToCalculationRules = new ClaimTypeEnum("2", "ClaimToCalculationRules");
        public static readonly ClaimTypeEnum CalimToScores = new ClaimTypeEnum("3","CalimToScores");
        public static readonly ClaimTypeEnum Others = new ClaimTypeEnum("4","Others");

        protected ClaimTypeEnum(string value, string name)
            : base(value, name)
        {
        }

        public bool SameValueAs(ClaimTypeEnum other)
        {
            return Equals(other);
        }
        public static bool operator ==(ClaimTypeEnum left, ClaimTypeEnum right)
        {
            return object.Equals(left, right);
        }
        public static bool operator !=(ClaimTypeEnum left, ClaimTypeEnum right)
        {
            return !(left == right);
        }

       

    }
}
