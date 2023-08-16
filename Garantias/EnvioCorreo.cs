using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Garantias
{
    class EnvioCorreo
    {
        //estado
        private bool estado()
        {
            return false;
        }
        /// <summary>
        /// metodo Datos Registros gf
        /// </summary>
        /// <param name="DatosRegistros"></param>
        /// 
        public static void EnviarCorreo( string DatosRegistros)
        {
            string sTextoMail = string.Empty;

            //sTextoMail += " <br/> Estimad@ : " + "<b>" + nombreagente + "</b>";
            sTextoMail += " <br/> Estimad@s : ";
            sTextoMail += " <br/><br/> A continuación sírvase encontrar las Garantías Proximas a caducar "; //+ "<b> <FONT COLOR='blue'>" + FechaProceso + "</b></FONT>";
            // string correoagente = "marcelo.tejada@aviacioncivil.gob.ec";

            string noreply = "no_reply@aviacioncivil.gob.ec";
            string asunto = "Garantías Proximas a Caducar";
            //sTextoMail += " <br/><br/>  Matricula:  " + "<b>" + Matricula + "</b>";
            if (DatosRegistros != "")
            {
                //sTextoMail += " <br/><br/> <b>Inconsistencia Peso Sobrevuelo:</b>";
                sTextoMail += " <br/><br/>" + DatosRegistros;
            }


            //sTextoMail += "<br/><br/><br/> Debe ingresar por la Opción Anulación Tarjetas de Crédito Switch, ingresar el código de tarjeta y Número de Referencia.";
            //sTextoMail += " <br/><br/>  Si desea consultar el Numero de Tarjeta de Credito, Ingresar en el menu SIGETAME, CONSULTAS, TRANSACCIONES SWITCH T/C,F7 Busqueda por Referencia";
            sTextoMail += "<br/><br/><br/> Por favor no responda a este correo, para mayor información sobre este documento, contactarse con personal de la direccion Financiera.";
            sTextoMail += "<br/><br/> Saludos Cordiales";
            sTextoMail += "<br/><br/><br/><br/>";

            try
            {
                //RECUPERA CORREOS
                //string Correousuario = "";
                //var Correos = CorreoUsuario.CorreosUsuario(Correousuario);

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(noreply); // Correo electronico que usara nuestra aplicacion mvc para enviar correos

                // correo.To.Add(Correos);
                correo.To.Add("marcelo.tejada@aviacioncivil.gob.ec");
                //correo.To.Add("digna.echeverria@aviacioncivil.gob.ec");
                //correo.To.Add("dmorales@aviacioncivil.gob.ec");
                //correo.To.Add("franklin.perez@aviacioncivil.gob.ec");


               // correo.CC.Add("luis.leonf@aviacioncivil.gob.ec");

                correo.Subject = asunto;
                correo.Body = sTextoMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //Configuracion del servidor smtp
                SmtpClient smtp = new SmtpClient("172.20.16.21");
                //SmtpClient smtp = new SmtpClient("172.20.17.87");
                smtp.Send(correo);


                //EnvioCorreo.EnvioCorreosIndividual enviadorCorreo = new EnvioCorreo.EnvioCorreosIndividual();

                //                enviadorCorreo.enviarCorreoDestinatarioVariasCopias(sTextoMail, "TRANSACCION TARJETA DE CREDITO SIN AUTOTORIZACION", correoagente, "noreply@tame.com.ec", correosCopia, "noreply@tame.com.ec");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }



        }//fin funcion

    }
}
