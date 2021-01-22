using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public abstract class Utilisateur : BaseEntity
    {
        [Required]
        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        
        [MaxLength(20), MinLength(6)]
        public string Pseudo { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        public string Confirmpwd { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required]
        //[EnumDataType(typeof(string))]
        public TypeUtilisateur typeUtilisateur { get; set; }

        [Required]
        public Adresse Adresse { get; set; }

        public bool Statut { get; set; }

        public string photo { get; set; }
    }
}
