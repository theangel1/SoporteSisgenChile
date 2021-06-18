using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.DTO
{
    public class InformeProduccionDto
    {
        public string NombreTecnico { get; set; }
        public string Cliente { get; set; }
        public int TiempoTranscurridoEnMinutos { get; set; }
        public int CantidadTicketsPendientes { get; set; }
        
    }
}
