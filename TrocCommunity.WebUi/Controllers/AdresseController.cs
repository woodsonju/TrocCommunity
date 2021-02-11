using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.ViewModels;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;

namespace TrocCommunity.WebUi.Controllers
{
    public class AdresseController : Controller
    {
        IRepository<Adresse> contextAdresse;
        private IRepository<Utilisateur> contextUser;


        public AdresseController()
        {
            this.contextAdresse = new SQLRepository<Adresse>(new MyContext());
            contextUser = new SQLRepositoryUtilisateur(new MyContext());
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

        // GET: Account
        [AllowAnonymous]
        public ActionResult Create()
        {
            //Retourne la vue pour créer un utilisateur
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adresse adresse)
        {

            if (ModelState.IsValid)
            {
                contextAdresse.Insert(adresse); //Ajoute dans la liste d'utilisateur en mémoire (la liste locale)
                contextAdresse.SaveChanges();  //Ensuite Actualise dans la base de données (la liste distante: remote)

/*                ViewData["UserName"] = utilisateur.UserName;
*/
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: Utilisateurs/Details/5
        public ActionResult DetailsAdresse(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = contextUser.FindById((int)id);
            int adresse_id = utilisateur.AdresseId;
            Utilisateur uAdresse = ((SQLRepositoryUtilisateur)contextUser).FindByAdresseId(adresse_id);
            /* Utilisateur utilisateur = ((SQLRepositoryUtilisateur)contextUser).findByEmail(Email);*/


            if (utilisateur == null)
            {
                return HttpNotFound();
            }

            UtilisateurViewModel viewModel = new UtilisateurViewModel();

            //int id_adresse = utilisateur.Adresse.Id;

            viewModel.Utilisateur = uAdresse;
            viewModel.Adresse = uAdresse.Adresse;
            /* TempData["Id"] = id_adresse;*/
            return View(viewModel);
        }


        // GET: Adresse/Edit/5
        //GET:Account/EditeAdresse/@IdUser  
        public ActionResult EditAdresse(int? id)
        {
            try
            {
                /*Utilisateur utilisateur = contextUser.FindById((int)id);*/
                Utilisateur utilisateur = ((SQLRepositoryUtilisateur)contextUser).FindByAdresseId(id);
                // Adresse adresse = contextAdresse.FindById((int)id);
                if (utilisateur.Adresse == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    UtilisateurViewModel viewModel = new UtilisateurViewModel();
                    viewModel.Adresse = utilisateur.Adresse;
                    viewModel.Utilisateur = utilisateur;

                    TempData.Keep();
                    return View(viewModel);
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

 [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdresse(Adresse adresse, int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(adresse);
                }
                else
                {
                    contextAdresse.Update(adresse);
                    contextAdresse.SaveChanges();
             /*       TempData["ID"] = id;
                    TempData.Keep();*/
                    return RedirectToAction("DetailsAdresse", "Adresse", new { id = @TempData["ID"] });
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

    }
}