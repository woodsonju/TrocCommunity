namespace TrocCommunity.Core.Models
{
    public class LigneDeCommande: BaseEntity
    {
        public int NbreLivreEnvoye { get; set; }
        public int NbreLivreRecu { get; set; }
    }
}