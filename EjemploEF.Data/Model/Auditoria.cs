using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEF.Data
{
    public partial class Auditoria : BaseModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Action { get; set; }
        
        [ForeignKey("GrupoDeAuditoria")]
        public int GrupoDeAuditoriaId { get; set; }

        public virtual GrupoDeAuditoria GrupoDeAuditoria { get; set; }
    }
}
