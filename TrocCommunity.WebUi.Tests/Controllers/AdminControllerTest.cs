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
    public class AdminControllerTest
    {
        IRepository<UpdateDatabaseAdmin> contextcontextDatabaseAdmin;
        IRepository<Utilisateur> contextUser;
        IRepository<Utilisateur> contextMockUser;

        [TestInitialize]
        public void Setup()
        {
            contextcontextDatabaseAdmin = new SQLRepository<UpdateDatabaseAdmin>(new MyContext());
            contextUser = new SQLRepositoryUtilisateur(new MyContext());

            contextMockUser = new MockContext<Utilisateur>();

        }


        [TestMethod]
        [TestCategory("Admin Controller")]
        public void Suppression_Compte_Utilisateur_Par_Admin()
        {
            //Arrange

            string username = "Tsdfsdfsdfsdfsdf";
            string email = "dfsdfs123@gmail.com";
            string password = "Ffffffffff1";
            string confirmPassword = "Ffffffffff1";
            string dateNaiss = "17-08-2000";
            DateTime dateNaissance = DateTime.Parse(dateNaiss);

            AdminController admin = new AdminController(contextUser);


            FormRegister formRegister = new FormRegister { UserName = username, Email = email, Password = password, Confirmpwd = confirmPassword, DateNaissance = dateNaissance };
            admin.Register(formRegister);
            Utilisateur user = ((SQLRepositoryUtilisateur)contextUser).findByEmail(formRegister.Email);
            var resultApresInsertion = admin.Index() as ViewResult;
            var viewModelApresInsertion = (List<Utilisateur>)resultApresInsertion.ViewData.Model;
            int tailleApresInsertion = viewModelApresInsertion.Count;
            //Act


            var result = admin.DeleteConfirmed(user.Id);

            var resultApresDelete = admin.Index() as ViewResult;
            var viewModeApresDelete = (List<Utilisateur>)resultApresDelete.ViewData.Model;
            int tailleApresDelete = viewModeApresDelete.Count;



            //Assert
            Assert.AreEqual(tailleApresInsertion, tailleApresDelete + 1);

        }



        [TestMethod]
        [TestCategory("Admin Controller")]
        public void Admin_Cree_Admin()
        {
            //Arrange
            



            //Act
            



            


            //Assert
            

        }



    }
}
