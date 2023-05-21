using CRUD_Persona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Persona.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        private readonly CRUD_DBEntities_Test db_read = new CRUD_DBEntities_Test();
        public ActionResult Index()
        {

            var departments = db_read.Departamentoes.Select(d => new SelectListItem
            { Value = d.Id.ToString(),
            Text = d.nombre_dpto
            }).ToList();

            ViewBag.Departments = departments;

            return View();
        }
    }
}