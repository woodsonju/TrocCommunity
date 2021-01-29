using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            contextUser = new SQLRepositoryUtilisateur(new MyContext());
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


        public ActionResult Login()
        {


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {



            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = ((SQLRepositoryUtilisateur)contextUser).findByEmail(Email);

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
                        Session["Connexion"] = utilisateur.UserName;
                        Session["TypeUtilisateur"] = utilisateur.TypeUtilisateur;

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

            return RedirectToAction("Index", "Home");
        }



        public ActionResult ConfirmNewPassWord(string Email)
        {
            Utilisateur u = ((SQLRepositoryUtilisateur)contextUser).findByEmail(Email);
            
            if (u != null)
            {
                return View(u);
            }
            else
            {
      
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmNewPassWord(Utilisateur u )
        {
            if (ModelState.IsValid)
            {
                if (u.Password.Equals(u.Confirmpwd))
                {
                    contextUser.Update(u);
                    contextUser.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.ErrorLog = "Les deux mot de passe ne correspondent pas";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorLog = "Le mot de passe n'est pas conforme";
                ModelState.Clear();
                return View();
            }
            
        }

        public ActionResult RestartPassWord()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestartPassWord(string Email)
        {
            // Envoyer le Mail
            if (ModelState.IsValid)
            {

                if (((SQLRepositoryUtilisateur)contextUser).findByEmail(Email) != null)
                {
                    string actionURL = Url.Action("ConfirmNewPassWord", "Account", null, Request.Url.Scheme);
                    MailMessage msg = new MailMessage("no.reply.TrocCommunity@gmail.com", Email, "TrocCommunity Restart PassWord",
                                                                                                    "<h1>Changement de mot de passe</h1>" + "\n" + 
                                                                                                    "<p> Veuillezcliquer sur le lien ci-joint afin de changer de mot de passe<p>"+ "\n"+ 
                                                                                                    "<a href=\""+actionURL+"?Email="+Email+"\">Lien remplacement mot de passe<a>" );
                    msg.IsBodyHtml = true;
                    //msg.CC.Add("pereiramarc@hotmail.fr");
                    // Provider GMAIL

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential("no.reply.TrocCommunity@gmail.com", "mvpnlwj@testTrocMailBox");
                    client.EnableSsl = true;
                    client.Send(msg);
                    ViewBag.Message = "Votre message a bien été envoyé";
                    ModelState.Clear();
                }

                
            }

            return View("");
        }


    }
}