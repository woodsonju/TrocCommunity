using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class AccountControllerTest
    {
        IRepository<Utilisateur> contextUser;
        IRepository<Utilisateur> contextMockUser;
        UtilisateursController controllerUser;

/*        IRepository<Adresse> controllerAdresse;
*/
        [TestInitialize]
        public void Setup()
        {
            /*contextUser = new SQLRepository<Utilisateur>(new MyContext());*/
            contextUser = new SQLRepositoryUtilisateur(new MyContext());
            contextMockUser = new MockContext<Utilisateur>();

            controllerUser = new UtilisateursController(contextUser);

            /*controllerAdresse = new */

        }

        [TestMethod]
        [TestCategory("Account Controller")]
        public void RegisterWithHttpPost_DoesInsertUser()
        {
            //Arrange
            string username = "TestUnit56";
            string email = "TestUnit56@gmail.com";
            string password = "TestUnit556";
            string confirmPassword = "TestUnit56";
            string dateNaiss = "17-08-2016";
            DateTime dateNaissance = DateTime.Parse(dateNaiss);

            var resultAvantInsertion = controllerUser.Index() as ViewResult;
            var viewModelAvantInsertion = (List<Utilisateur>)resultAvantInsertion.ViewData.Model;
            int tailleAvantInsertion = viewModelAvantInsertion.Count;

            //Act
            AccountController controller = new AccountController(contextUser);
            FormRegister formRegister = new FormRegister { UserName = username, Email = email, Password = password, Confirmpwd = confirmPassword, DateNaissance = dateNaissance };
            var result = controller.Register(formRegister);

            var resultApresInsertion = controllerUser.Index() as ViewResult;
            var viewModeApresInsertion = (List<Utilisateur>)resultApresInsertion.ViewData.Model;
            int tailleApresInsertion = viewModeApresInsertion.Count;

            //Assert
            Assert.AreEqual(tailleApresInsertion, tailleAvantInsertion + 1);

        }


        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void RegisterAction_DoesReturn_AdresseIsNotNull()
        {
            //Arrange
            string username = "TestAdresse34";
            string email = "testAdresse34@gmail.com";
            string password = "testAdresse34";
            string confirmPassword = "testAdresse34";
            string dateNaiss = "18-11-2011";
            DateTime dateNaissance = DateTime.Parse(dateNaiss);

            //Act
            AccountController controller = new AccountController(contextUser);
            FormRegister formRegister = new FormRegister { UserName = username, Email = email, Password = password, Confirmpwd = confirmPassword, DateNaissance = dateNaissance };
            var result = controller.Register(formRegister);

            Utilisateur utilisateur = ((SQLRepositoryUtilisateur)contextUser).findByEmail("testAdresse34@gmail.com");

            //Assert
            Assert.IsNotNull(utilisateur.Adresse);
        }

    }
}
