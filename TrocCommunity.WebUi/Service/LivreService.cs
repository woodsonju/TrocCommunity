using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Service
{
    public class LivreService : ILivre
    {
        private IRepository<Livre> repo;

        public LivreService(IRepository<Livre> repo)
        {
            this.repo = repo;
        }
        public int Count(string cat)
        {
            return ((SQLRepositoryLivre)repo).Count(cat);
        }

        public int SearchCount(string search)
        {
            return ((SQLRepositoryLivre)repo).SearchCount(search);
        }

        public IQueryable<Livre> LivreParCategorie(string cat)
        {
            return ((SQLRepositoryLivre)repo).LivreParCategorie(cat);
        }

        public List<Livre> NbPagination(int page, int pageSize, string cat)
        {
            return ((SQLRepositoryLivre)repo).NbPagination(page, pageSize, cat);
        }

        public List<Livre> NbPaginationSearch(int page, int pageSize, string search)
        {
            return ((SQLRepositoryLivre)repo).NbPagination(page, pageSize, search);
        }

        public IEnumerable<Livre> Search(string search)
        {
            return ((SQLRepositoryLivre)repo).Search(search);
        }

        public IEnumerable<Livre> TroisDerniersLivresAjoutes()
        {
            return ((SQLRepositoryLivre)repo).TroisDerniersLivresAjoutes();
        }

        public Livre FindMailByBook(int id)
        {
            return ((SQLRepositoryLivre)repo).FindMailByBook(id);
        }
    }
}