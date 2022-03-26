using System;
using System.Collections.Generic;
using System.Text;

namespace Toka.Domain.DTO
{
    public class ServiceResponseDTO<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
