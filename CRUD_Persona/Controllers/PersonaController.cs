using System;
using System.Collections.Generic;
using System.Linq;
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

            using (CRUDEntities db = new CRUDEntities())
            {
                list = (from d in db.Persona
                        select new ListPersonaViewModel
                        {
                            Id = d.Id,
                            Rut = d.Rut,
                            Nombre = d.Nombre,
                            FecNac = d.FecNac,
                            Departamento = d.Departamento,
                            Salario = (int)d.Salario
                        }).ToList();
                recordsTotal= list.Count();

                list = list.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new { draw=draw, recordsFiltered=recordsTotal, recordsTotal=recordsTotal,data=list });
        }
    }
}