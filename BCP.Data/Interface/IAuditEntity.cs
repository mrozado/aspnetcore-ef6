using BCP.Data.Model;
using System;

namespace BCP.Data.Interface
{
    public interface IAuditEntity 
    { 
        int GrupoDeAuditoriaId { get; set; }
        GrupoDeAuditoria Auditoria { get; set; }
    }
}
