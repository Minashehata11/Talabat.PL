using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;
        private Hashtable _respositories;
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            _respositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        => await _storeDbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _storeDbContext.DisposeAsync();  

        public IGenericRepository<Tentity> Repository<Tentity>() where Tentity : BaseEntity
        {
            var type = typeof(Tentity).Name; //product //address
            if (!_respositories.ContainsKey(type))
            {
                var Repository=new GenericRepository<Tentity>(_storeDbContext);
                _respositories.Add(type, Repository);   
            }
            return (IGenericRepository<Tentity>) _respositories[type];
        }
    }
}
