using Pustok.Core.Entities;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Interfaces;


namespace Pustok.DataAccess.Repositories.Implementations
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
