using MailKit.Net.Smtp;
using MimeKit;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Utility
{
    public static class MailSend
    {
        public static bool SendMail(FacturaElectronica factura, BoletaElectronica boleta, string addresses)
        {
            try
            {
                string type = "default";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Informatica Sisgen", "informatica@sisgenchile.cl"));

                foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(InternetAddress.Parse(address));
                }

                if (factura != null)
                {
                    type = "factura";
                }

                if (boleta != null)
                {
                    type = "boleta";
                }

                //if (guia != null)
                //{
                //    type = "guia";
                //}

                switch (type)
                {
                    case "boleta":
                        message.Subject = "Boleta Electrónica: " + boleta.RazonSocial;
                        message.Body = new TextPart("plain")
                        {
                            Text = @"Datos Boleta Electrónica Cliente: " + boleta.RazonSocial + "\n" +
                                    "Id: " + boleta.Id + "\n" +
                                    "Rut Cliente: " + boleta.RutCliente + "\n" +
                                    "Razón Social: " + boleta.RazonSocial + "\n" +
                                    "Fecha Inicio: " + boleta.FechaInicio + "\n" +
                                    "Estado: " + boleta.Estado + "\n" +
                                    "Observación: " + boleta.Observacion + "\n",
                        };
                        break;

                    case "factura":
                        message.Subject = "Factura Electrónica: " + factura.RazonSocial;
                        message.Body = new TextPart("plain")
                        {
                            Text = @"Datos Factura Electrónica Cliente: " + factura.RazonSocial + "\n" +
                                    "Id: " + factura.Id + "\n" +
                                    "Rut Cliente: " + factura.RutCliente + "\n" +
                                    "Razón Social: " + factura.RazonSocial + "\n" +
                                    "Fecha Inicio: " + factura.FechaInicio + "\n" +
                                    "Estado: " + factura.Estado + "\n",
                        };
                        break;

                        //case "guia":
                        //    message.Subject = "Guia Electrónica: " + guia.RazonSocial;
                        //    message.Body = new TextPart("plain")
                        //    {
                        //        Text = @"Datos Guia Electrónica Cliente: " + guia.RazonSocial + "\n" +
                        //                "Id: " + guia.Id + "\n" +
                        //                "Rut Cliente: " + guia.RutCliente + "\n" +
                        //                "Razón Social: " + guia.RazonSocial + "\n" +
                        //                "Fecha Inicio: " + guia.FechaInicio + "\n" +
                        //                "Estado: " + guia.Estado + "\n" +
                        //                "Observación: " + guia.Observacion + "\n",
                        //    };
                        //    break;
                }

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("informatica@sisgenchile.cl", "T&7h55R666");

                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
