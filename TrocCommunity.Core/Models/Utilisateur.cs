using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public abstract class Utilisateur : BaseEntity
    {

        /*        [Required]
        */      /*  [MaxLength(50), MinLength(2)]*/
        public string LastName { get; set; }

        /*[Required]*/
        /*[MaxLength(50), MinLength(2)]*/
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20), MinLength(3)]
      // [Display(Name = "Nom Utilisateur")]
        public string UserName { get; set; }   //Pseud

        /*[Required]*/
        /*[DataType(DataType.PhoneNumber)]*/
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
       // [Display(Name = "Adresse e-mail ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
       // [Display(Name = "Mot de passe ")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
      //  [Display(Name = "Entrez le mot de passe à nouveau ")]
        public string Confirmpwd { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        /*[Required]*/
        public TypeUtilisateur TypeUtilisateur { get; set; }

        [ForeignKey("AdresseId")]
        public Adresse Adresse { get; set; }

        public int AdresseId { get; set; }

        /// public bool Statut { get; set; }

        public string Photo { get; set; }

        public Utilisateur()
        {
           
        }


        //Constructeur permettant de faire l'injection de ma classe FormRegister dans Utilisateur
        public Utilisateur(string userName, string email, string password, string confirmpwd, DateTime dateNaissance)
        {
            UserName = userName;
            Email = email;
            Password = password;
            Confirmpwd = confirmpwd;
            DateNaissance = dateNaissance;
            TypeUtilisateur = TypeUtilisateur.CLIENT;  //Lors de la création d'un utilisateur il sera un Client par défaut
            Photo = "imgProfile.png";
            this.Adresse = new Adresse
            {
                TypeDeVoie = "",
                NomDeVoie = "",
                NumVoie = 0,
                CodePostale = "00000",
                Ville = "",
                Pays = ""
            };
        }

        public Utilisateur(int id, string userName, string email, string password, string confirmpwd, DateTime dateNaissance, TypeUtilisateur typeUtilisateur)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
            Confirmpwd = confirmpwd;
            DateNaissance = dateNaissance;
            TypeUtilisateur = typeUtilisateur;
        }

    }
}
