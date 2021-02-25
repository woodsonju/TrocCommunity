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

        IRepository<Categorie> contextCategorie;
        IRepository<Livre> contextLivre;
        IRepository<WishList> contextWishList;
        private LivreService serviceBook;



        public CategoriesController()
        {
            this.contextCategorie = new SQLRepository<Categorie>(new MyContext());
            this.contextLivre = new SQLRepositoryLivre(new MyContext());
            this.contextWishList = new SQLRepositoryWishList(new MyContext());
            serviceBook = new LivreService(contextLivre);

        }


        public CategoriesController(IRepository<Categorie> contextCategorie, IRepository<Livre> contextLivre, IRepository<WishList> contextWishList)
        {
            this.contextCategorie = contextCategorie;
            this.contextLivre = contextLivre;
            this.contextWishList = contextWishList;
            serviceBook = new LivreService(contextLivre);

        }

        // GET: Categories
        public ActionResult Catalogue(int page = 1, string cat = null, string category = null)
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


            viewModel.Livres = Livres;


            return View(viewModel) ;
        }

        public ActionResult Search(int page = 1,string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            viewModel.Categories = contextCategorie.Collection().ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)((SQLRepositoryLivre)contextLivre).SearchCount(search) / pageSize);

            //var list = contextLivre.Collection().ToList();

            var Livres = serviceBook.NbPaginationSearch(page, pageSize,search);
            viewModel.Livres = Livres;
            //LivresSearch = ((SQLRepositoryLivre)contextLivre).NbPagination(search, page, pageSize);
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
         
            ViewBag.NbLivreSearch = Livres.Count();

            return View("Catalogue",viewModel);
        }


        // Vue Littérature
        /* public ActionResult Lit(int page = 1, string search = null, string categorie = null)
         {
             CategorieLivre viewModel = new CategorieLivre();
             var vps = ((SQLRepositoryLivre)contextLivre).NomCategorie(((SQLRepositoryLivre)contextLivre).NbPagination(search, page, pageSize),"Lit");
             viewModel.Livres = vps;
             ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
             viewModel.Categories = contextCategorie.Collection().ToList();
             ViewBag.currentPage = page;
             ViewBag.PageSize = pageSize;
             ViewBag.Search = search;
             return View(viewModel);
         }*/



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