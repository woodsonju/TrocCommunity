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
        public string FullName { get; set; }

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

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string PlaceId { get; set; }

    }


    
}
