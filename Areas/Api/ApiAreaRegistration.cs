using System.Web.Mvc;

namespace RESTAppNFC.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "ServiciosREST1",
              "Api/Docente/Docente/{id}",
              new { controller = "Docente", action = "Docente", id = UrlParameter.Optional }
          );

            context.MapRoute(
               "ServiciosREST2",
               "Api/Estudiante/Estudiante/{id}",
                new { controller = "Estudiante", action = "Estudiante", id = UrlParameter.Optional }
           );


            context.MapRoute(
               "ServiciosREST3",
               "Api/Docente/GetParametrosLogin",
               new { controller = "Docente", action = "GetParametrosLogin" }
           );


            context.MapRoute(
               "ServiciosREST4",
               "Api/Estudiante/GetParametrosLogin",
               new { controller = "Estudiante", action = "GetParametrosLogin" }
            );

            context.MapRoute(
             "ServiciosREST5",
             "Api/Docente/ObtenerMateriasXDocente/{id}",
             new { controller = "Docente", action = "ObtenerMateriasXDocente", id = UrlParameter.Optional }
          );


            context.MapRoute(
             "ServiciosREST6",
             "Api/Estudiante/ObtenerMateriasXEstudiante/{id}",
             new { controller = "Estudiante", action = "ObtenerMateriasXEstudiante", id = UrlParameter.Optional }
          );


            context.MapRoute(
                  "ServiciosREST7",
                  "Api/Test/Test",
                  new { controller = "Test", action = "Test"}
              );


            context.MapRoute(
               "ServiciosREST8",
               "Api/Test/TestCabecera/{id}",
               new { controller = "Test", action = "TestCabecera", id = UrlParameter.Optional }
           );

            context.MapRoute(
                "ServiciosREST9",
                "Api/Test/TestDetalle/{id}",
                new { controller = "Test", action = "TestDetalle", id = UrlParameter.Optional }
            );

            context.MapRoute(
             "ServiciosREST10",
             "Api/Test/ObtenerTestxMateria/{id}",
             new { controller = "Test", action = "ObtenerTestxMateria", id = UrlParameter.Optional }
          );

            context.MapRoute(
                "ServiciosREST11",
                "Api/Test/UpdateTest",
                new { controller = "Test", action = "UpdateTest" }
            );

            context.MapRoute(
            "ServiciosREST12",
            "Api/Test/ObtenerTestActivosxMateria",
            new { controller = "Test", action = "ObtenerTestActivosxMateria" }
         );


            context.MapRoute(
             "ServiciosREST13",
             "Api/Test/ObtenerTestCompletosxMateria/{id}",
             new { controller = "Test", action = "ObtenerTestCompletosxMateria", id = UrlParameter.Optional }
          );


            context.MapRoute(
                       "ServiciosREST14",
                       "Api/Test/CalificarTest",
                       new { controller = "Test", action = "CalificarTest" }
               );

            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
