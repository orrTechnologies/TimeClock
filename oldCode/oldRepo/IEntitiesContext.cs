using System.Data.Entity;
using System.Threading.Tasks;

namespace TimeClock.Data
{
    public interface IEntitiesContext
    {
    //    IDbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "This is to match the EF terminology.")]
        IDbSet<T> Set<T>() where T : class;
        void DeleteOnCommit<T>(T entity) where T : class;
        void SetCommandTimeout(int? seconds);
        Database GetDatabase();
    }
}