using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTAppNFC.Areas.Api.Models;

namespace RESTAppNFC.Areas.Api.Controllers
{
    public class TestController : Controller
    {

        [HttpGet]
        public JsonResult Test()
        {
            return Json(TestManager.GetPlantillaTest(Convert.ToInt32(Request.Params["idestudiante"]),
                           Convert.ToInt32(Request.Params["idmateria"]), Convert.ToInt32(Request.Params["idtest"])),
                        JsonRequestBehavior.AllowGet);
        }


        public JsonResult TestCabecera(int? id, Test item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(TestManager.InsertarCabeceraTest(item));
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }


        public JsonResult TestDetalle(int? id, Test item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    //item.EncuestaNro = (int)id;
                    return Json(TestManager.InsertarDetalleTest(item));
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }


        [HttpGet]
        public JsonResult ObtenerTestxMateria(int id)
        {
            return Json(TestManager.ObtenerTestXMateria(id),
                        JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateTest()
        {
            return Json(TestManager.ActualizarEstadoTest(Convert.ToInt32(Request.Params["idmateria"]),
                           Convert.ToInt32(Request.Params["idtest"]), Request.Params["estado"],Convert.ToInt32(Request.Params["flag"]), Convert.ToInt32(Request.Params["tiempo"])),
                        JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult ObtenerTestActivosxMateria()
        {
            return Json(TestManager.ObtenerTestActivosXMateria(Convert.ToInt32(Request.Params["idmateria"]),
                          Convert.ToInt32(Request.Params["idestudiante"])),
                        JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerTestCompletosxMateria(int id)
        {
            return Json(TestManager.ObtenerTestCompletosXMateria(id),
                        JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CalificarTest()
        {
            return Json(TestManager.CalificarTest(Convert.ToInt32(Request.Params["idmateria"]),
                           Convert.ToInt32(Request.Params["idtest"]), Convert.ToInt32(Request.Params["idestudiante"])),
                        JsonRequestBehavior.AllowGet);
        }

    }
}
