using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RESTAppNFC.Areas.Api.Models
{
    public class DocenteManager
    {

        public static int IngresarDocente(Docente Doc)
        {
            int vResult = 0;
            //String vResult = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spingresardocente", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                
                                new SqlParameter(){ParameterName = "@IdDocente", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.InputOutput, Value = Doc.Id },
                                new SqlParameter(){ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar , Direction = System.Data.ParameterDirection.Input, Value = Doc.Nombre, Size = 50 },
                                new SqlParameter(){ParameterName = "@Apellido", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Doc.Apellido, Size = 50 },
                                new SqlParameter(){ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = string.IsNullOrEmpty(Doc.Usuario)?"":Doc.Usuario, Size =10 },
                                new SqlParameter(){ParameterName = "@Contrasenia", SqlDbType = System.Data.SqlDbType.VarChar , Direction = System.Data.ParameterDirection.Input, Value = string.IsNullOrEmpty(Doc.Contrasenia)?"":Doc.Contrasenia, Size = 10 },  
                                new SqlParameter(){ParameterName = "@Cedula", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Doc.Cedula, Size = 10},
                                new SqlParameter(){ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Doc.Email, Size = 30 }
                                //new SqlParameter(){ParameterName = "@tempidusuario", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.ReturnValue, Value = Doc.Id },
                        };


                        cmd.Parameters.AddRange(pc);
                        cmd.ExecuteNonQuery();
                        vResult = Convert.ToInt32(cmd.Parameters["@IdDocente"].Value);
                        cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                //vResult = e.Message;
                vResult = -1;
            }
            return vResult;
        }
       
        
        public static Docente ObtenerDocentePorId(int Iddocente)
        {
            Docente vResult = new Docente();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryDocentePorId", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                new SqlParameter(){ParameterName = "@iddocente", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = Iddocente }
                        };
                        cmd.Parameters.AddRange(pc);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                vResult.Id = Convert.ToInt32(dr["IdDocente"]);
                                vResult.Nombre = dr["Nombre"].ToString();
                                vResult.Apellido = dr["Apellido"].ToString();
                                vResult.Cedula = dr["Cedula"].ToString();
                                vResult.Email = dr["Email"].ToString();

                            }
                        }
                        dr.Close();
                        cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                //vResult = null;
                vResult.Id = 99;
                vResult.Nombre = e.Message;
                vResult.Apellido = "";
                vResult.Email = "";
            }
            return vResult;
        }


        public static Docente ObtenerParametrosLogin(string Usuario, string Clave)
        {
            Docente vResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryLoginDocente", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                new SqlParameter(){ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Usuario, Size = 10 },
                                new SqlParameter(){ParameterName = "@Clave", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Clave, Size = 10 }
                        };
                        cmd.Parameters.AddRange(pc);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            vResult = new Docente();
                            if (dr.Read())
                            {
                                vResult.Id = Convert.ToInt32(dr["IdDocente"]);
                                vResult.Nombre = dr["Nombre"].ToString();
                                vResult.Apellido = dr["Apellido"].ToString();
                                vResult.Cedula = dr["Cedula"].ToString();
                                vResult.Email = dr["Email"].ToString();
                            }
                        }
                        dr.Close();
                        cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                vResult = null;
            }
            return vResult;


        }


        public static List<Materia> ObtenerMateriasXDocente(int IdDocente)
        {
            List<Materia> vResult = new List<Materia>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerMateriasxDocente", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdDocente", IdDocente);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                vResult.Add(new Materia()
                                {

                                    Id = Convert.ToInt32(dr["IdMateria"]),
                                    Nombre = dr["Nombre"].ToString()

                                });
                            }
                        }
                        dr.Close();
                    }
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                //vResult.Add(new Materia() { Nombre = e.Message });

            }
            return vResult;
        }


    }
}