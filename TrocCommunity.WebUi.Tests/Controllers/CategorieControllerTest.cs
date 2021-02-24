using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TrocCommunity.Core.Logic;
using TrocCommunity.Core.Models;
using TrocCommunity.DataAccess.SQL;
using TrocCommunity.DataAccess.SQL.DAO;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Tests.Controllers
{

    [TestClass]
    public class CategorieControllerTest
    {
        private ICategorieService service;
        private IRepository<Categorie> repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new SQLRepository<Categorie>(new MyContext());
            service = new CategorieService(repository); 
        }


        [TestMethod]
        public void TestAddCategorie()
        {
            //Arrange
            List<Categorie> categories = new List<Categorie>();
            categories.Add(new Categorie("Littérature"));
            categories.Add(new Categorie("Bien-être, Santé et vie pratique"));
            categories.Add(new Categorie("Jeunesse"));
            categories.Add(new Categorie("Bandes dessinées, et Manga"));
            categories.Add(new Categorie("Art et Sciences humaine"));
            categories.Add(new Categorie("Scolaire et Pédagogie"));
            categories.Add(new Categorie("Loisirs créatifs, nature et voyages"));
            categories.Add(new Categorie("Sciences, Techniques et Medecine "));
            categories.Add(new Categorie("Entreprise, droit et économique"));

            //Act
            for(int i=0; i<categories.Count; i++)
            {
                service.Insert(categories[i]);
            }

            //Assert
            Assert.AreEqual(9, categories.Count);

        }
    }
}
