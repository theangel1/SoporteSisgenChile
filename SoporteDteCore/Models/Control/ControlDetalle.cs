using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.Control
{
    public class ControlDetalle
    {
        public int Id { get; set; }
        public string Observacion { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ControlEstadoId { get; set; }

        [ForeignKey("ControlEstadoId")]
        public virtual ControlEstado ControlEstado { get; set; }
    }
}
