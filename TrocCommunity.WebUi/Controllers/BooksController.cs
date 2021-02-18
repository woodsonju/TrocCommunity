using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrocCommunity.WebUi.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult AddBook()
        {
            return View();
        }


    }
}