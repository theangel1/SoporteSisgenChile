using SendGrid;
using SendGrid.Helpers.Mail;
using SoporteDteCore.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Services
{
    public static class EnviarEmail
    {
        public async static Task<Response> Enviar(string email, ControlEstado cEstado)
        {
            string servicio = string.Empty, tipoDocumento = string.Empty;

            switch (cEstado.Estado)
            {
                case EstadoCertificacion.SOLICITADO:
                    servicio = "Solicitado";
                    break;
                case EstadoCertificacion.VERIFICACION:
                    servicio = "Verificación";
                    break;
                case EstadoCertificacion.CERTIFICACION:
                    servicio = "Certificación";
                    break;
                case EstadoCertificacion.INSTALACION:
                    servicio = "Instalación";
                    break;
                case EstadoCertificacion.FIN_PROCESO:
                    servicio = "Proceso Finalizado";
                    break;
                default:
                    break;
            }

            switch (cEstado.TipoServicio)
            {
                case TipoServicio.FacturacionElectronica:
                    tipoDocumento = "Factura Electrónica";
                    break;
                case TipoServicio.BoletaElectronica:
                    tipoDocumento = "Boleta Electrónica";
                    break;
                case TipoServicio.FacturaExportacion:
                    tipoDocumento = "Factura Exportación Electrónica";
                    break;
                default:
                    break;
            }

            string body = string.Format("<h3 style='color:blue;'>Datos {0}</h3>" +
                "<strong>Cliente:</strong> {1} <br>" +
                "<strong>Id:</strong> {2} <br>" +
                "<strong>Rut Cliente:</strong> {3} <br>" +
                "<strong>Razón Social:</strong> {4} <br>" +
                "<strong>Fecha Inicio:</strong> {5} <br>" +
                "<strong style='color:darkorange'>Estado:</strong> {6} <br>" +
                "<hr><br><code>Atte. Equipo de informática Sisgen Chile</code>"
                 , tipoDocumento, cEstado.RazonSocial, cEstado.Id, cEstado.RutCliente, cEstado.RazonSocial, cEstado.FechaInicio, servicio);


            var plainTextContent = "";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("do-not-reply@sisgenchile.cl", "Equipo de Desarrollo Sisgen Chile");
            var to = new EmailAddress(email);

            var subject = tipoDocumento + " -> " + cEstado.RazonSocial;
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msg.AddTo("angel.pinilla@sisgenchile.cl");


            if (cEstado.Estado == EstadoCertificacion.SOLICITADO || cEstado.Estado == EstadoCertificacion.FIN_PROCESO)
                msg.AddCc("nicolas.cortes@sisgenchile.cl"); /*a nicolas*/

            if (cEstado.Estado == EstadoCertificacion.VERIFICACION || cEstado.Estado == EstadoCertificacion.INSTALACION)
                msg.AddCc("mauricio.olivares@sisgenchile.cl"); /*a mauricio*/

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response;

        }

        public async static Task<Response> EnvioCorreoSolicitud(string emailSolicitud)
        {
            string body = string.Format("<h3 style='color:blue;'>Formulario Certificado Digital</h3>" +
                                        "<strong>Estimados(as),</strong><br>" +
                                        "<strong>Me presento soy María Ortega, Control de Gestión de Sisgen Chile.</strong><br>" +
                                        "<strong>Se requiere que completen el siguiente <a href='https://soporte.sisgenchile.cl/Guest/Home/Formulario' target='blank'>formulario</a>, basta con que lo completen solo una vez.</strong><br>" +
                                        "<strong>Por último, con estos datos se solicitará un certificado digital y a continuación se realizará la certificación de Factura y Boleta Electrónica. </strong><br>" +
                                        "<strong>Quedo atenta a sus comentarios.</strong><br>" +
                                        "<strong>Saludos cordiales</strong><br>" +
                                        "<hr><br><code>Atte. Depto. de Administración y Finanzas</code>");

            var plainTextContent = "";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("do-not-reply@sisgenchile.cl", "Administración y Finanzas Sisgen Chile");
            var to = new EmailAddress(emailSolicitud);
            

            var subject = "Solicitud Formulario Certificado Digital";
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            msg.AddTo("maria.ortega@sisgenchile.cl");

            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response;
        }
    }
}
