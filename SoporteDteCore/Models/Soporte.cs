using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    //Clase que manejara los trabajos realizados los dias sabados y domingo
    public class Soporte
    {
        public int Id { get; set; }

        [DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        [DisplayName("Fecha de cierre")]
        public DateTime FechaFin { get; set; }

        [DisplayName("Rut Cliente"), Required]
        
        public string RutCliente { get; set; }

        [DisplayName("Razon Social"),Required]
        public string RazonSocialCliente { get; set; }
        public string TecnicoId { get; set; }
        public string Problema { get; set; }
        public string Descripcion { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("TecnicoId"), DisplayName("Técnico")]
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}
