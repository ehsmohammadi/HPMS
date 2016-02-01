using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Data.NH;
using MITD.Domain.Repository;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Repository;

namespace MITD.PMSSecurity.Persistence.NH
{
    public class PartyCustomActionRepository : NHRepository<PartyCustomAction>, IPartyCustomActionRepository
    {
        public PartyCustomActionRepository(IUnitOfWorkScope unitofworkscope)
            : base(unitofworkscope)
        {
        }

        public void DeleteAllByPartyId(long partyid)
        {
            List<PartyCustomAction> partyCustomActions = GetAll().Where(pca => pca.Id == partyid).ToList();
            partyCustomActions.ForEach(pca=>Delete(pca));
        }
    }
}
