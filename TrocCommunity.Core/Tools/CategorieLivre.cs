using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.Core.Tools
{
    public class CategorieLivre 
    {
        public IEnumerable<Livre> Livres { get; set; }


        public IEnumerable<Categorie> Categories { get; set; }
    }
}