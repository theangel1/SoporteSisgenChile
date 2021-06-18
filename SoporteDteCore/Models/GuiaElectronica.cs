using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class GuiaElectronica 
    {
        public int Id { get; set; }

        [Required, DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [Required, DisplayName("Razon Social")]
        public  string RazonSocial { get; set; }

        [Required, DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        public Estado Estado { get; set; }

        public string Observacion { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }


    }
}
