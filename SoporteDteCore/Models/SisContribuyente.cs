using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models
{
    public class SisContribuyente
    {
        public int ID { get; set; }
        public int Sis_contribuyente_id { get; set; }
        public string RazonSocial { get; set; }
        public string Rut { get; set; }
        public int IsOnline { get; set; }
        public bool IsOnServer { get; set; }
        public Estado Estado { get; set; }
    }
}
