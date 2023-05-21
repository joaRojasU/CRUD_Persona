using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Persona.Models
{
    public class UsuarioGrid
    {
        public int? Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string depto { get; set; }
        [Required]
        public string pefil { get; set; }

        public DateTime? fecha_vigencia { get; set; }
    }
}