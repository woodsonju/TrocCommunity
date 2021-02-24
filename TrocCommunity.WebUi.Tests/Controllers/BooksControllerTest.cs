using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TrocCommunity.Core.Models;
using TrocCommunity.WebUi.ApiClient;
using TrocCommunity.WebUi.Service;

namespace TrocCommunity.WebUi.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        private BookService service;

        string isbn = "";
       

     [TestInitialize]
        public void Setup()
        {
            //Arrange
            isbn = "9780631168522";
            /* isbn = "9780631168522";   The Berbers*/
            /* isbn = "9780810867307";   No Country for Old Men*/

            service = new BookService();
        }

        [TestMethod]
        public async void GetBookByISBN_DoesReturnNumberAuthors()
        {
            //Arrange
            int nobreAuteur = 2;

            //Act
            List<string> auteursApi = await service.GetAuthors(isbn);

            //Assert
            Assert.AreEqual(nobreAuteur, auteursApi.Count);

        }

        [TestMethod]
        public async void GetBookByISBN_DoesReturnVolume()
        {
            //Arrange
            int volume = 350;

            //Act
            int volumeApi = await service.GetVolume(isbn);

            //Assert
            Assert.AreEqual(volume, volumeApi);
        }

        [TestMethod]
        public async void GetBookByISBN_DoesReturnTitle()
        {
            //Arrange
            string title = "The Berbers";

            //Act
            string titleApi = await service.GetTitle(isbn);

            //Assert
            Assert.AreEqual(title, titleApi);

        }

        [TestMethod]
        public async void GetBookByISBN_DoesReturnPrice()
        {
            //Arrange
            string priceOk = "OK";
            string priceNOK = "NOK";

            //Act
            double price = await service.GetPrice(isbn);

            string isPrice = (price <= 50 && price >= 6) ? priceOk : priceNOK;

            //Assert
            Assert.AreEqual(priceOk, isPrice);

        }


        [TestMethod]
        public void GetPointsByPriceAndBookStateExchangeFalse_DoesReturnNbrPoints()
        {
            //Arrange
            double price = 20;
            EtatDuLivre bonEtat = EtatDuLivre.BONETAT;
            double nbrPoints = price * (int)bonEtat * 0.01;
            bool exchange = false;

            //Act
            double nbrPointsByService = service.GetPoints(price, bonEtat, exchange);



            //Assert
            Assert.AreEqual(nbrPoints, nbrPointsByService);

        }

        [TestMethod]
        public void GetPointsByPriceAndBookStateExchangeTrue_DoesReturnNbrPoints()
        {
            //Arrange
            double price = 20;
            EtatDuLivre bonEtat = EtatDuLivre.BONETAT;
            double nbrPoints = price * (int)bonEtat * 0.01 * 0.25;
            bool exchange = true;

            //Act
            double nbrPointsByService = service.GetPoints(price, bonEtat, exchange);



            //Assert
            Assert.AreEqual(nbrPoints, nbrPointsByService);

        }

    }
}
