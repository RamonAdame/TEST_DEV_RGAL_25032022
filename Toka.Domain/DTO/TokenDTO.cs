using System;
using System.Collections.Generic;
using System.Text;

namespace Toka.Domain.DTO
{
    public class TokenDTO
    {
        public string Tokens { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
