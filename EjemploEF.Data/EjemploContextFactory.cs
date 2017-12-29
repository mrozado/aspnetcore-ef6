using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEF.Data
{
    #region snippet_IDbContextFactory
    public class SchoolContextFactory : IDbContextFactory<EjemploContext>
    {
        public EjemploContext Create()
        {
            return new EjemploContext("data source = (localdb)\\MSSqlLocalDB; initial catalog = Ejemplo; integrated security = True; MultipleActiveResultSets = True; App = EntityFramework");
        }
    }
    #endregion
}
