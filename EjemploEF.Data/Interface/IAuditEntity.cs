using System;

namespace EjemploEF.Data.Interface
{
    public interface IAuditEntity 
    { 
        int GrupoDeAuditoriaId { get; set; }
        GrupoDeAuditoria Auditoria { get; set; }
    }
}
