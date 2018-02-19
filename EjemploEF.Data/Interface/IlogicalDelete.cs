using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEF.Data.Interface
{
    public interface ILogicalDelete
    {
        bool IsDeleted { get; set; }
    }
}
