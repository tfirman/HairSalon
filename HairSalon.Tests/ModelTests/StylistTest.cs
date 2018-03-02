using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonDB.Models;
using HSalon;

namespace HairSalonDB.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tim_firman_test;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            int result = Stylist.GetAll().Count;
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Stylist_DoesntSaveToDatabase_StylistCount()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            int result = Stylist.GetAll().Count;
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Stylist()
        {
            Stylist firstStylist = new Stylist("Kevin Smith", "Clerk");
            Stylist secondStylist = new Stylist("Kevin Smith", "Clerk");
            Assert.AreEqual(firstStylist, secondStylist);
        }
        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            testStylist.Save();
            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};
            CollectionAssert.AreEqual(testList, result);
        }
        [TestMethod]
        public void GetClients_RetrievesAllClientsWithStylist_ClientList()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            testStylist.Save();
            Client firstClient = new Client("Melissa Arnold", testStylist.GetId());
            firstClient.Save();
            Client secondClient = new Client("Tony Akin", testStylist.GetId());
            secondClient.Save();
            List<Client> testClientList = new List<Client> {firstClient, secondClient};
            List<Client> resultClientList = testStylist.GetClients();

            CollectionAssert.AreEqual(testClientList, resultClientList);
        }

        [TestMethod]
        public void Find_FindFindsStylist_Stylist()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            testStylist.Save();
            Stylist result = Stylist.Find(testStylist.GetId());
            Assert.AreEqual(result, testStylist);
        }

        [TestMethod]
        public void Edit_EditChangesStylist_Stylist()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            testStylist.Save();
            testStylist.Edit("Al Pacino", "Hitman");
            Stylist result = Stylist.Find(testStylist.GetId());
            Assert.AreEqual("Al Pacino", result.GetName());
            Assert.AreEqual("Hitman", result.GetDescription());
        }

        [TestMethod]
        public void Delete_DeleteRemovesStylist_Stylist()
        {
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");
            testStylist.Save();
            testStylist.Delete();
            List<Stylist> testList = new List<Stylist>{};
            List<Stylist> result = Stylist.GetAll();
            CollectionAssert.AreEqual(testList, result);
        }
    }
}
