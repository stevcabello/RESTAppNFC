using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAppNFC.Areas.Api.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public string NumeroMatricula { get; set; }
        public string Estado { get; set; }
      
    }
}