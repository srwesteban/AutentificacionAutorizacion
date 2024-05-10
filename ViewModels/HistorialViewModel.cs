using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutentificacionAutorizacion.ViewModels
{
    public class HistorialViewModel
    {
        public List<Registro> Registros { get; set; }
        public string IdUsuario { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}