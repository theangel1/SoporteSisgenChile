using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.Control
{
    public class ControlEstado
    {
        public int Id { get; set; }

        [Required, DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [Required, DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        [DisplayName("Servicio")]
        public TipoServicio TipoServicio { get; set; } = TipoServicio.FacturacionElectronica;

        [Required, DisplayName("Inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        public DateTime FechaFinalizacion { get; set; }

        public EstadoCertificacion Estado { get; set; } = EstadoCertificacion.SOLICITADO;

        public List<ControlDetalle> ControlDetalles { get; set; } = new List<ControlDetalle>();

        public string Email { get; set; }
        public string Telefono { get; set; }


    }
}
