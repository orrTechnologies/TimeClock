using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeClock.Data;
using System.Data.Entity;
   
namespace WebApplication1.Tests.TestUtils
{
    public class FakeEntitiesContext : IEntitiesContext
    {
        private readonly Dictionary<Type, object> dbSets = new Dictionary<Type, object>();
        private bool _areChangesSaved;

        public Task<int> SaveChangesAsync()
        {
            _areChangesSaved = true;
            return Task.FromResult(0);
        }

        public IDbSet<T> Set<T>() where T : class
        {
            if (!dbSets.ContainsKey(typeof(T)))
            {
                dbSets.Add(typeof(T), new FakeDbSet<T>(this));
            }

            return (IDbSet<T>)(dbSets[typeof(T)]);
        }

        public void DeleteOnCommit<T>(T entity) where T : class
        {
            ((FakeDbSet<T>)(Set<T>())).Remove(entity);
        }

        public void VerifyCommitChanges()
        {
            Assert.IsTrue(_areChangesSaved, "SaveChanges() has not been called on the entity context.");
        }


        public void SetCommandTimeout(int? seconds)
        {
            throw new NotSupportedException();
        }

        public Database GetDatabase()
        {
            throw new NotSupportedException();
        }
    }
}
