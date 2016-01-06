using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Repository;

namespace MITD.PMSSecurity.Domain.Repository
{
    public interface IPartyCustomActionRepository : IRepository<PartyCustomAction>
    {

        void DeleteAllByPartyId(long partyid);

    }
}
