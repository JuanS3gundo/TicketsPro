using System;
using System.Collections.Generic;

namespace Entity.DTOs
{
    public class TendenciaTicketsDTO
    {
        public Dictionary<DateTime, int> Creados { get; set; }
        public Dictionary<DateTime, int> Resueltos { get; set; }
    }
}
