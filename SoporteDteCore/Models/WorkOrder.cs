using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La hora de llegada es requerida")]
        [DisplayName("Hora Llegada")]
        public DateTime HoraLlegada { get; set; }

        [Required(ErrorMessage = "La hora de salida es requerida")]
        [DisplayName("Hora Salida")]
        public DateTime HoraSalida { get; set; }

        [Required(ErrorMessage = "El Motivo de la Visita es requerido")]
        [DisplayName("Motivo Visita")]
        public string MotivoVisita { get; set; }

        [Required(ErrorMessage = "El Trabajo Realizado es requerido")]
        [DisplayName("Trabajo Realizado")]
        public string TrabajoRealizado { get; set; }

        public string Observaciones { get; set; }

        [DisplayName("Tecnico")]
        public string NombreTecnico { get; set; }

        [DisplayName("Firma")]
        public string NombreImagenFirma { get; set; }

        public string Derivado { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "El Nombre de la Persona es requerido")]
        [DisplayName("Nombre persona que recibe")]
        public string NombrePersonaRecibe { get; set; }

        //AGREGADO PARA QUE LE LLEGUE EL CORREO AL CLIENTE
        [Required(ErrorMessage = "El Correo Electrónico es requerido")]
        [DisplayName("Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El Correo Electrónico no tiene el formato correcto")]
        public string CorreoElectronico{ get; set; }

        [DefaultValue(0),DisplayName("Visita asociada")]
        public int?  VisitaId { get; set; }

        [ForeignKey("VisitaId")]
        public virtual Visita Visita { get; set; }

        public int? EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual Empresa Empresa { get; set; }
    }
}
