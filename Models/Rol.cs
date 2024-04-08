using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutentificacionAutorizacion.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int rolAdmin = 2;
        public int rolInvitado = 3;
    }
}