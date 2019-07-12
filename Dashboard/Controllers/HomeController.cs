using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ContentResult AjaxMethod(string especialista)
        {
            //string Constring = "Data Source=LAPTOP-OBK0L9EC; Initial Catalog=Northwind; Integrated Security=True";
            string query = "SELECT T.nombre, COUNT(id_reserva)";
            query += " FROM Reserva R, TipoEstadoReserva T WHERE R.id_especialista = @Especialista And R.id_estado=T.id GROUP BY T.nombre";
            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Especialista", especialista);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sb.Append("[");
                        while (sdr.Read())
                        {
                            sb.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", sdr[0], sdr[1], color));
                            sb.Append("},");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("]");
                    }

                    con.Close();
                }
            }

            return Content(sb.ToString());
        }


        [HttpPost]
        public ContentResult AjaxMethod2(string motivo)
        {
            //string Constring = "Data Source=LAPTOP-OBK0L9EC; Initial Catalog=Northwind; Integrated Security=True";
            string query2 = "Select TM.nombre, COUNT(id_reserva)";
            query2 += " FROM TipoMotivo TM, Reserva RE WHERE RE.id_estado = @EstadoReserva And RE.id_motivo=TM.id GROUP BY TM.nombre";
            string constr2 = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb2 = new StringBuilder();
            using (SqlConnection con2 = new SqlConnection(constr2))
            {
                using (SqlCommand cmd2 = new SqlCommand(query2))
                {
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = con2;
                    cmd2.Parameters.AddWithValue("@EstadoReserva", motivo);
                    con2.Open();
                    using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                    {
                        sb2.Append("[");
                        while (sdr2.Read())
                        {
                            sb2.Append("{");
                            System.Threading.Thread.Sleep(50);
                            string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                            sb2.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", sdr2[0], sdr2[1], color));
                            sb2.Append("},");
                        }
                        sb2 = sb2.Remove(sb2.Length - 1, 1);
                        sb2.Append("]");
                    }

                    con2.Close();
                }
            }

            return Content(sb2.ToString());
        }


    }
}