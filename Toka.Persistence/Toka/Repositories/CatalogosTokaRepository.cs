using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toka.Domain.Contracts;
using Toka.Domain.DTO;
using Toka.Domain.Models;

namespace Toka.Persistence.Toka.Repositories
{
    public class CatalogosTokaRepository : ICatalogosTokaRepository
    {
        private readonly TokaContext _context;

        public CatalogosTokaRepository(TokaContext context)
        {
            _context = context;
        }

        //OBTENER LAS PERSONAS FISICAS
        public async Task<ServiceResponseDTO<IEnumerable<Tb_PersonasFisicas>>> GetPersonasFisicas()
        {
            //INSTANCIO EL MODELO QUE USO COMO SERVICIO PARA VALIDAR LA API
            ServiceResponseDTO<IEnumerable<Tb_PersonasFisicas>> service = new ServiceResponseDTO<IEnumerable<Tb_PersonasFisicas>>();
            try
            {
                var res = await _context.Tb_PersonasFisicas.Where(x => x.Activo == true).ToListAsync();
                if (res.Count > 0)
                {
                    service.Data = res;
                    service.Success = true;
                }
                else
                {
                    throw new Exception("SIn Registros");
                }
            }
            catch (Exception e)
            {
                service.Success = false;
                service.Message = $"Error favor de intentarlo nuevamente, mensaje de error: {e.Message}";
            }
            return service;
        }

        //OBTENER UNA PERSONA FISICA
        public async Task<ServiceResponseDTO<Tb_PersonasFisicas>> GetPersonaFisica(Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL MODELO QUE USO COMO SERVICIO PARA VALIDAR LA API
            ServiceResponseDTO<Tb_PersonasFisicas> service = new ServiceResponseDTO<Tb_PersonasFisicas>();
            try
            {
                var personaDB = await _context.Tb_PersonasFisicas.FirstOrDefaultAsync(x => x.IdPersonaFisica == persona.IdPersonaFisica);
                if (personaDB != null)
                {
                    service.Data = personaDB;
                    service.Success = true;
                }
                else
                {
                    throw new Exception("Error de consulta");
                }
            }
            catch (Exception e)
            {
                service.Success = false;
                service.Message = $"Error favor de intentarlo nuevamente, mensaje de error: {e.Message}";
            }
            return service;
        }

        //GUARDAR LAS PERSONAS FISICAS
        public async Task<ServiceResponseDTO<int>> SetPersonasFisicas(Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL MODELO QUE USO COMO SERVICIO PARA VALIDAR LA API
            ServiceResponseDTO<int> service = new ServiceResponseDTO<int>();
            try
            {
                //CADENA PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
                String sql = "dbo.sp_AgregarPersonaFisica @Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega,@Respuesta output";
                //INSTANCIO CADA UNO DE LOS PARAMETROS QUE ME PIDE EL PROCEDIMIENTO
                SqlParameter n = new SqlParameter("@Nombre", persona.Nombre);
                SqlParameter ap = new SqlParameter("@ApellidoPaterno", persona.ApellidoPaterno);
                SqlParameter am = new SqlParameter("@ApellidoMaterno", persona.ApellidoMaterno);
                SqlParameter rfc = new SqlParameter("@RFC", persona.RFC);
                SqlParameter fn = new SqlParameter("@FechaNacimiento", persona.FechaNacimiento.Value.Date);
                SqlParameter ua = new SqlParameter("@UsuarioAgrega", persona.UsuarioAgrega);
                SqlParameter salida = new SqlParameter("@Respuesta", 0) { Direction = ParameterDirection.Output };
                //REALIZO UN EXCEXUTESQL CON RAW PARA QUE LEA LA CADENA, LOS PARAMETROS Y LO EJECUTE
                await _context.Database.ExecuteSqlRawAsync(sql, n, ap, am, rfc, fn, ua, salida);
                //OBTENGO EL VALOR DE SALIDA PARA VALIDAR LA RESPUESTA Y ASIGNARLA AL OBJETO DATA
                var res = Convert.ToInt32(salida.Value);
                service.Data = res;

                //VALIDO SI EL RESULTADO ES SATISFACTORIO QUE ME AGREGUE AL OBJETO DATA DEL SERVICE RESPONSE
                //COMO TAMBIEN AÑADIR BANDERAS PARA VALIDAR EN FRONTEND
                if (res == 1)
                {
                    service.Success = true;
                    service.Message = "Registro Exitoso";
                }
                else if (res == 50001)
                {
                    throw new Exception("El RFC no es válido");
                }
                else if (res == 50002)
                {
                    throw new Exception("El RFC ya existe en el sistema");
                }

            }
            catch (Exception e)
            {
                service.Success = false;
                service.Message = e.Message;
            }
            return service;
        }

