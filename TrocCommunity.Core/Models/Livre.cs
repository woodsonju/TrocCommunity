using System;
using System.ComponentModel.DataAnnotations;

namespace TrocCommunity.Core.Models
{
    public class Livre: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        public string Editor { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime DateEdition { get; set; }

        [Required]
        public long  ISBN { get; set; }

        [Required]
        public int Volume { get; set; }

        public string Language { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public Categorie Categorie { get; set; }

        public bool Disponible { get; set; }

        public Client Client { get; set; }

        [EnumDataType(typeof(string))]
        public EtatDuLivre EtatDuLivre { get; set; }

        public int PointDuLivre { get; set; }


    }
}