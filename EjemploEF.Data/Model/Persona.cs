﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEF.Data
{
    public class Persona : BaseModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
