using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Maya.Tinker
{
    namespace PMNY.Web
    {
        public class BaseEntity
        {
            public int id { get; set; }
        }

        public interface IRepository<T> where T : BaseEntity
        {
            T Get(int id);
            DbSet<T> GetAll();
            SQLResults Insert(T entity);
            SQLResults Update(T entity);
            SQLResults Delete(int id);
        }

        public class AmazonRepository<T> : IRepository<T> where T : BaseEntity
        {
            private readonly DbContext _context;

            private DbSet<T> Entities => _context.Set<T>();


            public AmazonRepository()
            {
                //_context = new AmazonEntities();
            }


            public T Get(int id)
            {
                return Entities.SingleOrDefault(s => s.id == id);
            }

            public DbSet<T> GetAll()
            {
                return Entities;
            }

            public SQLResults Insert(T entity)
            {
                if (entity == null)
                {
                    return SQLResults.NotFound();
                }
                Entities.Add(entity);
                _context.SaveChanges();
                return SQLResults.Success();
            }

            public SQLResults Update(T entity)
            {
                if (entity == null)
                {
                    return SQLResults.NotFound();
                }
                Entities.AddOrUpdate(entity);
                _context.SaveChanges();
                return SQLResults.Success();
            }

            public SQLResults Delete(int id)
            {
                var entity = Entities.SingleOrDefault(s => s.id == id);
                if (entity == null)
                {
                    return SQLResults.NotFound();
                }
                Entities.Remove(entity);
                _context.SaveChanges();
                return SQLResults.Success();
            }

        }
    }

}
