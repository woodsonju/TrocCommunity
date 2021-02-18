using System.Collections.Generic;

namespace TrocCommunity.Core.Models
{
    public class TableauDeBord : BaseEntity
    {
        public List<Livre> Livres { get; set; }
        public Client Client { get; set; }
        public List<Client> Clients { get; set; }
        public WishList WishList { get; set; }
        public LigneDeCommande LigneDeCommande { get; set; }
        public GestionnaireDePoint GestionnaireDePoint { get; set; }
        public BibliothequeVirtuelle BibliothequeVirtuelle { get; set; }
    }
}