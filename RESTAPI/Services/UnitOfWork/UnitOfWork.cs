using Database;

namespace Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleStoreEntities db;
        public UnitOfWork(SimpleStoreEntities context)
        {
            db = context;
        }
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}