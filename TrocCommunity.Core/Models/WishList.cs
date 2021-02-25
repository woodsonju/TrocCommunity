using System.Collections.Generic;

namespace TrocCommunity.Core.Models
{
    public class WishList: BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int ClientOwner { get; set; }
        public int IdBook { get; set; }
        public List<Livre> Livres { get; set; }
    }
}