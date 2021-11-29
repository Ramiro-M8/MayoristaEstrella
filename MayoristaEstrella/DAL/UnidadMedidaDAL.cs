using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class UnidadMedidaDAL
    {
        public List<UnidadMedida> GetUnidadMedidas(string connectionString)
        {
            List<UnidadMedida> UnidadMedidasList = new List<UnidadMedida>();
            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = "select IdUnidadMedida, DescripcionUnidadMedida  from UnidadMedida";
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    UnidadMedida UnidadMedida = new UnidadMedida();

                    UnidadMedida.IdUnidadMedida = Convert.ToInt32(dr["IdUnidadMedida"]);
                    UnidadMedida.DescripcionUnidadMedida = Convert.ToString(dr["DescripcionUnidadMedida"]);

                    UnidadMedidasList.Add(UnidadMedida);
                }
            }
            con.Close();
            return UnidadMedidasList;
        }

        public void CrearUnidadMedida(string connectionString, UnidadMedida UnidadMedida)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("CrearUnidadMedida", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@DescripcionUnidadMedida", UnidadMedida.DescripcionUnidadMedida));

                    con.Open();
                    cmd.ExecuteNonQuery(); // se usa para ABM no consultas
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public UnidadMedida GetUnidadMedidaData(string connectionString, int UnidadMedidaId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string selectSQL = "select IdUnidadMedida, DescripcionUnidadMedida  from UnidadMedida " +
                "where IdUnidadMedida = " + UnidadMedidaId;
            con.Open();
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader dr = cmd.ExecuteReader();
            UnidadMedida UnidadMedida = new UnidadMedida();
            if (dr != null)
            {
                while (dr.Read())
                {
                    UnidadMedida.IdUnidadMedida = Convert.ToInt32(dr["IdUnidadMedida"]);
                    UnidadMedida.DescripcionUnidadMedida = dr["DescripcionUnidadMedida"].ToString();
                }
            }
            con.Close();
            return UnidadMedida;
        }


        public void EditarUnidadMedida(string connectionString, UnidadMedida UnidadMedida)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateUnidadMedida", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", UnidadMedida.IdUnidadMedida));
                    cmd.Parameters.Add(new SqlParameter("@DescripcionUnidadMedida", UnidadMedida.DescripcionUnidadMedida));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteUnidadMedida(string connectionString, int UnidadMedidaId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteUnidadMedida", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdUnidadMedida", UnidadMedidaId));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
