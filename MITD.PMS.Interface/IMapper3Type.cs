using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public interface IMapper<TEntity1,TEntity2,TModel> : IMapper
        where TEntity1 : class
        where TEntity2 : class
        where TModel : new()

    {
        TModel MapToModel(TEntity1 entity1, TEntity2 entity2);
    }
}
