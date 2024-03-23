using LeadFunnel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Interface.Repositories
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T Get(int Id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void RemoveAll(List<T> entity);
        void Remove(int Id);
        void SaveChanges();
    }
}
