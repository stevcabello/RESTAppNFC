using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace RESTAppNFC.Areas.Api.Models
{
    public class TestManager
    {

        public static List<Test> GetPlantillaTest(int idestudiante, int idmateria, int idtest)
        {
            int IdIni = 0;
            List<Test> vResult = new List<Test>();
            OpcionesMultiples opcAnt = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerTest", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idestudiante", idestudiante);
                        cmd.Parameters.AddWithValue("@idmateria", idmateria);
                        cmd.Parameters.AddWithValue("@idtest", idtest);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (!IdIni.Equals(Convert.ToInt32(dr["IdPregunta"])))
                                {
                                    vResult.Add(new Test()
                                    {
                                        IdMateria = Convert.ToInt32(dr["IdMateria"]),
                                        IdTest = Convert.ToInt32(dr["IdTest"]),
                                        IdPregunta = Convert.ToInt32(dr["IdPregunta"]),
                                        Pregunta = dr["Pregunta"].ToString(),
                                        OpcionesMultiples = new List<OpcionesMultiples>()
                                    });
                                   
                                        opcAnt = new OpcionesMultiples();
                                        opcAnt.IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]);
                                        opcAnt.Respuesta = dr["Respuesta"].ToString();
                                        
                                    
                                }
                                else
                                {
                                    OpcionesMultiples item = new OpcionesMultiples();
                                    item.IdRespuesta = Convert.ToInt32(dr["IdRespuesta"]);
                                    item.Respuesta = dr["Respuesta"].ToString();
                                    if (opcAnt != null)
                                    {
                                        vResult[vResult.Count - 1].OpcionesMultiples.Add(opcAnt);
                                        opcAnt = null;
                                    }
                                    if (vResult[vResult.Count - 1].OpcionesMultiples == null)
                                    {
                                        vResult[vResult.Count - 1].OpcionesMultiples =
                                            new List<OpcionesMultiples>();
                                        vResult[vResult.Count - 1].OpcionesMultiples.Add(item);
                                    }
                                    else
                                        vResult[vResult.Count - 1].OpcionesMultiples.Add(item);
                                }

                                IdIni = Convert.ToInt32(dr["IdPregunta"]);
                            }
                        }
                    }
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return vResult;
        }


        public static string InsertarCabeceraTest(Test test, int Id = 0)
        {
            string vResult = "ok,";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spinsertarregistrocabecera", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] pc = 
                        {
                                new SqlParameter(){ParameterName = "@idmateria", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdMateria },
                                new SqlParameter(){ParameterName = "@idtest", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdTest},
                                new SqlParameter(){ParameterName = "@idestudiante", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdEstudiante  }
                                
                        };
                        cmd.Parameters.AddRange(pc);
                        cmd.ExecuteNonQuery();
                        //Id = Convert.ToInt32(cmd.Parameters["@i_Numero"].Value);
                        //vResult += Id.ToString();
                        cnn.Close();



                    }
                }
            }
            catch (Exception e)
            {
                vResult = e.Message;
            }
            return vResult;
        }


        public static string InsertarDetalleTest(Test test)
        {
            string vResult = "ok,";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spinsertarregistrodetalle", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc = 
                        {
                                new SqlParameter(){ParameterName = "@idmateria", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdMateria },
                                new SqlParameter(){ParameterName = "@idtest", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdTest },
                                new SqlParameter(){ParameterName = "@idestudiante", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdEstudiante },
                                new SqlParameter(){ParameterName = "@idpregunta", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.IdPregunta },
                                new SqlParameter(){ParameterName = "@opcion", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = test.Opcion  }
                        };
                        cmd.Parameters.AddRange(pc);
                        cmd.ExecuteNonQuery();
                        //vResult += Enc.EncuestaNro.ToString();
                        cnn.Close();
                    }
                }
            }
            catch (Exception e)
            {
                vResult = e.Message;
            }
            return vResult;
        }


        public static List<TestList> ObtenerTestXMateria(int IdMateria)
        {
            List<TestList> vResult = new List<TestList>();
            
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerListaTestxMateria", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmateria", IdMateria);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                vResult.Add(new TestList()
                                {

                                    IdMateria = Convert.ToInt32(dr["idmateria"]),
                                    IdTest = Convert.ToInt32(dr["idtest"]),
                                    Titulo = dr["titulo"].ToString(),
                                    Descripcion = dr["descripcion"].ToString(),
                                    Promedio = Convert.ToInt32(dr["promedio"]),
                                    Estado = dr["estado"].ToString(),
                                    Tiempo = Convert.ToInt32(dr["tiempo"])


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

            }
            return vResult;
        }

        public static int ActualizarEstadoTest(int idmateria,int idtest,String estado,int flag,int tiempo)
        {
            int vResult = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spupdatetest", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc1 = 
                        {
                           new SqlParameter(){ParameterName = "@idmateria", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = idmateria},
                           new SqlParameter(){ParameterName = "@idtest", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = idtest},
                           new SqlParameter(){ParameterName = "@flag", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.InputOutput, Value = flag},
                           new SqlParameter(){ParameterName = "@estado", SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Input, Value = string.IsNullOrEmpty(estado)?"":estado , Size = 1 },
                           new SqlParameter(){ParameterName = "@tiempo", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = tiempo}
                        };
                        cmd.Parameters.AddRange(pc1);
                        cmd.ExecuteNonQuery();
                        vResult = Convert.ToInt32(cmd.Parameters["@flag"].Value);
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

        public static List<TestList> ObtenerTestActivosXMateria(int IdMateria, int IdEstudiante)
        {
            List<TestList> vResult = new List<TestList>();
            

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerListaTestActivosxMateria", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmateria", IdMateria);
                        cmd.Parameters.AddWithValue("@idestudiante", IdEstudiante);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                vResult.Add(new TestList()
                                {

                                    IdMateria = Convert.ToInt32(dr["idmateria"]),
                                    IdTest = Convert.ToInt32(dr["idtest"]),
                                    Titulo = dr["titulo"].ToString(),
                                    Descripcion = dr["descripcion"].ToString(),
                                    Promedio = Convert.ToInt32(dr["promedio"]),
                                    Estado = dr["estado"].ToString(),
                                    Tiempo = Convert.ToInt32(dr["tiempo"])


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

            }
            return vResult;
        }

        public static List<TestList> ObtenerTestCompletosXMateria(int IdMateria)
        {
            List<TestList> vResult = new List<TestList>();
            

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.QryObtenerListaTestCompletosxMateria", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmateria", IdMateria);

                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                vResult.Add(new TestList()
                                {

                                    IdMateria = Convert.ToInt32(dr["idmateria"]),
                                    IdTest = Convert.ToInt32(dr["idtest"]),
                                    Titulo = dr["titulo"].ToString(),
                                    Descripcion = dr["descripcion"].ToString(),
                                    Promedio = Convert.ToInt32(dr["promedio"]),
                                    Estado = dr["estado"].ToString(),
                                    Tiempo = Convert.ToInt32(dr["tiempo"])


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

            }
            return vResult;
        }

        public static int CalificarTest(int idmateria, int idtest, int idestudiante)
        {
            int vResult = 1;
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConexionCad.GetConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.spcalificartest", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter[] pc1 = 
                        {
                           new SqlParameter(){ParameterName = "@idmateria", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = idmateria},
                           new SqlParameter(){ParameterName = "@idtest", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = idtest},
                           new SqlParameter(){ParameterName = "@idestudiante", SqlDbType = System.Data.SqlDbType.Int , Direction = System.Data.ParameterDirection.Input, Value = idestudiante}
                        };
                        cmd.Parameters.AddRange(pc1);
                        cmd.ExecuteNonQuery();
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
    }
}