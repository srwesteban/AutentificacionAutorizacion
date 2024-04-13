using AutentificacionAutorizacion.Datos;
using AutentificacionAutorizacion.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace AutentificacionAutorizacion.ViewModels
{
    public class VerificacionViewModel
    {

        public Usuario Usuario { get; set; }

        public string tokenEnviado { get; set; }

    }
}