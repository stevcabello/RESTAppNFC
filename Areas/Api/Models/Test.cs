using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAppNFC.Areas.Api.Models
{
    public class Test
    {
        public int IdMateria { get; set; }
        public int IdTest { get; set; }
        public int IdPregunta { get; set; }
        public int IdEstudiante { get; set; }
        public String Pregunta { get; set; }
        public List<OpcionesMultiples> OpcionesMultiples { get; set; }
        public int Opcion { get; set; }

    }

    public class OpcionesMultiples 
    {
        public int IdRespuesta { get; set; }
        public String Respuesta { get; set; }
    
    }
}