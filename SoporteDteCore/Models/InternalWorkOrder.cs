using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class InternalWorkOrder
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Rut Cliente")] //== A futuro obtener de una lista
        public string RutCliente { get; set; }

        [Required, DisplayName("Nombre Cliente")] //== A futuro se autocompleta según RUT cliente
        public string NombreCliente { get; set; }

        [Required, DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [Required, DisplayName("Fecha de Término")]
        public DateTime FechaTermino { get; set; }

        [DisplayName("Observación")]
        public string Observacion { get; set; }

        [Required, DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public bool IsValid { get; set; }

    }
}
