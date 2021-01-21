using System.Collections.Generic;

namespace TrocCommunity.Core.Models
{
    public class BibliothequeVirtuelle: BaseEntity
    {
        public List<Livre> Livres { get; set; }
    }
}