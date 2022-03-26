using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toka.Domain.Contracts;
using Toka.Domain.DTO;
using Toka.Domain.Models;

namespace Toka.Controllers
{
    [Authorize]
    [Route("CatalogosApi/")]
    public class CatalogosApiController : Controller
    {
        private readonly ICatalogosTokaRepository _catalogosTokaRepository;

        public CatalogosApiController(ICatalogosTokaRepository catalogosTokaRepository)
        {
            _catalogosTokaRepository = catalogosTokaRepository;
        }

        //OBTENER LAS PERSONAS FISICAS
        [HttpGet("GetPersonas")]
        public async Task<IActionResult> GetPersonasFisicas()
        {
            //INSTANCIO EL SERVICE RESPONSE PARA VALIDAR EL ESTADO QUE SE RETORNARA
            ServiceResponseDTO<IEnumerable<Tb_PersonasFisicas>> response = await _catalogosTokaRepository.GetPersonasFisicas();
            if (response.Data == null)
            {
                //SI VIENE VACIO REGRESARA UN 404
                return NotFound(response);
            }
            //ESTE REGRESA UN 200 OK
            return Ok(response);
        }
        //OBTENER PERSONA FISICA
        [HttpGet("GetPersona")]
        public async Task<IActionResult> GetPersonaFisica(Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL SERVICE RESPONSE PARA VALIDAR EL ESTADO QUE SE RETORNARA
            ServiceResponseDTO<Tb_PersonasFisicas> response = await _catalogosTokaRepository.GetPersonaFisica(persona);
            if (response.Data == null)
            {
                //SI VIENE VACIO REGRESARA UN 404
                return NotFound(response);
            }
            //ESTE REGRESA UN 200 OK
            return Ok(response);
        }
        //GUARDAR LAS PERSONAS FISICAS
        [HttpPost("SetPersonas")]
        public async Task<IActionResult> SetPersonasFisicas([FromBody] Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL SERVICE RESPONSE PARA VALIDAR EL ESTADO QUE SE RETORNARA
            ServiceResponseDTO<int> response = await _catalogosTokaRepository.SetPersonasFisicas(persona);
            if (response.Data != 1)
            {
                //SI VIENE VACIO REGRESARA UN 404
                return NotFound(response);
            }
            //ESTE REGRESA UN 200 OK
            return Ok(response);
        }
        //ACTUALIZAR LAS PERSONAS FISICAS
        [HttpPut("ChangePersonas")]
        public async Task<IActionResult> ChangePersonasFisicas([FromBody] Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL SERVICE RESPONSE PARA VALIDAR EL ESTADO QUE SE RETORNARA
            ServiceResponseDTO<int> response = await _catalogosTokaRepository.ChangePersonasFisicas(persona);
            if (response.Data != 1)
            {
                //SI VIENE VACIO REGRESARA UN 404
                return NotFound(response);
            }
            //ESTE REGRESA UN 200 OK
            return Ok(response);
        }
        //ELIMINAR LAS PERSONAS FISICAS
        [HttpDelete("DeletePersonas")]
        public async Task<IActionResult> DeletePersonasFisicas([FromBody] Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL SERVICE RESPONSE PARA VALIDAR EL ESTADO QUE SE RETORNARA
            ServiceResponseDTO<int> response = await _catalogosTokaRepository.DeletePersonasFisicas(persona);
            if (response.Data != 1)
            {
                //SI VIENE VACIO REGRESARA UN 404
                return NotFound(response);
            }
            //ESTE REGRESA UN 200 OK
            return Ok(response);
        }
    }
}
