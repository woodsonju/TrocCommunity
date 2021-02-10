using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.Core.Tools;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.Controllers;
using TrocCommunity.WebUi.Tests.Mocks;

namespace TrocCommunity.WebUi.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        IRepository<Livre> contextLivre;
        IRepository<Categorie> ContextCategorie;

        [TestInitialize]
        public void Setup()
        {
            contextLivre = new SQLRepository<Livre>(new MyContext());
            ContextCategorie = new SQLRepository<Categorie>(new MyContext());


        }

        [TestMethod]
        [TestCategory("Categorie Controller")]
        public void Liste_Categorie()
        {
            //Arrange
            CategoriesController categorie = new CategoriesController(ContextCategorie, contextLivre);

            //Act

            var result = categorie.Catalogue() as ViewResult;
            var viewModel = (CategorieLivre)result.ViewData.Model;

            //Assert
            Assert.AreEqual(9, viewModel.Categories.Count()); 

        }

        [TestMethod]
        [TestCategory("Categorie Controller")]
        public void Livre_Par_Categorie()
        {
            //Arrange
            CategoriesController categorie = new CategoriesController(ContextCategorie, contextLivre);
   
            //Act
            var LivreJeunesse = (from Livre in contextLivre.Collection() where Livre.Categorie.Id == 4 select Livre).Count();
            var result = categorie.Jeu() as ViewResult;
            var viewModel = (CategorieLivre)result.ViewData.Model;

            //Assert
            Assert.AreEqual(LivreJeunesse, viewModel.Livres.Count());

        }
    }
}
