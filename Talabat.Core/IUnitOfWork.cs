using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepository<Tentity> Repository<Tentity>() where Tentity : BaseEntity;
        Task<int> CompleteAsync();
    }
}
