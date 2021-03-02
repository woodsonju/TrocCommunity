using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using System.Diagnostics;
using TrocCommunity.Core.Tools;

namespace TrocCommunity.DataAccess.SQL.DAO
{

    public class SQLRepositoryLivre : SQLRepository<Livre>
    {

        
        
        public SQLRepositoryLivre(MyContext DataContext) : base(DataContext)
        {

        }

        public Livre finByIdWClient(int id)
        {
            return dataContext.Livres.Include(l => l.Client).SingleOrDefault(l => l.Id == id);
        }

        public int Count(string cat = null)
        {

            if (cat == null)
            {
                return dataContext.Livres.AsNoTracking().Where(l=>l.Disponible == true).Count();
            }
            else
            {
                return dataContext.Livres.AsNoTracking().Where(l => l.Disponible == true).Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat).Count();

            }

        }
        public int SearchCount(string search)
        {

            return dataContext.Livres.AsNoTracking().Where(l => l.Disponible == true).Where(p => search == null
            || p.Title.Contains(search)).Count();

        }

        public IEnumerable<Livre> Search(string search)
        {
            IEnumerable<Livre> book = new List<Livre>();


            book = dataContext.Livres.AsNoTracking().Where(l => l.Disponible == true).Where(p => search == null
             ||p.Title.Contains(search) || p.Author.Contains(search));

            return book;
        }

        public List<Livre> NbPaginationSearch(int page, int pageSize, string search)
        {

            return Search(search).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Livre> NbPagination(int page, int pageSize, string cat)
        {
            IEnumerable<Livre> result = new List<Livre>();

            if (cat == null)
            {
                result = dataContext.Livres.Include(bookClient => bookClient.Client).Include(bookClient => bookClient.Client.Adresse).Where(l => l.Disponible == true).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                result = LivreParCategorie(cat).Where(l => l.Disponible == true).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
            }


            return result.ToList();
        }


        public IEnumerable<Livre> LivreParCategorie(string cat)
        {
            IEnumerable<Livre> result = dataContext.Livres.Where(l => l.Disponible == true).Where(x => x.Categorie.NomCategorie.Substring(0, 3) == cat);
            return result;

        }



        public List<Livre> NbPaginationAdvancedSearchWithGeo(int min, int max, string Auteur, string Titre, List<int> AcceptableState, bool? ville, bool? rayon, int? range, Adresse adClient,int page, int pageSize)
        {
            return AdvancedSearchWithGeo(min, max, Auteur, Titre, AcceptableState, ville, rayon, range, adClient).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }
        public List<Livre> NbPaginationAdvancedSearch(int min, int max, string Auteur, string Titre, List<int> AcceptableState,int page, int pageSize)
        {
            return AdvancedSearch(min, max, Auteur, Titre, AcceptableState).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }



        public int CountAdvancedSearchWithGeo(int min, int max, string Auteur, string Titre, List<int> AcceptableState, bool? ville, bool? rayon, int? range, Adresse adClient)
        {

            return AdvancedSearchWithGeo(min,max,Auteur,Titre,AcceptableState,ville,rayon,range,adClient).Count();
            
        }

        public IEnumerable<Livre> TroisDerniersLivresAjoutes()
        {
            IEnumerable<Livre> result = dataContext.Livres.Include(bookClient => bookClient.Client).Include(bookClient => bookClient.Client.Adresse).Where(l => l.Disponible == true).OrderByDescending(x => x.Id).Take(3);
            return result;
        }

        public IEnumerable<Livre> BookByClient(int id)
        {
            IEnumerable<Livre> result = dataContext.Livres.Include(bookClient => bookClient.Client).Where(l => l.Disponible == true).Where(bookClient => bookClient.Client.Id == id);
            return result;
        }

        public Livre FindMailByBook(int id)
        {

            Livre book = dataContext.Livres.Include(bookClient => bookClient.Client).SingleOrDefault(bookClient => bookClient.Id == id);
            return book;

        }


        public int CountAdvancedSearch(int min, int max, string Auteur, string Titre, List<int> AcceptableState)
        {
            return AdvancedSearch(min, max, Auteur, Titre, AcceptableState).Count();
        }

            //AdvancedSearch
            public IEnumerable<Livre> AdvancedSearchWithGeo(int min, int max, string Auteur, string Titre, List<int> AcceptableState, bool? ville, bool? rayon, int? range, Adresse adClient)
        {

            IEnumerable<Livre> livre = AdvancedSearch(min, max, Auteur, Titre, AcceptableState);


            // Géolocalisation

            if (ville == null && rayon == null || (ville == false && rayon == false))
            {
                return livre;
            }

            else if ((bool)ville && !(bool)rayon)
            {

                livre = livre.Where(l => l.Disponible == true).Where(book => book.Client.Adresse.Ville.Equals(adClient.Ville));



            }
            else if (!(bool)ville && (bool)rayon)
            {

                livre = livre.Where(l => l.Disponible == true).Where(book => DistanceOrth.DistanceOrthodromique(adClient.Longitude,adClient.Latitude, book.Client.Adresse.Longitude, book.Client.Adresse.Latitude)<(int)range );
            }
            else if ((bool)ville && (bool)rayon)
            {
                livre = livre.Where(l => l.Disponible == true).Where(book => book.Client.Adresse.Ville.Equals(adClient.Ville) ||
                                             DistanceOrth.DistanceOrthodromique(adClient.Longitude, adClient.Latitude, book.Client.Adresse.Longitude, book.Client.Adresse.Latitude) < (int)range
                );
            }

            return livre;
        }


        public IEnumerable<Livre> AdvancedSearch(int min, int max, string Auteur, string Titre, List<int> AcceptableState)
        {

            // Price

            IEnumerable<Livre> livre = dataContext.Livres.Include(book => book.Client).Include(book => book.Client.Adresse).AsNoTracking().Where(l => l.Disponible == true).Where(book => book.PointDuLivre >= min && book.PointDuLivre <= max );

            // State

            livre = (from a1 in livre where AcceptableState.Contains((int)a1.EtatDuLivre) select a1);


            // Author and Title

            livre = livre.Where(book =>(book.Title != null && book.Title.Contains(Titre))
                                   || (book.Author != null && book.Author.Contains(Auteur)));


            return livre;
        }


    }
}
