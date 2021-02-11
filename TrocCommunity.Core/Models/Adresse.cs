using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrocCommunity.Core.Models
{
    public class Adresse : BaseEntity
    {
     /*   [Required]*/
        public string TypeDeVoie { get; set; }

    /*    [Required]*/
        public string NomDeVoie { get; set; }

 /*       [Required] 
        [Range(1, 100000)]*/
        public int NumVoie { get; set; }

  /*      [Required]
        [DataType(DataType.PostalCode)]*/
        public string CodePostale { get; set; }

     /*   [Required]*/
        public string  Ville { get; set; }

      /*  [Required]*/
        public string Pays { get; set; }
/*
        [Required]
        public Utilisateur Utilisateur { get; set; }*/
    }


    
}
/*Adresse FindByUtilisateur(int utilisateurId);*/