using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EjemploEF.Data
{
    #region snippet_Constructor
    public class EjemploContext : DbContext
    {
        public EjemploContext(string connString) : base(connString)
        {
        }
        #endregion

        public DbSet<Persona> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}