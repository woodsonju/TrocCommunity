using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrocCommunity.Core.Models
{
    public class Annonce :BaseEntity
    {
        public Client Client { get; set; }

        [ForeignKey("LivreId")]
        public Livre Livre { get; set; }

        public int LivreId { get; set; }

        [EnumDataType(typeof(string))]
        public EtatDuLivre EtatDuLivre { get; set; }

        public int PointDuLivre { get; set; }

        public DateTime date { get; set; }
    }
}