using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Data.Model
{

    public partial class GrupoDeAuditoria : BaseModel
    {

        public GrupoDeAuditoria()
        {
            Auditorias = new HashSet<Auditoria>();
        }

        [Required]
        public string Entidad { get; set; }


        public virtual ICollection<Auditoria> Auditorias { get; set; }

    }
}
