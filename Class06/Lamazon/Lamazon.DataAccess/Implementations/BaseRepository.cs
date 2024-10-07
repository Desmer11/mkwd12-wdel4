using Lamazon.DataAccess.DataContext;

namespace Lamazon.DataAccess.Implementations
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
