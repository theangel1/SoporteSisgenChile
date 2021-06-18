using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class CertificadoDigital
    {
        public int Id { get; set; }
        [Required]
        public string NombreEmpresa { get; set; }
        [Required]
        public string RutEmpresa { get; set; }
        [Required, DisplayName("Representante Legal")]
        public string NombreRepresentanteLegal { get; set; }
        [Required, DisplayName("Rut R. Legal")]
        public string RutRepresentanteLegal { get; set; }
        [Required, DisplayName("Fecha Emision")]
        public DateTime FechaEmision { get; set; }
        [Required, DisplayName("Fecha Vencimiento")]
        public DateTime FechaVencimiento { get; set; }

       [NotMapped]
        public int DiasRestantes 
        {
            get
            {
                return (int)((FechaVencimiento - DateTime.Now).TotalDays);
            }
        }
    }
}
