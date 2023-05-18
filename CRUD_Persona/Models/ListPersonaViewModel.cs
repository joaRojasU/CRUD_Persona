using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Persona.Models
{
    public class ListPersonaViewModel
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Telefono { get; set; }

    }
}