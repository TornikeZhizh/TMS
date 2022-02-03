using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.DataAccess.Impl
{
    public abstract class DataAccessBase
    {
        protected readonly DataContext _context;

        public DataAccessBase(DataContext context)
        {
            _context = context;
        }
    }
}
