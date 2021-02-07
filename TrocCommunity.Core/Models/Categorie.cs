using System.Collections.Generic;

namespace TrocCommunity.Core.Models
{
    public class Categorie: BaseEntity
    {
        public string NomCategorie { get; set; }
        public string Description { get; set; } //sous description
        public List<Livre> Livres { get; set; }
    }
}