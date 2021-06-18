using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoporteDteCore.Models
{
    public class Problema
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = "El nombre del problema es obligatorio")]
        [MaxLength(256,
            ErrorMessage = "El nombre no puede contener mas de 256 caractes ni menos de 5"),
            MinLength(5)]
        public string Nombre {get;set ;}
    }
}
