using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ProyectoCrud.Models;
using System.Data.SqlClient;
using System.Data;
namespace ProyectoCrud.Controllers
{
    public class ContactoController : Controller
    {

        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> listaContactos = new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {

            listaContactos = new List<Contacto>();

            using (SqlConnection conexionSQL = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO",conexionSQL);
                cmd.CommandType = CommandType.Text;
                conexionSQL.Open();

                using (SqlDataReader dr= cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();
                        nuevoContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        nuevoContacto.Nombres = dr["Nombres"].ToString();
                        nuevoContacto.Apellidos = dr["Apellidos"].ToString();
                        nuevoContacto.Telefono = dr["Telefono"].ToString();
                        nuevoContacto.Correo = dr["Correo"].ToString();

                        listaContactos.Add(nuevoContacto);
                    }
                }
            }


            return View(listaContactos);
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Contacto oContacto)
        {
            using (SqlConnection conexionSQL = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", conexionSQL);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                conexionSQL.Open();
                cmd.ExecuteNonQuery();

           
            }
            return RedirectToAction("Inicio","Contacto");
        }

        [HttpGet]
        public ActionResult Editar(int? idContacto)
        {
            if (idContacto == null)
                return RedirectToAction("Inicio", "Contacto");

            Contacto oContacto = listaContactos.Where(c =>c.IdContacto==idContacto).FirstOrDefault();
            return View(oContacto);
        }


        [HttpPost]
        public ActionResult Editar(Contacto oContacto)
        {
            using (SqlConnection conexionSQL = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", conexionSQL);
                cmd.Parameters.AddWithValue("IdContacto", oContacto.IdContacto);
                cmd.Parameters.AddWithValue("Nombres", oContacto.Nombres);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                conexionSQL.Open();
                cmd.ExecuteNonQuery();


            }
            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpGet]
        public ActionResult Eliminar(int? idcontacto)
        {
            if (idcontacto == null)
                return RedirectToAction("Inicio", "Contacto");

            Contacto ocontacto = listaContactos.Where(c => c.IdContacto == idcontacto).FirstOrDefault();
            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Eliminar(string IdContacto)
        {
            using (SqlConnection conexionSQL = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", conexionSQL);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                conexionSQL.Open();
                cmd.ExecuteNonQuery();


            }
            return RedirectToAction("Inicio", "Contacto");
        }
    }
}