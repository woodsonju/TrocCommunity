using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.Core.ViewModels
{
    public class WishListViewModel
    {
        public IQueryable<WishList> wishList { get; set; }
        public Utilisateur user { get; set; }
        public Livre book { get; set; }
    }
}
