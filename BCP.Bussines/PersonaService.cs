using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.Data;
using BCP.Data.Model;

namespace BCP.Bussines
{
    public class PersonaService : BaseService<Persona>
    {
        public PersonaService(BCPContext _context) : base(_context)
        {
        }
    }
}
