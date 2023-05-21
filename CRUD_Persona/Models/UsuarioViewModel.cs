using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Persona.Models
{
    public class UsuarioViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public int iddepto { get; set; }
        [Required]
        public int idpefil { get; set; }

        public DateTime? fecha_vigencia { get; set; }
    }
}