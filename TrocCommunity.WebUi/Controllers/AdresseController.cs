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
using TrocCommunity.Core.Tools;
using System.Configuration;
using System.Web.Services;


namespace TrocCommunity.WebUi.Controllers
{
    public class AdresseController : Controller
    {
        private IRepository<Adresse> contextAdresse;
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
        public ActionResult DetailsAdresse(int ?id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Utilisateur utilisateur = contextUser.FindById((int)id);
            int adresse_id = utilisateur.AdresseId;
            string mail = utilisateur.Email;
            Utilisateur uAdresse = ((SQLRepositoryUtilisateur)contextUser).FindByMailWithAdressId(mail, adresse_id);
            /* Utilisateur utilisateur = ((SQLRepositoryUtilisateur)contextUser).findByEmail(Email);*/

            AdresseViewModel viewModel = new AdresseViewModel();
            if (utilisateur == null)
            {
                return HttpNotFound();
            }

            if (uAdresse.Adresse.FullName == "")
            {
                viewModel.FullName = "";
                viewModel.Id = adresse_id;

            }
            else
            {

                viewModel.Id = adresse_id;
                viewModel.FullName = uAdresse.Adresse.FullName;
                viewModel.CodePostale = uAdresse.Adresse.CodePostale;
                viewModel.NomDeVoie = uAdresse.Adresse.NomDeVoie;
                viewModel.NumVoie = uAdresse.Adresse.NumVoie;
                viewModel.Pays = uAdresse.Adresse.Pays;
                
                viewModel.Ville = uAdresse.Adresse.Ville;



            }


            return View(viewModel);
        }


        // GET: Adresse/Edit/5
        //GET:Account/EditeAdresse/@IdUser  

        public ActionResult EditAdresse(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Adresse adresse = contextAdresse.FindById((int)id);

                return View(adresse);

            }
            catch (Exception)
            {

                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> EditAdresse(Adresse adresse,string searchInput)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(adresse);
                }
                else
                {
                    
                    Adresse a = await GooglePlaceApifunctions.AdresseInformation(ConfigurationManager.AppSettings["GooglePlaceAPIKey"], searchInput);
                    a.Id = adresse.Id;
                    contextAdresse.Update(a);
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

        [WebMethod]
        public JsonResult GetSearchElemTest(string search)
        {
            string[] aa = { "ddd", "bcc", "bbb", "aaa" };
            return new JsonResult { Data = aa, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //[HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> GetSearchElem(string search)
        {


            List<string> adresses = await GooglePlaceApifunctions.AutoCompleteSearch(ConfigurationManager.AppSettings["GooglePlaceAPIKey"],search);

            List<string> lst = new List<string>();

            foreach (var item in adresses)
            {
                
                lst.Add(item);
            }

            JsonResult res = new JsonResult { Data = lst, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            object data = res.Data;
            return res;
        }



    }
}