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
    public class AdresseController : Controller
    {
        IRepository<Adresse> contextAdresse;


        public AdresseController()
        {
            this.contextAdresse = new SQLRepository<Adresse>(new MyContext());
        }


        public AdresseController(IRepository<Adresse> contextAdresse)
        {
            this.contextAdresse = contextAdresse;
        }



        // GET: Adresse
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Adresse adresse)
        {
            if(ModelState.IsValid)
            {
                contextAdresse.Insert(adresse);
                contextAdresse.SaveChanges();
                return RedirectToAction("Index", "Home"); 
            }
            return View();
        }
    }
}