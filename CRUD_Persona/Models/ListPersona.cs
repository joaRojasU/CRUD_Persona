using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Persona.Models
{
    public class ListPersona
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string FecNac { get; set; }
        public string Departamento { get; set; }
        public int Salario { get; set; }
    }
}