using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toka.Domain.DTO;
using Toka.Domain.Models;

namespace Toka.Domain.Contracts
{
    public interface ICatalogosTokaRepository
    {
        //OBTENER PERSONAS FISICAS
        Task<ServiceResponseDTO<IEnumerable<Tb_PersonasFisicas>>> GetPersonasFisicas();
        Task<ServiceResponseDTO<Tb_PersonasFisicas>> GetPersonaFisica(Tb_PersonasFisicas persona);
        Task<ServiceResponseDTO<int>> SetPersonasFisicas(Tb_PersonasFisicas persona);
        Task<ServiceResponseDTO<int>> ChangePersonasFisicas(Tb_PersonasFisicas persona);
        Task<ServiceResponseDTO<int>> DeletePersonasFisicas(Tb_PersonasFisicas persona);
    }
}
