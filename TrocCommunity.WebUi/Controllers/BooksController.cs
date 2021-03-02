using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.ViewModels;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.ApiClient;
using TrocCommunity.WebUi.ExceptionTC;
using TrocCommunity.WebUi.Interceptors;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Controllers
{
    [LoginFilter]
    public class BooksController : Controller
    {
        private GoogleBookApiFunctions api = new GoogleBookApiFunctions();
        private IBookService service;
        IRepository<Categorie> contextCategory;
        IRepository<Utilisateur> contextUser;

        IRepository<Livre> contextBook;

        public BooksController()
        {
            this.service = new BookService();
            contextCategory = new SQLRepositoryCategorie(new MyContext());
            contextUser = new SQLRepository<Utilisateur>(new MyContext());
            contextBook = new SQLRepositoryLivre(new MyContext());
        }


        // GET: Books
        public ActionResult AddBook()
        {
            
            LivreCategorieViewModel viewModel = new LivreCategorieViewModel();
            viewModel.Livre = new Livre();

            viewModel.Categories = contextCategory.Collection().ToList();

            ViewBag.FirstStep = true;

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(Livre livre)
        {
            if (ModelState.IsValid)
            {
                //Récupération des autres informations du livre vers l'api
                /*                string isbnString = livre.ISBN.ToString();
                                string author = await service.GetAuthors(isbnString);*/

                livre.ClientId = (int)Session["Id"];


                Client utilisateur = (Client)contextUser.FindById((int)Session["Id"]);

                utilisateur.SoldeCompte += livre.AvancePoints;

                contextUser.Update(utilisateur);
                contextUser.SaveChanges();


                Categorie categorie = ((SQLRepositoryCategorie)contextCategory).FindByName(livre.Categorie.NomCategorie);

                livre.Categorie = null;
                livre.CatgorieId = categorie.Id;
                
                livre.Price = service.GetPoints(livre.Price, livre.EtatDuLivre, livre.IsExchange);
                livre.Disponible = true;
                contextBook.Insert(livre);
                contextBook.SaveChanges();


                return RedirectToAction("Catalogue", "Categories"); ;

            }
                return View();
            


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> AddPartialBook(Livre livre)
        {
            if (ModelState.IsValid)
            {
                string isbnString = livre.ISBN.ToString();

                string title = "",auteur = "",editionDate ="", image = "", description = "";
                double price = 0.0, nbrePoints = 0.0, avancePoints = 0.0;
                try
                {
                    title = await service.GetTitle(isbnString);
                    price = await service.GetPrice(isbnString);
                    nbrePoints = service.GetPoints(price, EtatDuLivre.COMMENEUF);
                    avancePoints = service.GetPoints(price, EtatDuLivre.COMMENEUF, livre.IsExchange);
                    editionDate = await service.GetDateEdition(isbnString);
                    image = await service.GetImage(isbnString);
                    description = await service.GetDescription(isbnString);
                    auteur = await service.GetAuthors(isbnString);
                }
                catch (BookNotFoundException bookException)
                {
                    ViewBag.ExceptionBook = bookException.Message;
                    return PartialView();
                }
                livre.Author = auteur;
                livre.ISBN = Convert.ToInt64(isbnString);
                livre.Title = title;
                livre.Image = image;
                livre.Description = description;
                livre.Price = price;
                livre.PointDuLivre = nbrePoints;
                livre.AvancePoints = avancePoints;
                livre.DateEdition = editionDate;
                /*                livre.DateEdition = DateTime.Parse(editionDate).Year;
                */

                LivreCategorieViewModel viewModel = new LivreCategorieViewModel();
                viewModel.Livre = livre;

                viewModel.Categories = contextCategory.Collection().ToList();


                return PartialView("_BookInfos", viewModel);

            }
            else
            {
                return PartialView();
            }

            /*            string author = service.GetEdithor(isbnString);
                        string editionDate = service.GetDateEdition(isbnString);
                        string langauge = service.GetLanguage(isbnString);
                        int volume = service.GetVolume(isbnString);
                        List<string> categories = service.GetCategories(isbnString);
                        string dimension = service.GetDimension(isbnString);
                        string description = service.GetDescription(isbnString);
                        double criticalBook = service.GetAverageRatingCritical(isbnString);
                        */


            //Calcul du nombre de points



        }


    }
}