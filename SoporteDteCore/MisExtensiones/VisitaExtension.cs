using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.MisExtensiones
{
    public static class VisitaExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItemVisita<T>(this IEnumerable<T> items, string selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("RazonSocial") + " - [" + DateTime.Parse(item.GetPropertyValue("FechaProgramacion")).ToString("dd/MM/yyyy") + "]",
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue)
                   };
        }
    }
}
