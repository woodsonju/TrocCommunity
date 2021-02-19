﻿using System;
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
            this.contextLivre = new SQLRepositoryLivre(new MyContext());


        }


        public CategoriesController(IRepository<Categorie> contextCategorie, IRepository<Livre> contextLivre)
        {
            this.contextCategorie = contextCategorie;
            this.contextLivre = contextLivre;

        }

        // GET: Categories
        public ActionResult Catalogue(int page = 1,  string search = null, string cat = null)
        {

            CategorieLivre viewModel = new CategorieLivre();


            IEnumerable<Livre>list = new List<Livre>();
            if(cat == null)
            {
                list = contextLivre.Collection();

            }
            else
            {
                list = ((SQLRepositoryLivre)contextLivre).LivreParCategorie(cat);
            }


            //IEnumerable<Livre> LivresSearch = ((SQLRepositoryLivre)contextLivre).Search(search);
            viewModel.Categories = contextCategorie.Collection().ToList();
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)list.Count() / pageSize);
            //var list = contextLivre.Collection().ToList();
            var Livres = NbPagination(search, page, pageSize);
            viewModel.Livres = Livres;
            //LivresSearch = ((SQLRepositoryLivre)contextLivre).NbPagination(search, page, pageSize);
            ViewBag.currentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
            ViewBag.NoSearch = 0;

            if (((SQLRepositoryLivre)contextLivre).Search(search).Count() > 0)
            {
                var NbLivreRecherche = ((SQLRepositoryLivre)contextLivre).Search(search).Count();
                ViewBag.NbLivreSearch = NbLivreRecherche;
            }

            viewModel.Livres = Livres;


            return View(viewModel) ;
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