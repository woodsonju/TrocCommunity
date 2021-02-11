using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = contextUser.FindById((int)id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

    }
}