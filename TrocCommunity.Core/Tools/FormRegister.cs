using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Tools
{
    public class FormRegister
    {
        [Required]
        [MaxLength(20), MinLength(3)]
        [Display(Name = "Nom Utilisateur")]
        public string UserName { get; set; }   //Pseud

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse e-mail ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        [Display(Name = "Mot de passe ")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        [Display(Name = "Confirmation mot de passe")]
        public string Confirmpwd { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date de Naissance")]
        public DateTime DateNaissance { get; set; }
    }
}
