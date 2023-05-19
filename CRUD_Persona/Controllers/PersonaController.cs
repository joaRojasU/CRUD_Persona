using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static bool ValidaRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Método que valida el rut con el digito verificador
        /// por separado
        /// </summary>
        /// <param name="rut">integer</param>
        /// <param name="dv">char</param>
        /// <returns>booleano</returns>
        public static bool ValidaRut(string rut, string dv)
        {
            return ValidaRut(rut + "-" + dv);
        }

        /// <summary>
        /// método que calcula el digito verificador a partir
        /// de la mantisa del rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
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
                            Id = d.Id,
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
            bool rutCheck;
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
                        rutCheck = ValidaRut(oPersona.Rut);
                        if (rutCheck)
                        {
                            tempPersona.Rut = oPersona.Rut;
                            tempPersona.Nombre = oPersona.Nombre;
                            tempPersona.Email = oPersona.Email;
                            tempPersona.Departamento = oPersona.Departamento;
                            tempPersona.Telefono = oPersona.Telefono;

                            db.SaveChanges();
                        }
                        else
                        {
                            respuesta = false;
                        }
                        
                    }
                    else
                    {
                        rutCheck = ValidaRut(oPersona.Rut);
                        if (rutCheck)
                        {
                            db.Personas.Add(oPersona);
                            db.SaveChanges();
                        }
                        else
                        {
                            respuesta = false;
                        }
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
        
        public ActionResult Buscar(string filtro, string searchInput)     {
                  
            

           
            List<ListPersonaViewModel> list = new List<ListPersonaViewModel>();
            List<ListPersonaViewModel> list2 = new List<ListPersonaViewModel>();
            bool respuesta = true;
            using (CRUD_DBEntities db = new CRUD_DBEntities())

            {
                list2 = (from d in db.Personas
                        select new ListPersonaViewModel
                        {
                            Id = d.Id,
                            Rut = d.Rut,
                            Nombre = d.Nombre,
                            Email = d.Email,
                            Departamento = d.Departamento,
                            Telefono = d.Telefono
                        }).ToList();

                if (filtro == "filtroRut")
                {
                    list = list2.Where(x => x.Rut.Contains(searchInput)).ToList();
                    
                    recordsTotal = list.Count();
                    respuesta = true;
                }
                else if (filtro == "filtroNombre")
                {
                    list = list2.Where(x => x.Nombre.Contains(searchInput)).ToList();
                    recordsTotal = list.Count();
                    respuesta = true;
                }
                else if (filtro == "filtroEmail")
                {
                    list = list2.Where(x => x.Email.Contains(searchInput)).ToList();
                    recordsTotal = list.Count();
                    respuesta = true;
                }
                else if (filtro == "filtroDepartamento")
                {
                    list = list2.Where(x => x.Departamento.Contains(searchInput)).ToList();
                    recordsTotal = list.Count();
                    respuesta = true;
                }
                else if (filtro == "filtroTelefono")
                {
                    list = list2.Where(x => x.Telefono.Contains(searchInput)).ToList();
                    recordsTotal = list.Count();
                    respuesta = true;
                }
                else {
                    respuesta = false;
                }
                draw = "1";


            }

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }
    }
}