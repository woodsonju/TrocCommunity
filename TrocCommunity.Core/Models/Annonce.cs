using System.ComponentModel.DataAnnotations;

namespace TrocCommunity.Core.Models
{
    public class Annonce :BaseEntity
    {
        public Client Client { get; set; }

        public Livre Livre { get; set; }

        [EnumDataType(typeof(string))]
        public EtatDuLivre EtatDuLivre { get; set; }

        public int PointDuLivre { get; set; }
    }
}