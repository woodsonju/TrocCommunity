using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

    public class WishListController : Controller
    {

        IRepository<WishList> contextWishList;



        public WishListController()
        {
            this.contextWishList = new SQLRepositoryWishList(new MyContext());


        }


        public WishListController(IRepository<WishList> contextWishList)
        {
            this.contextWishList = contextWishList;

        }

        // GET: WishList
        public ActionResult Index()
        {
            int CurrentIdClient = (int)Session["idCurrentClient"];
            IQueryable<WishList> booksWished = ((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient);
            return View(booksWished);
        }

        // GET: WishList/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WishList wishList = db.WishLists.Find(id);
        //    if (wishList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(wishList);
        //}

        // GET: WishList/Create
        //[Authorize(Roles = "Client")]
        public ActionResult Create(WishList BookAdded)
        {

            int CurrentIdClient = (int)Session["idCurrentClient"];


            if (Session["count"] == null)
            {
                BookAdded.Author = (string)Session["author"];
                BookAdded.Title = (string)Session["title"];
                BookAdded.Image = (string)Session["image"];
                BookAdded.ClientOwner = (int)Session["idCurrentClient"];
                BookAdded.IdBook = (int)Session["idBook"];
                contextWishList.Insert(BookAdded);
                contextWishList.SaveChanges();
                Session["cart"] = (((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient));
                ViewBag.cart = (((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient).Count());


                Session["count"] = 1;


            }
            else
            {
                BookAdded.Author = (string)Session["author"];
                BookAdded.Title = (string)Session["title"];
                BookAdded.Image = (string)Session["image"];
                BookAdded.ClientOwner = (int)Session["idCurrentClient"];
                BookAdded.IdBook = (int)Session["idBook"];
                contextWishList.Insert(BookAdded);
                contextWishList.SaveChanges();
                Session["cart"] = (((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient));
                ViewBag.cart = (((SQLRepositoryWishList)contextWishList).listWLbyIdClient(CurrentIdClient)).Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");
        }
        //    return View();
        //}

        //// POST: WishList/Create
        //// Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //// plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id")] WishList wishList)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        contextWishList.Insert(wishList);
        //        contextWishList.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(wishList);
        //}

        // GET: WishList/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WishList wishList = db.WishLists.Find(id);
        //    if (wishList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(wishList);
        //}

        //POST: WishList/Edit/5
        //Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        //plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id")] WishList wishList)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(wishList).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(wishList);
        //}

        // GET: WishList/Delete/5
        public ActionResult Delete(int id)
        {
            WishList wishList = contextWishList.FindById(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            contextWishList.Delete(id);
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            contextWishList.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: WishList/Delete/5
        public ActionResult DeleteWishList(int id)
        {
            WishList wishList = contextWishList.FindById(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishList/Delete/5
        [HttpPost, ActionName("DeleteWishListConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWishListConfirmed(int id)
        {

            contextWishList.Delete(id);
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            contextWishList.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
