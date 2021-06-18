using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public enum Servicios
    {
        Sisgen,//0
        Siscon,//1
        Sisrem,
        Movigen,
        FE_Acepta,
        FE_Directa,
        Soporte_Basico,
        Soporte_Premium,
        Soporte_Full,
        BE_Directa,
        Respaldo_Datos,
        Sist_Restaurant //11

    }

    public class ProductosCliente
    {
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public Servicios Servicios { get; set; }

    }
}
