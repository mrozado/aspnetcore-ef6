using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EjemploEF.Data;
using EjemploEF.Bussines;

namespace EjemploEF.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Persona")]
    public class PersonaController : Controller
    {
        private PersonaService personaService;

        public PersonaController(PersonaService _personaService)
        {
            personaService = _personaService;
        }

        [HttpGet]
        public List<Persona> Index()
        {
            return personaService.List().ToList();
        }
    }
}