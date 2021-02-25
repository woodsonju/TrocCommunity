using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.ApiClient;
using TrocCommunity.WebUi.ExceptionTC;

namespace TrocCommunity.WebUi.Service
{
    public class BookService : IBookService
    {
        private GoogleBookApiFunctions api = new GoogleBookApiFunctions();

        private IRepository<Livre> repo;


        public BookService(IRepository<Livre> repo)
        {
            this.repo = repo;
        }

        public BookService()
        {
        }


        /// <summary>
        /// Retourne l'auteur(s) du livre 
        /// </summary>
        /// <param name="books"></param>
        /// <returns>Auteur(s)</returns>
        public async Task<List<string>> GetAuthors(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);

            List<string> auteursApi = new List<string>();
            foreach (var autheur in book.SelectTokens("items[0].volumeInfo.authors[*]"))
            {
                auteursApi.Add((string)autheur);
            }

            return auteursApi;
        }


        /// <summary>
        /// Retourne le volume du livre (nombre de page)
        /// </summary>
        /// <param name="books"></param>
        /// <returns>Le nombre de page</returns>
        public async Task<int> GetVolume(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return Convert.ToInt32(book.SelectToken("items[0].volumeInfo.pageCount"));
        }


        /// <summary>
        /// Retourne le titre du livre
        /// </summary>
        /// <param name="books"></param>
        /// <returns>Titre</returns>
        public async Task<string> GetTitle(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.title")).ToString();
        }

        /// <summary>
        /// Retoune l'image du livre
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Image de couverture du livre</returns>
        public async Task<string> GetImage(string isbn)
        {
            string image = "";
            JObject book = await GetJObjectBook(isbn);

            /*string image =  book.SelectToken("items[0].volumeInfo.imageLinks.thumbnail").ToString();*/

            /*           if (image == null)
                           {*/
            if (book.SelectToken("items[0].volumeInfo.imageLinks.thumbnail") == null)
            {
                image = "~/Content/TEMPLATE/images/Livres/default-book.jpg";
            } else
            {
                image = book.SelectToken("items[0].volumeInfo.imageLinks.thumbnail").ToString();
            }
            //  }

            return image;
           // return (book.SelectToken("items[0].volumeInfo.imageLinks.thumbnail")).ToString();
        }


        /// <summary>
        /// Retourne la langue d'edition du livre
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Langue d'édition</returns>
        public async Task<string> GetLanguage(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.language")).ToString();
        }

