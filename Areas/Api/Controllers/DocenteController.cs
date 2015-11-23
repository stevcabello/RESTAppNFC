using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTAppNFC.Areas.Api.Models;

namespace RESTAppNFC.Areas.Api.Controllers
{
    public class DocenteController : Controller
    {


        [HttpPost]
        public JsonResult GetParametrosLogin(Credenciales auten)
        {
            try
            {
                return Json(DocenteManager.ObtenerParametrosLogin(auten.Usuario, auten.Clave));
            }
            catch (Exception e)
            {
                return Json(new { Error = true, Message = e.Message });
            }
        }



        public JsonResult Docente(int? id, Docente data)
        {
            try
            {
                int Temp = 0;
                //String Temp = "";
                switch (Request.HttpMethod)
                {
                    case "POST":
                        Temp = DocenteManager.IngresarDocente(data);
                        return Json(Temp);
                    case "GET":
                        return Json(DocenteManager.ObtenerDocentePorId(id.GetValueOrDefault()),
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
        public JsonResult ObtenerMateriasxDocente(int id)
        {
            return Json(DocenteManager.ObtenerMateriasXDocente(id),
                        JsonRequestBehavior.AllowGet);
        }


       

    }
}
