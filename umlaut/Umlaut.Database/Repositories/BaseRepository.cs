using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umlaut.Database.Repositories
{
    public class BaseRepository
    {
        protected readonly UmlautDBContext _context;

        public BaseRepository(UmlautDBContext context)
        {
            _context = context;
        }

    }
}
