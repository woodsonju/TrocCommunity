using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<Utilisateur> contextUser;

        public AccountController()
        {
            contextUser = new SQLRepository<Utilisateur>(new MyContext());
        }

        public AccountController(IRepository<Utilisateur> contextUser)
        {
            this.contextUser = contextUser;
        }


        // GET: Account
        [AllowAnonymous]
        public ActionResult Register()
        {
            /* FormRegister user = new FormRegister();*/
            contextUser.Collection().ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormRegister formRegister)
        {

            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = new Client(formRegister.UserName, formRegister.Email, formRegister.Password, formRegister.Confirmpwd, formRegister.DateNaissance);

                contextUser.Insert(utilisateur);
                contextUser.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}