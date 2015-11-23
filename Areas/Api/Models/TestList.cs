using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAppNFC.Areas.Api.Models
{
    public class TestList
    {
        public int IdMateria { get; set; }
        public int IdTest { get; set; }
        public String Titulo { get; set; }
        public String Descripcion { get; set; }
        public int Promedio { get; set; }
        public String Estado { get; set; }
        public int Tiempo { get; set; }
    }
}