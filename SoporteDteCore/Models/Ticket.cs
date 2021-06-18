using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Rut Cliente")]
        public string RutCliente { get; set; }

        [Required]
        [DisplayName("Razón Social")]
        public string RazonSocial { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [DisplayName("Fecha de Cierre")]
        public DateTime FechaCierre { get; set; }

        [DisplayName("Técnico")]
        public string NombreTecnico { get; set; }

        public string Problema { get; set; }

        public bool IsActive { get; set; }

        [DisplayName("Tipo de estación")]
        public string TipoEstacion { get; set; }

      
        public string Prioridad { get; set; }
        /*estado pendiente, etc*/
        public string Estado { get; set; }

        public string IdDerivado { get; set; }

        [DisplayName("Nombre contacto")]
        public string NombreContacto { get; set; }

        [DisplayName("Teléfono contacto")]
        public string TelefonoContacto { get; set; }

        [ForeignKey("IdDerivado")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("NombreTecnico")]
        public virtual ApplicationUser Tecnico { get; set; }

        [NotMapped]
        public double TiempoResolucion
        {
            get
            {
                return Math.Round((FechaCierre - FechaCreacion).TotalMinutes, 2);
            }
        }
    }

    public class DetalleTicket
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        [Required]
        public string Detalle { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm tt}",
                       ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
    }
}
