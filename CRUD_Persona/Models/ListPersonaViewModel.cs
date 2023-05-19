using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Persona.Models
{
    public class ListPersonaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Rut { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Departamento { get; set; }
        [Required]
        public string Telefono { get; set; }

    }
}