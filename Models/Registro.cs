using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutentificacionAutorizacion.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public string Coordenadas { get; set; }
        public DateTime FechaBusqueda { get; set; }
        public int IdUsuario { get; set; }
    }
}