        /// <summary>
        /// Retourne la date de publication
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>date  de publication</returns>
        public async Task<string> GetDateEdition(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.publishedDate")).ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>catigories</returns>
        public async Task<List<string>> GetCategories(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            List<string> categories = new List<string>();
            foreach (var categorie in book.SelectTokens("items[0].volumeInfo.categories[*]"))
            {
                categories.Add((string)categorie);
            }

            return categories;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Description</returns>
        public async Task<string> GetDescription(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.description")).ToString();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Editeur</returns>
        public async Task<string> GetEdithor(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.publisher")).ToString();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Note Moyenne critique pour ce volume</returns>
        public async Task<double> GetAverageRatingCritical(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return Convert.ToDouble(book.SelectToken("items[0].volumeInfo.averageRating"));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Largeur du livre</returns>
        public async Task<string> GetDimension(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            return (book.SelectToken("items[0].volumeInfo.dimensions.width")).ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>Retourne un Prix Aléatoire</returns>
        public async Task<double> GetPrice(string isbn)
        {
            JObject book = await GetJObjectBook(isbn);
            double price = Convert.ToDouble(book.SelectToken("items[0].volumeInfo.saleInfo.retailPrice"));

            if (price == 0)
            {
                //Simulateur de prix en attendant de trouver un api qui permet de toruver
                //les prix des livres qui n'ont pas été renseignés par l'api Google book
                price = RandomPrice(6, 50);
            }
            return price;
        }


        /// <summary>
        /// 1 point = 1euro 
        /// NombreDePoint = PrixNeuf * EtatLivre(%)*isExchange
        /// Obtient 25% des points totaux avant échange
        /// </summary>
        /// <param name="price"></param>
        /// <param name="etatDuLivre"></param>
        /// <param name="isExchange">isExchange=true si l'echange est effectué</param>
        /// <returns>Nombre de points</returns>
        public double GetPoints(double price, EtatDuLivre etatDuLivre, bool isExchange)
        {

            return CalculPoints(price, etatDuLivre, isExchange);
        }

        public double GetPoints(double price, EtatDuLivre etatDuLivre)
        {
            return CalculPoints(price, etatDuLivre);
        }


        public double CalculPoints(double price, EtatDuLivre etatDuLivre)
        {
            double nbrePoints = 0.0;
            switch (etatDuLivre)
            {
                case EtatDuLivre.COMMENEUF:
                    nbrePoints = price * (int)EtatDuLivre.COMMENEUF * 0.01;
                    break;
                case EtatDuLivre.BONETAT:
                    nbrePoints = price * (int)EtatDuLivre.BONETAT * 0.01;
                    break;
                case EtatDuLivre.ETATMOYEN:
                    nbrePoints = price * (int)EtatDuLivre.ETATMOYEN * 0.01;
                    break;

                case EtatDuLivre.USE:
                    nbrePoints = price * (int)EtatDuLivre.USE * 0.01;
                    break;
            }

            return Math.Round(nbrePoints * 10) / 10;
        }


        public double CalculPoints(double price, EtatDuLivre etatDuLivre, bool isExchange)
        {
            double nbrePoints = 0.0;
            switch (etatDuLivre)
            {
                case EtatDuLivre.COMMENEUF:
                    if (isExchange == false)
                        nbrePoints = price * (int)EtatDuLivre.COMMENEUF * 0.01 * 0.25;
                    else
                        nbrePoints = price * (int)EtatDuLivre.COMMENEUF * 0.01 * 0.75;
                    break;
                case EtatDuLivre.BONETAT:
                    if (isExchange == false)
                        nbrePoints = price * (int)EtatDuLivre.BONETAT * 0.01 * 0.25;
                    else
                        nbrePoints = price * (int)EtatDuLivre.BONETAT * 0.01 * 0.75 ;
                    break;

                case EtatDuLivre.ETATMOYEN:
                    if (isExchange == false)
                        nbrePoints = price * (int)EtatDuLivre.ETATMOYEN * 0.01 * 0.25;
                    else
                        nbrePoints = price * (int)EtatDuLivre.ETATMOYEN * 0.01 * 0.75;
                    break;

                case EtatDuLivre.USE:
                    if (isExchange == false)
                        nbrePoints = price * (int)EtatDuLivre.USE * 0.01 * 0.25;
                    else
                        nbrePoints = price * (int)EtatDuLivre.USE * 0.01 * 0.75;
                    break;
            }

            return Math.Round(nbrePoints*10)/10;
        }


        //throw new BookNotFoundException("Code-barres invalide (doit comporter 12 ou 13 chiffres)");
        private async Task<JObject> GetJObjectBook(string isbn)
        {
            var output = await api.GetBookByISBN(isbn);

            //Resultat en JSON
            var result = output;

            //JObject.Parse convertit une chaine json en un objet csharp
            //Parse le JSON en un objet C#
            JObject book = JObject.Parse(result);

            int totalItems =  Convert.ToInt32(book.SelectToken("totalItems"));

            if(totalItems == 0)
            {
                throw new BookNotFoundException("Code-barres invalide (doit comporter 12 ou 13 chiffres)");
            }

            return book;
        }

        public double RandomPrice(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        public int Count(string cat)
        {
            return ((SQLRepositoryLivre)repo).Count(cat);
        }

        public int SearchCount(string search)
        {
            return ((SQLRepositoryLivre)repo).SearchCount(search);
        }

        public IEnumerable<Livre> LivreParCategorie(string cat)
        {
            return ((SQLRepositoryLivre)repo).LivreParCategorie(cat);
        }

        public List<Livre> NbPagination(int page, int pageSize, string cat)
        {
            return ((SQLRepositoryLivre)repo).NbPagination(page, pageSize, cat);
        }

        public List<Livre> NbPaginationSearch(int page, int pageSize, string search)
        {
            return ((SQLRepositoryLivre)repo).NbPaginationSearch(page, pageSize, search);
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