using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace RESTAppNFC.Areas.Api.Models
{
    public class EstudianteManager
    {

        public static int IngresarEstudiante(Estudiante Est)
        {
            int vResult = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spingresarestudiante", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                
                                new SqlParameter(){ParameterName = "@IdEstudiante", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.InputOutput, Value = Est.Id },
                                new SqlParameter(){ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.VarChar , Direction = System.Data.ParameterDirection.Input, Value = Est.Nombre, Size = 50 },
                                new SqlParameter(){ParameterName = "@Apellido", SqlDbType = System.Data.SqlDbType.VarChar , Direction = System.Data.ParameterDirection.Input, Value = Est.Apellido, Size = 50 },
                                new SqlParameter(){ParameterName = "@Usuario", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = string.IsNullOrEmpty(Est.Usuario)?"":Est.Usuario, Size =10 },
                                new SqlParameter(){ParameterName = "@Contrasenia", SqlDbType = System.Data.SqlDbType.VarChar , Direction = System.Data.ParameterDirection.Input, Value = string.IsNullOrEmpty(Est.Contrasenia)?"":Est.Contrasenia, Size = 10 },  
                                new SqlParameter(){ParameterName = "@NumeroMatricula", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Est.NumeroMatricula, Size = 10},
                                new SqlParameter(){ParameterName = "@Email", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = Est.Email, Size = 30 },
                               
                        };
                        cmd.Parameters.AddRange(pc);
                        cmd.ExecuteNonQuery();
                        vResult = Convert.ToInt32(cmd.Parameters["@IdEstudiante"].Value);
                        cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                vResult = -1;
            }
            return vResult;
        }


        public static Estudiante ObtenerEstudiantePorId(int Idestudiante)
        {
            Estudiante vResult = new Estudiante();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryEstudiantePorId", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                new SqlParameter(){ParameterName = "@idestudiante", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = Idestudiante }
                        };
                        cmd.Parameters.AddRange(pc);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                vResult.Id = Convert.ToInt32(dr["IdEstudiante"]);
                                vResult.Nombre = dr["Nombre"].ToString();
                                vResult.Apellido = dr["Apellido"].ToString();
                                vResult.NumeroMatricula = dr["NumeroMatricula"].ToString();
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


        public static Estudiante ObtenerParametrosLogin(string Usuario, string Clave)
        {
            Estudiante vResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryLoginEstudiante", cnn))
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
                            vResult = new Estudiante();
                            if (dr.Read())
                            {
                                vResult.Id = Convert.ToInt32(dr["IdEstudiante"]);
                                vResult.Nombre = dr["Nombre"].ToString();
                                vResult.Apellido = dr["Apellido"].ToString();
                                vResult.NumeroMatricula = dr["NumeroMatricula"].ToString();
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



        public static List<Materia> ObtenerMateriasXEstudiante(int idestudiante)
        {
            List<Materia> vResult = new List<Materia>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerMateriasXEstudiante", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEstudiante", idestudiante);
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
                vResult.Add(new Materia() { Nombre = e.Message });

            }
            return vResult;
        }

    }
}