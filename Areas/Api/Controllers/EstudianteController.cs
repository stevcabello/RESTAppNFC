using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTAppNFC.Areas.Api.Models;

namespace RESTAppNFC.Areas.Api.Controllers
{
    public class EstudianteController : Controller
    {

        [HttpPost]
        public JsonResult GetParametrosLogin(Credenciales auten)
        {
            try
            {
                return Json(EstudianteManager.ObtenerParametrosLogin(auten.Usuario, auten.Clave));
            }
            catch (Exception e)
            {
                return Json(new { Error = true, Message = e.Message });
            }
        }


        public JsonResult Estudiante(int? id, Estudiante data)
        {
            try
            {
                int Temp = 0;
                switch (Request.HttpMethod)
                {
                    case "POST":
                        Temp = EstudianteManager.IngresarEstudiante(data);
                        return Json(Temp);
                    case "GET":
                        return Json(EstudianteManager.ObtenerEstudiantePorId(id.GetValueOrDefault()),
                                    JsonRequestBehavior.AllowGet);

                }
                return Json(new { Error = true, Message = "Operación HTTP desconocida" });
            }
            catch (Exception e)
            {
                string s = "No es nulo";
                if (data == null)
                    s = "Aja es nulo " + e.Message;
                else
                    s += e.Message;
                return Json(new { Error = true, Message = s });
            }
        }


        [HttpGet]
        public JsonResult ObtenerMateriasxEstudiante(int id)
        {
            return Json(EstudianteManager.ObtenerMateriasXEstudiante(id),
                        JsonRequestBehavior.AllowGet);
        }

    }
}
