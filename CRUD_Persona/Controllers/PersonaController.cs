using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CRUD_Persona.Models;

namespace CRUD_Persona.Controllers
{
    public class PersonaController : Controller
    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        // GET: Persona

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Json()
        {
            List<ListPersonaViewModel> list = new List<ListPersonaViewModel>();
            //datatable parametros
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0 ;
            skip = start != null ? Convert.ToInt32(start) :  0 ;
            recordsTotal = 0;

            using (CRUD_DBEntities db = new CRUD_DBEntities())
            {
                list = (from d in db.Personas
                        select new ListPersonaViewModel
                        {
                            Id = (int)d.Id,
                            Rut = d.Rut,
                            Nombre = d.Nombre,
                            Email = d.Email,
                            Departamento = d.Departamento,
                            Telefono = d.Telefono
                        }).ToList();
                recordsTotal= list.Count();

                list = list.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new { draw=draw, recordsFiltered=recordsTotal, recordsTotal=recordsTotal,data=list });

        }
        public JsonResult Obtener(int Id)
        {
            Persona oPersona = new Persona();

            using (CRUD_DBEntities db = new CRUD_DBEntities())
            {
                oPersona = (from p in db.Personas.Where(x => x.Id == Id)
                            select p).FirstOrDefault();
            }
            return Json(oPersona, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Persona oPersona) {

            bool respuesta = true;
            try
            {
                using (CRUD_DBEntities db = new CRUD_DBEntities())
                {
                    int CheckExiste = (from p in db.Personas
                                       where p.Rut == oPersona.Rut
                                       select p).Count();
                    if (CheckExiste > 0) {
                        Persona tempPersona = (from p in db.Personas
                                               where p.Rut == oPersona.Rut
                                               select p).FirstOrDefault();
                        tempPersona.Rut = oPersona.Rut;
                        tempPersona.Nombre = oPersona.Nombre;
                        tempPersona.Email = oPersona.Email;
                        tempPersona.Departamento = oPersona.Departamento;
                        tempPersona.Telefono = oPersona.Telefono;

                        db.SaveChanges();
                    }
                    else
                    {
                        db.Personas.Add(oPersona);
                        db.SaveChanges();
                    }

                }
                /*
                if (oPersona.)
                {*/

                /*
                                }*/
                /*else
                {
                    using (CRUD_DBEntities db = new CRUD_DBEntities())
                    {
                        Persona tempPersona = (from p in db.Personas
                                               where p.Id == oPersona.Id
                                               select p).FirstOrDefault();
                        tempPersona.Rut = oPersona.Rut;
                        tempPersona.Nombre = oPersona.Nombre;
                        tempPersona.Email = oPersona.Email;
                        tempPersona.Departamento = oPersona.Departamento;
                        tempPersona.Telefono = oPersona.Telefono;

                        db.SaveChanges();
                    }
                }
                */
            }
            catch { 
                respuesta = false;
            }

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(int Id) {
            
            bool respuesta = true;
            try
            {
                using (CRUD_DBEntities db = new CRUD_DBEntities())
                {
                    Persona oPersona = new Persona();
                    oPersona = (from p in db.Personas.Where(x => x.Id == Id) select p).FirstOrDefault();

                    db.Personas.Remove(oPersona);

                    db.SaveChanges();
                }
            }
            catch 
            {
                respuesta = false;
            }
            
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}