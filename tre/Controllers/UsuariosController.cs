using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tre.Models.VistasModel;
using tre.Models;
namespace tre.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            try
            {
                if (Session["Usuario"].ToString() =="")
                {
                    return Redirect("~/Login");
                }
            }
            catch (Exception)
            {

                return Redirect("~/Login");
            }
            List<ListaUsuarios> listaUsuarios = null;

            try
            {
                using (usuarioEntities db = new usuarioEntities())
                {
                    listaUsuarios = (from d in db.usuarios
                                     select new ListaUsuarios
                                     {
                                         id = d.id,
                                         usuario = d.usuario,
                                         clave = d.clave
                                     }
                                     ).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(listaUsuarios);
        }

        public ActionResult Editar(int id)
        {
            UsuariosViewModel us = new UsuariosViewModel();
            try
            {

                using (usuarioEntities db = new usuarioEntities())
                {
                    var usuarioEd = db.usuarios.Find(id);
                    us.id = usuarioEd.id;
                    us.clave = usuarioEd.clave;
                    us.usuario = usuarioEd.usuario;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(us);
        }
        [HttpPost]
        public ActionResult Editar(UsuariosViewModel us)
        {
            try
            {
                using (usuarioEntities db = new usuarioEntities())
                {

                    var usuarioEdit = db.usuarios.Find(us.id);
                    usuarioEdit.id = us.id;
                    usuarioEdit.usuario = us.usuario;
                    usuarioEdit.clave = us.clave;
                    db.Entry(usuarioEdit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("~/Usuarios");

                }

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        public ActionResult Nuevo()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(UsuariosViewModel us)
        {
            try
            {
                using (usuarioEntities db=new usuarioEntities())
                {
                    var usuarioNuevo = new usuarios();
                    usuarioNuevo.id = us.id;
                    usuarioNuevo.usuario = us.usuario;
                    usuarioNuevo.clave = us.clave;
                    db.usuarios.Add(usuarioNuevo);
                    db.SaveChanges();


                }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        public ActionResult Eliminar(int id)
        {

            try
            {
                using (usuarioEntities us=new usuarioEntities())
                {
                    var user=us.usuarios.Find(id);
                    us.usuarios.Remove(user);
                    us.SaveChanges();
                    return Redirect("~/Usuarios");

                }
            }
            catch (Exception)
            {

                throw;
            }
            return Redirect("~/Usuarios");
        }
        
    }
}