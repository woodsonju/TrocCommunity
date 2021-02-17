using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;


namespace TrocCommunity.WebUi.Controllers
{
    public class CategoriesController : Controller
    {
        const int pageSize = 9;

        IRepository<Categorie> contextCategorie;
        IRepository<Livre> contextLivre; 



        public CategoriesController()
        {
            this.contextCategorie = new SQLRepository<Categorie>(new MyContext());
            this.contextLivre = new SQLRepository<Livre>(new MyContext());


        }


        public CategoriesController(IRepository<Categorie> contextCategorie, IRepository<Livre> contextLivre)
        {
            this.contextCategorie = contextCategorie;
            this.contextLivre = contextLivre;

        }

        // GET: Categories
        public ActionResult Catalogue(int page = 1,  string search = null)
        {

            CategorieLivre viewModel = new CategorieLivre();
            IEnumerable<Livre> Livres = contextLivre.Collection().Where(p => search == null
            || p.Author.Contains(search));
            

            viewModel.Categories = contextCategorie.Collection().ToList();
            
            var list = contextLivre.Collection().ToList();
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)list.Count() / pageSize);
            Livres = Livres.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
            ViewBag.NoSearch = 0;

            if (contextLivre.Collection().Where(p => p.Author.Contains(search)).Count() > 0)
            {
                var NbLivreRecherche = contextLivre.Collection().Where(p =>p.Author.Contains(search)).Count();
                ViewBag.NbLivreSearch = NbLivreRecherche;
            }

            viewModel.Livres = Livres;


            return View(viewModel) ;
        }



        // Vue Littérature
        public ActionResult Lit(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Lit").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Bien être, santé et vie pratique
        public ActionResult Bie(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Bie").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Jeunesse
        public ActionResult Jeu(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Jeu").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue  Bande dessinées et Mangas
        public ActionResult Ban(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Ban").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Art et Sciences humaines
        public ActionResult Art(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Art").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Scolaire et Pédagogie
        public ActionResult Sco(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Sco").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Loisirs créatifs, nature et voyages
        public ActionResult Loi(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Loi").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Sciences, Techniques et Médecine
        public ActionResult Sci(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Sci").ToList();
            viewModel.Livres = vps;
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        //Vue Entreprise, Droit, Economie
        public ActionResult Ent(int page = 1, bool isAsc = true, string search = null)
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Ent").ToList();
            viewModel.Livres = vps;
            viewModel.Livres = vps.Where(x => search == null || x.Author.Contains(search));
            ViewBag.TotalPages = (int)Math.Ceiling((double)vps.Count() / pageSize);
            viewModel.Livres = vps.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();

            
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;

            ViewBag.IsAsc = isAsc;


            return View(viewModel);
        }

        // Détails d'un livre
        public ActionResult DetailsLivre(int id)
        {
            Livre livre = contextLivre.FindById(id);
            if (livre == null)
            {
                return HttpNotFound();
            }
            return View(livre);
        }



    }
}