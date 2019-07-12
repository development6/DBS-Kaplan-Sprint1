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
    public class MotivoController : Controller
    {
        // GET: Motivo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Motivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Motivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motivo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Motivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Motivo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Motivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Motivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        [HttpPost]
        public ContentResult AjaxMethod(string motivo)
        {
            //string Constring = "Data Source=LAPTOP-OBK0L9EC; Initial Catalog=Northwind; Integrated Security=True";
            string query = "SELECT id_estado, COUNT(id_reserva)";
            query += " FROM Reserva WHERE id_especialista = @EstadoRetiro GROUP BY id_estado";
            string constr = ConfigurationManager.ConnectionStrings["ConexionKaplan"].ConnectionString;
            StringBuilder sb = new StringBuilder();
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@EstadoRetiro", motivo);
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
    }
}
