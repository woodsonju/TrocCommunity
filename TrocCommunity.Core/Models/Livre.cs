using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrocCommunity.Core.Models
{
    public class Livre: BaseEntity
    {
    /*    [Required]*/
        public string Title { get; set; }

    /*    [Required]*/
        public string Author { get; set; }

      /*  [Required]*/
        public string Editor { get; set; }

        
     /*   [DataType(DataType.Date)]*/
     //Retourne la date (yy)
        public string DateEdition { get; set; }

        [Required]
        public long  ISBN { get; set; }

      /*  [Required]*/
        public int Volume { get; set; }

        public string Language { get; set; }

       /* [Required]*/
        public string Image { get; set; }

        /*    [Required]*/

        [ForeignKey("CatgorieId")]
        public Categorie Categorie { get; set; }
        public int CatgorieId { get; set; }

        public bool Disponible { get; set; }


        private bool isExchange = false;

        public bool IsExchange
        {
            get { return isExchange; }
            set { isExchange = value; }
        }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        public EtatDuLivre EtatDuLivre { get; set; }

        public double PointDuLivre { get; set; }
        public string Description { get; set; }

        public double AvancePoints { get; set; }

        public double Price { get; set; }






    }
}