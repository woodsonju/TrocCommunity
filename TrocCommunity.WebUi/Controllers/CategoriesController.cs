using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.Core.ViewModels;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.Interceptors;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Controllers
{
    public class CategoriesController : Controller
    {
        const int pageSize = 9;

        private IRepository<Utilisateur> contextUser;
        IRepository<Categorie> contextCategorie;
        IRepository<Livre> contextLivre;
        IRepository<WishList> contextWishList;
        private BookService serviceBook;



        public CategoriesController()
        {
            contextUser = new SQLRepositoryUtilisateur(new MyContext());
            this.contextCategorie = new SQLRepository<Categorie>(new MyContext());
            this.contextLivre = new SQLRepositoryLivre(new MyContext());
            this.contextWishList = new SQLRepositoryWishList(new MyContext());
            serviceBook = new BookService(contextLivre);

        }

        public CategoriesController(IRepository<Utilisateur> contextUser, IRepository<Categorie> contextCategorie, IRepository<Livre> contextLivre, IRepository<WishList> contextWishList)
        {
            this.contextUser = contextUser;
            this.contextCategorie = contextCategorie;
            this.contextLivre = contextLivre;
            this.contextWishList = contextWishList;
            serviceBook = new BookService(contextLivre);

        }

        // GET: Categories
        public ActionResult Catalogue(int page = 1, string cat = null, string typeSearch = "Catalogue")
        {

            CategorieLivre viewModel = new CategorieLivre();


            IEnumerable<Livre>list = new List<Livre>();


            //IEnumerable<Livre> LivresSearch = ((SQLRepositoryLivre)contextLivre).Search(search);
            viewModel.Categories = contextCategorie.Collection().ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)serviceBook.Count(cat) / pageSize);
            //var list = contextLivre.Collection().ToList();

            var Livres = serviceBook.NbPagination(page, pageSize,cat);
            viewModel.Livres = Livres;
            //LivresSearch = ((SQLRepositoryLivre)contextLivre).NbPagination(search, page, pageSize);
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = null;
            ViewBag.typeSearch = typeSearch;

            viewModel.Livres = Livres;


            return View(viewModel) ;
        }

        public ActionResult Search(int page = 1,string search = null, string typeSearch = "Search")
        {
            CategorieLivre viewModel = new CategorieLivre();
            viewModel.Categories = contextCategorie.Collection().ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)serviceBook.SearchCount(search) / pageSize);

            //var list = contextLivre.Collection().ToList();

            var Livres = serviceBook.NbPaginationSearch(page, pageSize,search);
            viewModel.Livres = Livres;
            //LivresSearch = ((SQLRepositoryLivre)contextLivre).NbPagination(search, page, pageSize);
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.typeSearch = typeSearch;
            ViewBag.NbLivreSearch = Livres.Count();

            return View("Catalogue",viewModel);
        }





        public ActionResult AdvancedSearch(int amount1,int amount2,string Auteur,string Titre,bool cn,bool be, bool em, bool u, bool? ville, bool? rayon,int? range,int page = 1,string search = null, string typeSearch = "AdvancedSearch")
        {

            // Test Calcul Distance entre Paris 18 et la proche banlieue : resultat environ 4 kilomètre.
            double lon1 = 2.3370638;
            double lat1 = 48.8842813;
            double lon2 = 2.3402471;
            double lat2 = 48.92362;
            double dist = DistanceOrth.DistanceOrthodromique(lon2, lat2, lon1, lat1);
            if (Auteur == null) Auteur = "";
            if (Titre == null) Titre= "";


            CategorieLivre viewModel = new CategorieLivre();
            viewModel.Categories = contextCategorie.Collection().ToList();

            if (ModelState.IsValid)
            {
                
                List<int> AcceptableState = new List<int>();
                if (cn) AcceptableState.Add((int)EtatDuLivre.COMMENEUF);
                if (be) AcceptableState.Add((int)EtatDuLivre.BONETAT);
                if (em) AcceptableState.Add((int)EtatDuLivre.ETATMOYEN);
                if (u) AcceptableState.Add((int)EtatDuLivre.USE);
                IEnumerable<Livre> livre = new List<Livre>();
                

                if (Session["Connexion"] != null )
                {
                    Adresse utAdresse = ((SQLRepositoryUtilisateur)contextUser).findByEmail((string)Session["Email"]).Adresse;

                    if ( utAdresse.FullName != "")
                    {

                    
                        
                        // Vérification pour savoir si l'adresse est bonne

                        //ViewBag.ShowGeo = false;

                        ViewBag.TotalPages = (int)Math.Ceiling((decimal)((SQLRepositoryLivre)contextLivre).CountAdvancedSearchWithGeo(amount1, amount2, Auteur, Titre, AcceptableState, ville, rayon, range, utAdresse) / pageSize);

                        livre = ((SQLRepositoryLivre)contextLivre).NbPaginationAdvancedSearchWithGeo(amount1, amount2, Auteur, Titre, AcceptableState, ville, rayon, range, utAdresse, page, pageSize);

                        //livre = ((SQLRepositoryLivre)contextLivre).AdvancedSearchWithGeo(amount1, amount2, Auteur, Titre, AcceptableState, ville, rayon, range, utAdresse);
                        ViewBag.ShowGeo = "";
                    }
                    else
                    {
                        ViewBag.TotalPages = (int)Math.Ceiling((decimal)((SQLRepositoryLivre)contextLivre).CountAdvancedSearch(amount1, amount2, Auteur, Titre, AcceptableState) / pageSize);

                        livre = ((SQLRepositoryLivre)contextLivre).NbPaginationAdvancedSearch(amount1, amount2, Auteur, Titre, AcceptableState, page, pageSize);

                        ViewBag.ShowGeo = "Indiquer votre adresse dans les paramètres de votre compte afin d'utiliser la localisation dans votre recherche";
                    }

                }

                else
                {
                    ViewBag.TotalPages = (int)Math.Ceiling((decimal)((SQLRepositoryLivre)contextLivre).CountAdvancedSearch(amount1, amount2, Auteur, Titre, AcceptableState) / pageSize);

                    livre = ((SQLRepositoryLivre)contextLivre).NbPaginationAdvancedSearch(amount1, amount2, Auteur, Titre, AcceptableState, page, pageSize);

                    ViewBag.ShowGeo = "Connecter vous pour utiliser les fonctionnalité de recherche lié à votre position";
                }




                viewModel.Livres = livre.ToList();

                ViewBag.currentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.Search = search;
                ViewBag.typeSearch = typeSearch;

                ViewBag.NbLivreSearch = livre.Count();

                ViewBag.UseLastSearch = true;

                ViewBag.amount1 = amount1; ViewBag.amount2 = amount2; ViewBag.Auteur = Auteur; ViewBag.Titre = Titre; ViewBag.cn = cn; ViewBag.be = be; ViewBag.em = em; ViewBag.u = u; ViewBag.ville = ville; ViewBag.rayon = rayon; ViewBag.range = range;

                return View("Catalogue", viewModel);
            }

            // en cas de mauvais modèle

            return RedirectToAction("Catalogue","Categories");
        }

        // Détails d'un livre
        [LoginFilter]
        public ActionResult DetailsLivre(int id)
        {
            //Livre livre = contextLivre.FindById(id);
            WishListViewModel viewModelWL = new WishListViewModel();
            viewModelWL.book = contextLivre.FindById(id);
            Session["idBook"] = viewModelWL.book.Id;
            Session["author"] = viewModelWL.book.Author;
            Session["title"] = viewModelWL.book.Title;
            Session["image"] = viewModelWL.book.Image;
            var estPresent = true;
            Session["estPresent"] = estPresent;
            
            
            int CurrentIdClient = (int)Session["idCurrentClient"];
            viewModelWL.wishList = ((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient);

            if (viewModelWL == null)
            {
                return HttpNotFound();
            }
            return View(viewModelWL);
        }

        



    }
}