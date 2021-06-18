using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class Formulario
    {
        [Key]
        public int Id { get; set; }

        // Datos Empresa
        [Required, DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        [Required, DisplayName("Nombre de Fantasía")]
        public string NombreFantasia { get; set; }

        [Required, DisplayName("Rut Empresa")]
        public string RutEmpresa { get; set; }

        [Required]
        public string Giro { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required, DisplayName("Codigo actividad económica/Acteco")]
        public int CodigoActividad { get; set; }

        [Required, DisplayName("Correo de la empresa")]
        public string Correo { get; set; }

        // Datos Representante Legal
        [Required]
        public string Nombre { get; set; }

        [Required, DisplayName("Rut representante")]
        public string RutRep { get; set; }

        [Required, DisplayName("Número de serie carnet")]
        public string SerieCarnet { get; set; }

        [Required, DisplayName("Correo representante")]
        public string CorreoRep { get; set; }

        [Required, DisplayName("Direccion representante")]
        public string DireccionRep { get; set; }

        [Required, DisplayName("Telefono representante")]
        public string TelefonoRep { get; set; }

        // Validar si ya fue solicitado o no el certificado
        [Required, DefaultValue(false)]
        public bool solicitado { get; set; }
        public string NumeroPedido { get; set; }

        // Fecha de ingreso
        public DateTime FechaIngreso { get; set; }

        // Foreign Keys
        [Required, DisplayName("Región")]
        public int RegionId { get; set; }

        [Required, DisplayName("Provincia")]
        public int ProvinciaId { get; set; }

        [Required, DisplayName("Comuna")]
        public int ComunaId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }

        [ForeignKey("ProvinciaId")]
        public virtual Provincia Provincia { get; set; }

        [ForeignKey("ComunaId")]
        public virtual Comuna Comuna { get; set; }

        public string Nacionalidad { get; set; }
        public string FechaExpiracionCarnet { get; set; }


    }
}
