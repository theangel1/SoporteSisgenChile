using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class FacturaExportacion
    {
        public int Id { get; set; }
        [Required]
        public string RutCliente { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }

        public Estado Estado { get; set; }

        public string Observacion { get; set; }
    }
}
