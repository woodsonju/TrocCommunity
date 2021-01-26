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
    public class UtilisateursController : Controller
    {

        IRepository<Utilisateur> contextUser;
        IRepository<Utilisateur> contextMockUser;



        public UtilisateursController()
        {
            this.contextUser = new SQLRepository<Utilisateur>(new MyContext());
        }


        public UtilisateursController(IRepository<Utilisateur> contextUser)
        {
            this.contextUser = contextUser;
        }


        // GET: Utilisateurs
        public ActionResult Index()
        {
            List<Utilisateur> users = contextUser.Collection().ToList();
            return View(users);
        }
    }
}