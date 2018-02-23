using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonDB.Models;
using HSalon;

namespace HairSalonDB.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tim_firman_test;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        [TestMethod]
        public void GetAll_ClientsEmptyAtFirst_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSame_Client()
        {
            Client firstClient = new Client("Melissa Arnold", 1);
            Client secondClient = new Client("Melissa Arnold", 1);
            Assert.AreEqual(firstClient, secondClient);
        }

    }
}
