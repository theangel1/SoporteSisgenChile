using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class FacturaElectronica
    {
        public int Id { get; set; }
        [Required, DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [Required, DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        [Required, DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        public Estado Estado { get; set; }

        public string Observacion { get; set; }

        public string Email { get; set; }
        public string Telefono { get; set; }

        public bool EmailSolicitado { get; set; }
        public bool EmailSisgenInstalado { get; set; }
        public bool EmailCertificando { get; set; }
        public bool EmailFinalizado { get; set; }
        public bool EmailInstalado { get; set; }
    }
}
