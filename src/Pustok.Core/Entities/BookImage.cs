using Pustok.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Core.Entities
{
    public class BookImage : BaseSectionEntity
    {
        public string Image { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
