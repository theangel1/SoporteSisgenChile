using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class Cliente
    {
        
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string Fantasia { get; set; }
        public string Giro { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string NombreCertificado { get; set; }
        public string ClaveCertificado { get; set; }
        public int Acteco { get; set; }
        public string NombreRepresentanteLegal { get; set; }
        public string RutRL { get; set; }
        public string FechaResolucion { get; set; }
        public int NumeroResolucion { get; set; }
        public string UnidadSII { get; set; }
    }
}
