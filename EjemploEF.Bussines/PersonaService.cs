using EjemploEF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEF.Bussines
{
    public class PersonaService : BaseService<Persona>
    {
        public PersonaService(EjemploContext _context) : base(_context)
        {
        }
    }
}
