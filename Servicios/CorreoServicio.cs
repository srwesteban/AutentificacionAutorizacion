using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutentificacionAutorizacion.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Configuration;

namespace AutentificacionAutorizacion.Servicios
{
    public static class CorreoServicio
    {
        //private static string _Host = "smtp.gmail.com";
        //private static int _Puerto = 587;

        //private static string _NombreEnvia = "PetMap";
        //private static string _Correo = "sr.westeban@gmail.com";
        //private static string _Clave = "pslkleyhfgxllmmv";

        private static string _Host = ConfigurationManager.AppSettings["EmailHost"];
        private static int _Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
        private static string _NombreEnvia = ConfigurationManager.AppSettings["EmailFromName"];
        private static string _Correo = ConfigurationManager.AppSettings["EmailUsername"];
        private static string _Clave = ConfigurationManager.AppSettings["EmailPassword"];

        private static Random random = new Random();


        public static bool Enviar(Correo correodto)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correodto.Para));
                email.Subject = correodto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correodto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_Correo, _Clave);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerarToken()
        {
            const int longitudToken = 6;
            const string caracteresPermitidos = "0123456789";
            char[] token = new char[longitudToken];
            for (int i = 0; i < longitudToken; i++)
            {
                token[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }
            return new string(token);
        }

        public static string EnviarToken(Correo correodto)
        {
            try
            {
                string token = GenerarToken();
                string contenido = $"Tu token es: {token}";

                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correodto.Para));
                email.Subject = correodto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_Correo, _Clave);
                smtp.Send(email);
                smtp.Disconnect(true);
                return token;
            }
            catch
            {
                throw;
            }
        }
    }




}