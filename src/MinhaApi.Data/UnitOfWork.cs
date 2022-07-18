using MinhaApi.Core.Data;
using MinhaApi.Data.Context;

namespace MinhaApi.Data
{
    /// Classe com a implementação do padrão Unit Of Work
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MinhaApiContext _context;

        public UnitOfWork(MinhaApiContext context)
        {
            _context = context;
        }

        public bool CommitTransaction()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
