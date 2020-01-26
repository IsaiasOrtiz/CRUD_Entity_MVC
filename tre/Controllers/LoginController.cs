using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tre.Models;
using tre.Models.VistasModel;

namespace tre.Controllers
{
    public class LoginController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login(LoginViewModel m)
        {

            try
            {
                using (usuarioEntities db = new usuarioEntities())
                {
                    var user = (db.usuarios.Where(d => d.usuario.Equals(m.usuario) && d.clave.Equals(m.clave)).FirstOrDefault());
                    if (user != null)
                    {
                        Session["Usuario"] = user.usuario;
                        return Redirect("~/Usuarios");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Redirect("~/Login"); ;
        }
    }
}