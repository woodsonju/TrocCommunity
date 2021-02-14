using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Controllers
{
    public class CategoriesController : Controller
    {

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
        public ActionResult Catalogue()
        {
            CategorieLivre viewModel = new CategorieLivre();
            viewModel.Livres = contextLivre.Collection().ToList();
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        // Vue Littérature
        public ActionResult Lit()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Lit").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Bien être, santé et vie pratique
        public ActionResult Bie()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "bie").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Jeunesse
        public ActionResult Jeu()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "jeu").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue  Bande dessinées et Mangas
        public ActionResult Ban()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "ban").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Art et Sciences humaines
        public ActionResult Art()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "art").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Scolaire et Pédagogie
        public ActionResult Sco()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Sco").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Loisirs créatifs, nature et voyages
        public ActionResult Loi()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Loi").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Sciences, Techniques et Médecine
        public ActionResult Sci()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Sci").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }

        //Vue Entreprise, Droit, Economie
        public ActionResult Ent()
        {
            CategorieLivre viewModel = new CategorieLivre();
            var vps = contextLivre.Collection().Where(a => a.Categorie.NomCategorie.Substring(0, 3) == "Ent").ToList();
            viewModel.Livres = vps;
            viewModel.Categories = contextCategorie.Collection().ToList();
            return View(viewModel);
        }


    }
}