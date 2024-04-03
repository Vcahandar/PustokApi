using Pustok.Core.Entities;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Repositories.Implementations
{
    public class BookRepository :Repository<Book>,IBookRepository 
    {
        public BookRepository(AppDbContext context) : base(context) { }
      
    }
}
