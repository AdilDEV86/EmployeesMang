using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Employees.Model.User
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="le nom d'utilisateur est obligatoire")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "le Email est obligatoire")]
        public string Email { get; set; }
        [Required(ErrorMessage = "le Password est obligatoire")]
        public string Password { get; set; }
    }
}
