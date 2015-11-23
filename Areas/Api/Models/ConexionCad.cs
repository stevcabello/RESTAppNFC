using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAppNFC.Areas.Api.Models
{
    public class ConexionCad
    {

        static System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/RESTAppNFC");
        static System.Configuration.ConnectionStringSettings connString;


        public static string GetConnectionString()
        {
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["AppNFCCnn"];
                if (connString != null)
                    Console.WriteLine("AppNFCCnn connection string = \"{0}\"",
                        connString.ConnectionString);
                else
                    Console.WriteLine("No AppNFCCnn connection string");
            }
            return connString.ConnectionString;
        }

    }
}