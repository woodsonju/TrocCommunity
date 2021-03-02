using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.Interceptors;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Controllers
{
    [LoginFilter]
    public class EchangeLivreController : Controller
    {
        // GET: EchangeLivre
        IUtilisateurService serviceUser;
        IEchangeLivre serviceEchange;

        public EchangeLivreController()
        {
            SQLRepositoryLivreEchange contextEchange = new SQLRepositoryLivreEchange(new MyContext());

            SQLRepositoryUtilisateur  contextUser = new SQLRepositoryUtilisateur(new MyContext());

            serviceUser = new UtilisateurService(contextUser);

            serviceEchange = new EchangeLivreService(contextEchange);
        }

        public ActionResult ProposeEchange(int livreId)
        {
            int id = (int)Session["Id"];

            EchangeLivre ech = new EchangeLivre() { ClientPropId = id, LivreEchangeId = livreId, EtatEchange = EtatEchange.Propose, DateEchangeCreation = DateTime.Now, DateEchangeEffectue = DateTime.Now};
            serviceEchange.EchangeBookProposition(ech);

            return RedirectToAction("DetailsLivre", "Categories",new { id = livreId });
        }
        

        public ActionResult AccepterEchange(int echId)
        {
            serviceEchange.EchangeBookAccept(echId);

            return RedirectToAction("ShowEchange", "EchangeLivre", new { etat = EtatEchange.Propose });
        }

        public ActionResult CancelEchangeWEch(int echId)
        {

            serviceEchange.CancelOperation(echId);

            return RedirectToAction("ShowEchange", "EchangeLivre", new { etat = EtatEchange.Propose });
        }

        public ActionResult CancelEchange(int livreId)
        {
            EchangeLivre ech = serviceEchange.enCoursEchange(livreId, (int)Session["Id"]);

            if (ech != null) serviceEchange.CancelOperation(ech.Id);

            return RedirectToAction("DetailsLivre", "Categories", new { id = livreId });
        }

        public ActionResult ShowEchange(EtatEchange etat)
        {
            int id =(int)Session["Id"];
            ViewBag.Historique = Convert.ToBoolean(etat);
            List<EchangeLivre> listRes = serviceEchange.ShowAll(etat,id);
            return View("Index", listRes);
        }



    }
}