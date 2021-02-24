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
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Controllers
{
    public class BooksController : Controller
    {
        private GoogleBookApiFunctions api = new GoogleBookApiFunctions();
        private IBookService service;
        IRepository<Categorie> contextCategory;

        public BooksController()
        {
            this.service = new BookService();
            contextCategory = new SQLRepository<Categorie>(new MyContext());

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
        public async Task<ActionResult> AddBook(Livre livre)
        {
            if (ModelState.IsValid)
            {
                livre.Client.Id = (int)Session["Id"];
                livre.Price = service.GetPoints(livre.Price, livre.EtatDuLivre, livre.IsExchange);
                return View("AddBook", livre);

            }
                return View();
            

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<PartialViewResult> AddPartialBook(Livre livre)
        {
            if (ModelState.IsValid)
            {
                string isbnString = livre.ISBN.ToString();
                string title = await service.GetTitle(isbnString);
                double price = await service.GetPrice(isbnString);
                double nbrePoints = service.GetPoints(price, EtatDuLivre.COMMENEUF);
                double avancePoints = service.GetPoints(price, EtatDuLivre.COMMENEUF, livre.IsExchange);
                string editionDate = await service.GetDateEdition(isbnString);

                string image = await service.GetImage(isbnString);



                livre.ISBN = Convert.ToInt64(isbnString);
                livre.Title = title;
                livre.Image = image;
                livre.Price = price;
                livre.PointDuLivre = nbrePoints;
                livre.AvancePoints = avancePoints;
                livre.DateEdition = Convert.ToInt32(editionDate);
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