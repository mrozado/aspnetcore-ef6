using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Data.Model;
using BCP.Bussines;
using Microsoft.AspNetCore.Authorization;

namespace BCP.Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Persona")]
    //[Authorize]
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