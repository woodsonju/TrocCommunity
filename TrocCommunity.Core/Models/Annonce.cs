namespace TrocCommunity.Core.Models
{
    public class Annonce :BaseEntity
    {
        public Client Client { get; set; }
        public Livre Livre { get; set; }
        public EtatDuLivre EtatDuLivre { get; set; }
        public int PointDuLivre { get; set; }
    }
}