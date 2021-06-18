using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class Visita
    {
        public int Id { get; set; }
        [DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        [DisplayName("Numero Contacto")]
        public string NumeroContacto { get; set; }

        [DisplayName("Nombre Contacto")]
        public string NombreContacto { get; set; }

        [DisplayName("Motivo Visita")]
        public string MotivoVisita { get; set; }
        public string Observacion { get; set; }

        [DisplayName("Fecha Programacion")]
        public DateTime FechaProgramacion { get; set; }

        [DisplayName("Estado Actual")]
        public string EstadoActual { get; set; }

        [DisplayName("Estado Final")]
        public string EstadoFinal { get; set; }

        [DisplayName("Nombre Tecnico")]
        public string NombreTecnico { get; set; }

        public bool IsActive { get; set; }
    }
}
