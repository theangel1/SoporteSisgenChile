using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SoporteDteCore.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(10,
            ErrorMessage = "EL mínimo de caracteres es de 9 y el máximo es de 10",
            MinimumLength = 9)]
        [DisplayName("Rut Empresa")]
        public string RutEmpresa { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Razón Social")]
        public string RazonSocial { get; set; }

        [DisplayName("Nombre de Fantasia")]
        public string NombreFantasia { get; set; }

        [Required(ErrorMessage = "Este campo es requerido"), DisplayName("Correo de contacto")]
        public string CorreoElectronico { get; set; }

        /*Cambios requeridos por administracion*/
        [DisplayName("Estado Bloqueo")]
        public bool IsBlocked { get; set; }
        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public string Ciudad { get; set; }


        public IEnumerable<Correo> Correos { get; set; }
        
        [DisplayName("Teléfonos")]
        public IEnumerable<Telefono> Telefonos { get; set; }

        [DisplayName("Productos/Servicios adquiridos")]
        public IEnumerable<ProductosCliente> ProductosCliente { get; set; }

        [DisplayName("Observaciones")]
        public IEnumerable<ObservacionCliente> ObservacionCliente { get; set; }

      
        
        [NotMapped, DisplayName("Dirección")]
        public string FullDireccion
        {
            /* i know, fancy stuff... */
            get
            {
                return (string.IsNullOrEmpty(Direccion) ? " ": Direccion +
                    (  " " + (string.IsNullOrEmpty(Comuna) ? " " : Comuna + 
                    ( " " + (string.IsNullOrEmpty(Ciudad) ? " " : Ciudad + ".")))));
            }
        }

    }
}
