using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Service
{
    public interface ILivre
    {
        int Count(string cat);

        int SearchCount(string search);


        IEnumerable<Livre> Search(string search);


        List<Livre> NbPaginationSearch(int page, int pageSize, string search);


        List<Livre> NbPagination(int page, int pageSize, string cat);


        IEnumerable<Livre> LivreParCategorie(string cat);


        IEnumerable<Livre> TroisDerniersLivresAjoutes();

        Livre FindMailByBook(int id);

    }
}
