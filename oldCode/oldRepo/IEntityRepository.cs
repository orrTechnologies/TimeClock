using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeClock.Data.Models;

namespace TimeClock.Data
{
    public interface IEntityRepository<T>
        where T : class, IEntity, new()
    {
        Task CommitChangesAsync();
        void DeleteOnCommit(T entity);
        T GetEntity(int key);
        IQueryable<T> GetAll();
        int InsertOnCommit(T entity);
    }

    public class EmployeeRepository : IEntityRepository<Employee>
    {
        public Task CommitChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteOnCommit(Employee entity)
        {
            throw new System.NotImplementedException();
        }

        public Employee GetEntity(int key)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Employee> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public int InsertOnCommit(Employee entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
