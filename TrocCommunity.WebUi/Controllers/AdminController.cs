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
using DataTables;
using System.Net;

namespace TrocCommunity.WebUi.Controllers
{
    public class AdminController : Controller
    {

        IRepository<Utilisateur> contextUser;
        IRepository<Admin> contextAdmin;
        IRepository<UpdateDatabaseAdmin> contextDatabaseAdmin;

        public AdminController()
        {
            this.contextUser = new SQLRepository<Utilisateur>(new MyContext());
            this.contextAdmin = new SQLRepository<Admin>(new MyContext());
            this.contextDatabaseAdmin = new SQLRepository<UpdateDatabaseAdmin>(new MyContext());
        }


        public AdminController(IRepository<Utilisateur> contextUser, IRepository<Admin> contextAdmin, IRepository<UpdateDatabaseAdmin> contextDatabaseAdmin)
        {
            this.contextUser = contextUser;
            this.contextAdmin = contextAdmin;
            this.contextDatabaseAdmin = contextDatabaseAdmin;
        }

        public AdminController(IRepository<Utilisateur> contextUser )
        {
            this.contextUser = contextUser;
        }


        // GET: Utilisateurs
        public ActionResult Index()
        {
            List<Utilisateur> users = contextUser.Collection().ToList();
            return View(users);
        }

        public ActionResult Edit(int id)
        {
            Utilisateur user = contextUser.FindById(id);
            UpdateDatabaseAdmin updateDB = new UpdateDatabaseAdmin();
            updateDB.typeUtilisateur = user.TypeUtilisateur;
            if (updateDB == null)
            {
                return HttpNotFound();
            }
            return View(updateDB);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateDatabaseAdmin updateDB, int id)
        {
            if (ModelState.IsValid)
            {
                Utilisateur user = contextUser.FindById(id);
                user.TypeUtilisateur = updateDB.typeUtilisateur ;
                contextUser.Update(user);
                contextUser.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updateDB);

        }

        public ActionResult Details (int id)
        {
            Utilisateur utilisateur = contextUser.FindById(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }


        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            Utilisateur utilisateur = contextUser.FindById(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   
            contextUser.Delete(id);
            contextUser.SaveChanges();
            return RedirectToAction("Index");
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