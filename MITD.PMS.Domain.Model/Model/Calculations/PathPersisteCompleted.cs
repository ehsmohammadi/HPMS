using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class PathPersisteCompleted : IDomainEvent<PathPersisteCompleted>
    {
        private readonly bool hasExceptionInPersist ;
        public bool HasExceptionInPersist { get { return hasExceptionInPersist; } }

        public PathPersisteCompleted(bool hasExceptionInPersist)
        {
            this.hasExceptionInPersist = hasExceptionInPersist;
        }
        public bool SameEventAs(PathPersisteCompleted other)
        {
            return true;
        }
    }
}
