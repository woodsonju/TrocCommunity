using System.Collections.Generic;

namespace TrocCommunity.Core.Models
{
    public class WishList: BaseEntity
    {
        public List<Livre> Livres { get; set; }
    }
}