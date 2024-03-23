using LeadFunnel.Domain;
using LeadFunnel.Interface.Repositories;
using LeadFunnel.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Services
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        private readonly IEntityRepository<T> _repo;

        public EntityService(IEntityRepository<T> repo)
        {
            _repo = repo;
        }

        public void Add(T entity)
        {
            _repo.Add(entity);
        }

        public void Delete(T entity)
        {
            _repo.Delete(entity);
        }

        public T Get(int Id)
        {
            return _repo.Get(Id);
        }

        public List<T> GetAll()
        {
            return _repo.GetAll();
        }

        public void Remove(int Id)
        {
            _repo.Remove(Id);
        }

        public void RemoveAll(List<T> entity)
        {
            _repo.RemoveAll(entity);
        }

        public void Update(T entity)
        {
            _repo.Update(entity);
        }
    }
}
