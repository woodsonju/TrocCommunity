using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Livre> contextLivre;



        public HomeController()
        {
            this.contextLivre = new SQLRepositoryLivre(new MyContext());


        }


        public HomeController(IRepository<Livre> contextLivre)
        {
            this.contextLivre = contextLivre;

        }
        public ActionResult Index()
        {
            IEnumerable<Livre> books = ((SQLRepositoryLivre)contextLivre).TroisDerniersLivresAjoutes();
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Merci de remplir ce formulaire";

            return View();
        }

        public ActionResult Unauthorized()
        {
            return View("Error403");
        }
    }
}