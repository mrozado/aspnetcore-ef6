using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCP.Data.Model;
using BCP.Bussines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BCP.Api.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Persona")]
    //[Authorize]
    public class PersonaController : Controller
    {
        private PersonaService personaService;

        private readonly ILogger logger;

        public PersonaController(PersonaService _personaService, ILogger<PersonaController> _logger)
        {
            personaService = _personaService;
            logger = _logger;   
        }

        [HttpGet]
        public List<Persona> Index()
        {
            this.logger.LogInformation("Logging Persona Index");
            try
            {
                return personaService.List().ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Persona Index");
                return null;
            }
        }
    }
}