        //ACTUALIZAR LAS PERSONAS FISICAS
        public async Task<ServiceResponseDTO<int>> ChangePersonasFisicas(Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL MODELO QUE USO COMO SERVICIO PARA VALIDAR LA API
            ServiceResponseDTO<int> service = new ServiceResponseDTO<int>();
            try
            {
                //CADENA PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
                String sql = "dbo.sp_ActualizarPersonaFisica @IdPersonaFisica,@Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega,@Respuesta output";
                //INSTANCIO CADA UNO DE LOS PARAMETROS QUE ME PIDE EL PROCEDIMIENTO
                SqlParameter id = new SqlParameter("@IdPersonaFisica", persona.IdPersonaFisica);
                SqlParameter nombre = new SqlParameter("@Nombre", persona.Nombre);
                SqlParameter ap = new SqlParameter("@ApellidoPaterno", persona.ApellidoPaterno);
                SqlParameter am = new SqlParameter("@ApellidoMaterno", persona.ApellidoMaterno);
                SqlParameter rfc = new SqlParameter("@RFC", persona.RFC);
                SqlParameter fn = new SqlParameter("@FechaNacimiento", persona.FechaNacimiento);
                SqlParameter ua = new SqlParameter("@UsuarioAgrega", persona.UsuarioAgrega);
                SqlParameter salida = new SqlParameter("@Respuesta", 0) { Direction = ParameterDirection.Output };
                //REALIZO UN EXCEXUTESQL CON RAW PARA QUE LEA LA CADENA, LOS PARAMETROS Y LO EJECUTE
                await _context.Database.ExecuteSqlRawAsync(sql, id, nombre, ap, am, rfc, fn, ua, salida);
                //OBTENGO EL VALOR DE SALIDA PARA VALIDAR LA RESPUESTA Y ASIGNARLA AL OBJETO DATA
                var res = Convert.ToInt32(salida.Value);
                service.Data = res;

                //VALIDO SI EL RESULTADO ES SATISFACTORIO QUE ME AGREGUE AL OBJETO DATA DEL SERVICE RESPONSE
                //LOS OBJETOS DE LA PERSONA QUE SE AÑADIO COMO TAMBIEN UNA BANDERA PARA VALIDAR EN EL FRONTEND
                if (res == 1)
                {
                    service.Success = true;
                    service.Message = "Actualización Exitosa";
                }
                else if (res == 50000)
                {
                    throw new Exception("La persona fisica no existe.");
                }
            }
            catch (Exception e)
            {
                service.Success = false;
                service.Message = e.Message;
            }
            return service;
        }

        //ELIMINAR LAS PERSONAS FISICAS
        public async Task<ServiceResponseDTO<int>> DeletePersonasFisicas(Tb_PersonasFisicas persona)
        {
            //INSTANCIO EL MODELO QUE USO COMO SERVICIO PARA VALIDAR LA API
            ServiceResponseDTO<int> service = new ServiceResponseDTO<int>();
            try
            {
                //CADENA PARA EJECUTAR EL PROCEDIMIENTO ALMACENADO
                String sql = "dbo.sp_EliminarPersonaFisica @IdPersonaFisica,@Respuesta output";
                //INSTANCIO CADA UNO DE LOS PARAMETROS QUE ME PIDE EL PROCEDIMIENTO
                SqlParameter id = new SqlParameter("@IdPersonaFisica", persona.IdPersonaFisica);
                SqlParameter salida = new SqlParameter("@Respuesta", 0) { Direction = ParameterDirection.Output };
                //REALIZO UN EXCEXUTESQL CON RAW PARA QUE LEA LA CADENA, LOS PARAMETROS Y LO EJECUTE
                await _context.Database.ExecuteSqlRawAsync(sql, id, salida);
                //OBTENGO EL VALOR DE SALIDA PARA VALIDAR LA RESPUESTA Y ASIGNARLA AL OBJETO DATA
                var res = Convert.ToInt32(salida.Value);
                service.Data = res;

                //VALIDO SI EL RESULTADO ES SATISFACTORIO QUE ME AGREGUE AL OBJETO DATA DEL SERVICE RESPONSE
                //LOS OBJETOS DE LA PERSONA QUE SE AÑADIO COMO TAMBIEN UNA BANDERA PARA VALIDAR EN EL FRONTEND
                if (res == 1)
                {
                    service.Success = true;
                }
                else if (res == 50000)
                {
                    throw new Exception("La persona fisica no existe.");
                }
            }
            catch (Exception e)
            {
                service.Success = false;
                service.Message = e.Message;
            }
            return service;
        }
    }
}
