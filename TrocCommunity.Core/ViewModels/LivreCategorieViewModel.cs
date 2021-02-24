using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.Core.ViewModels
{
    public class LivreCategorieViewModel
    {
        public Livre Livre { get; set; }

        public IEnumerable<Categorie> Categories { get; set; }
    }
}
