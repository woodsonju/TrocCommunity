using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.Core.ViewModels;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.Interceptors;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<Utilisateur> contextUser;
        private IRepository<Adresse> contextAdresse;
        private IRepository<WishList> contextWishList;

        private IUtilisateurService serviceUser;
        public AccountController()
        {
            contextUser = new SQLRepositoryUtilisateur(new MyContext());
            contextAdresse = new SQLRepository<Adresse>(new MyContext());
            contextWishList = new SQLRepositoryWishList(new MyContext());

            serviceUser = new UtilisateurService(contextUser);
        }

        public AccountController(IRepository<Utilisateur> contextUser)
        {
            this.contextUser = contextUser;
            serviceUser = new UtilisateurService(contextUser);
        }

        public AccountController(IRepository<Utilisateur> contextUser, IRepository<Adresse> contextAdresse, IRepository<WishList> contextWishList)
        {
            this.contextUser = contextUser;
            this.contextAdresse = contextAdresse;
            this.contextWishList = contextWishList;
            serviceUser = new UtilisateurService(contextUser);
        }

        public ActionResult Index()
        {
            TempData.Keep();

            return View();
        }


        // GET: Account
        [AllowAnonymous]
        public ActionResult Register()
        {
            //Retourne la vue pour créer un utilisateur
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

                //Lors de la création d'un utilisateur je crée un adresse par défaut
                /*            utilisateur.Adresse = new Adresse {
                              TypeDeVoie="", NomDeVoie="", NumVoie=0, CodePostale="00000", Ville="", Pays="" };*/

                serviceUser.Insert(utilisateur); //Ajoute dans la liste d'utilisateur en mémoire (la liste locale)
                serviceUser.SaveChanges();  //Ensuite Actualise dans la base de données (la liste distante: remote)

                ViewData["UserName"] = utilisateur.UserName;
                
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
                Utilisateur utilisateur = serviceUser.CheckLogin(Email,Password) ;


                if (utilisateur == null)
                {
                    ModelState.Clear();
                    ViewBag.ErrorLog = "L'adresse renseignée n'existe pas ou le mdp est faux";
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
                        Session["Photo"] = utilisateur.Photo;
                        Session["Email"] = utilisateur.Email;
                        Session["idCurrentClient"] = utilisateur.Id;
                        int CurrentIdClient = (int)Session["idCurrentClient"];
                        Session["count"] = (((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient)).Count();
                    
                   
                    Session["Id"] = utilisateur.Id;

                    /**
                        * Partie Woodson : Compte Client
                        */
                    //Username
                    TempData["UserName"] = utilisateur.UserName;
     
                    //ID
                    TempData["ID"] = utilisateur.Id;

                    //img : photo
                    if (utilisateur.Photo == null)
                    {
                        TempData["Photo"] = "~/Content/TEMPLATE/images/AccountImages/imgProfile.png";
                    }
                    else
                    {
                        TempData["Photo"] = Session["Photo"];
                    }

                    // TempData["Photo"] = utilisateur.Photo;

                    // TempData["Photo"] = utilisateur.Photo;
                    // TempData["AdresseId"] = utilisateur.Adresse.Id;

                    /*          int id_adresse = utilisateur.Adresse.Id;*//*
                                //TempData["ID_ADRESSE"] = id_adresse;*/

                    TempData.Keep();

                    return RedirectToAction("Index", "Home");
                    

                }

                return View();
            }
            else
            {
                ViewBag.ErrorLog = "Le Pseudonyme ou le mot de passe ne sont pas renseignés";
                return View();
            }


        }

        [LoginFilter]
        public ActionResult LogOut()
        {
            Session["Connexion"] = null;
            Session["TypeUtilisateur"] = null;
            Session["Photo"] = null;
            Session["Email"] = null;
            Session["Id"] = null;

            return RedirectToAction("Index", "Home");
        }



        public ActionResult ConfirmNewPassWord(string mail)
        {
            string decrypt = CryptingData.Unprotect( mail );
            Utilisateur u = serviceUser.FindByEmail(decrypt);
            
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
        public ActionResult ConfirmNewPassWord(FormRegister u )
        {

            Utilisateur ut = serviceUser.FindByEmail(u.Email);

            //ut.Password = u.Password;
            ut.Confirmpwd = u.Confirmpwd;

            if (ModelState.IsValid)
            {
                if (u.Password.Equals(u.Confirmpwd))
                {
                    serviceUser.Update(ut);
                    serviceUser.SaveChanges();
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

                if ( serviceUser.FindByEmail(Email) != null)
                {

                    // Encryptind Data

                    string encrypt = CryptingData.Protect(Email);

                    

                    string actionURL = Url.Action("ConfirmNewPassWord", "Account", null, Request.Url.Scheme);
                    MailMessage msg = new MailMessage("no.reply.TrocCommunity@gmail.com", Email, "TrocCommunity Restart PassWord",
                                                                                                    "<h1>Changement de mot de passe</h1>" + "\n" + 
                                                                                                    "<p> Veuillezcliquer sur le lien ci-joint afin de changer de mot de passe<p>"+ "\n"+ 
                                                                                                    "<a href=\""+actionURL+"?mail="+ encrypt + "\">Lien remplacement mot de passe<a>" );
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

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = serviceUser.FindById((int)id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Utilisateur utilisateur = serviceUser.FindById((int)id);
            /* int adresseId = utilisateur.AdresseId;*/
            string mail = utilisateur.Email;
            Utilisateur uAdresse = serviceUser.FindByEmail(mail);
            if (uAdresse == null)
            {
                return HttpNotFound();
            }

            
            return View(utilisateur);
        }

        // POST: Produits/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client, int id, HttpPostedFileBase image)
        {
        

            if (!ModelState.IsValid)
            {
                return View(client);
            } else
            {
                if(image!=null)
                {
                    client.Photo = client.Id + Path.GetExtension(image.FileName);
                    //Sauvegarde la photo dans le dossier AccountImages
                    image.SaveAs(Server.MapPath("~/Content/TEMPLATE/images/AccountImages/") + client.Photo);
                    //Actualise la session utilisateur avec sa nouvelle photo 
                    Session["Photo"] = client.Photo;
                } else
                {              
                    client.Photo = (string)Session["Photo"];                  
                }

                TempData["Photo"] = Session["Photo"];
                TempData["UserName"] = client.UserName;
                Session["Connexion"] = client.UserName;
                Session["Id"] = client.Id;
                TempData.Keep();
                serviceUser.Update(client);
                serviceUser.SaveChanges();
                //img : photo
              

                return RedirectToAction("Index");
            }

        }

    }
}