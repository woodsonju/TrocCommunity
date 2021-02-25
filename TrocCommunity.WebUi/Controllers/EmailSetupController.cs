using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Logic;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.DataAccess.SQL;

namespace TrocCommunity.WebUi.Controllers
{
    public class EmailSetupController : Controller
    {

        IRepository<Categorie> contextCategorie;
        IRepository<Livre> contextLivre;
        IRepository<WishList> contextWishList;




        public EmailSetupController()
        {
            this.contextCategorie = new SQLRepository<Categorie>(new MyContext());
            this.contextLivre = new SQLRepositoryLivre(new MyContext());
            this.contextWishList = new SQLRepositoryWishList(new MyContext());


        }


        public EmailSetupController(IRepository<Categorie> contextCategorie, IRepository<Livre> contextLivre, IRepository<WishList> contextWishList)
        {
            this.contextCategorie = contextCategorie;
            this.contextLivre = contextLivre;
            this.contextWishList = contextWishList;

        }
        // GET: EmailSetup
        public ActionResult Index(int id)
        {
            Livre livre = ((SQLRepositoryLivre)contextLivre).FindMailByBook(id);
            gmail gmail = new gmail() { To = livre.Client.Email };
            return View(gmail);
        }


        [HttpPost]
        public ActionResult Index(gmail model)
        {
            MailMessage mm = new MailMessage("no.reply.TrocCommunity@gmail.com", model.To);
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("no.reply.TrocCommunity@gmail.com", "mvpnlwj@testTrocMailBox");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Mail has been sent successfully";
            return RedirectToAction("Index", "Home");
        }

    }
}