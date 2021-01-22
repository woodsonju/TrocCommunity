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

    

    public class LoginController : Controller
    {

        IRepository<Utilisateur> context;

        public LoginController()
        {
            context = new SQLRepositoryUtilisateur(new MyContext());
        }

        // GET: Login
        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index( string Email, string Password)
        {

            

            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = ((SQLRepositoryUtilisateur)context).findByEmail(Email);

                if (utilisateur == null)
                {
                    ModelState.Clear();
                    ViewBag.ErrorLog = "L'adresse renseignée n'existe pas";
                }
                else
                {
                    if (utilisateur.Password != Password)
                    {
                  
                        ViewBag.ErrorLog = "La Combinaison email,mot de passe n'est pas la bonne";
                    }
                    else
                    {
                        // Connexion Réussite
                        Session["Connexion"] = utilisateur.Pseudo;
                        Session["TypeUtilisateur"] = utilisateur.typeUtilisateur;

                        return RedirectToAction("Index", "Home");
                    }

                }


                return View();
            }
            else
            {
                ViewBag.ErrorLog = "Le Pseudonyme ou le mot de passe ne sont pas renseignés";
                return View();
            }

            
        }

        public ActionResult LogOut()
        {
            Session["Connexion"] = null;
            Session["TypeUtilisateur"] = null;

            return RedirectToAction("Index","Home");
        }


    }
}