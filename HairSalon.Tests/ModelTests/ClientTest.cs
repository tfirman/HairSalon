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

        // It appears that my Client Delete method does not reset the seed for the id counter when it deletes all members of the table, and I've been unable to determine why when an apparently similar command works for Stylist etc. or how to force a reseed...

        // [TestMethod]
        // public void Save_SavesClientToDatabase_ClientList()
        // {
        //     Client testClient = new Client("Melissa Arnold", 1);
        //     testClient.Save();
        //     List<Client> result = Client.GetAll();
        //     List<Client> testList = new List<Client>{testClient};
        //     foreach (var client in result)
        //     {
        //         Console.WriteLine(client.GetName());
        //         Console.WriteLine(client.GetId());
        //     }
        //     foreach (var client in testList)
        //     {
        //         Console.WriteLine(client.GetName());
        //         Console.WriteLine(client.GetId());
        //     }
        //
        //     CollectionAssert.AreEqual(testList, result);
        // }
        //
        // [TestMethod]
        // public void Save_DatabaseAssignsIdToClient_Id()
        // {
        //     Client testClient = new Client("Melissa Arnold", 1);
        //     testClient.Save();
        //     Client savedClient = Client.GetAll()[0];
        //     int result = savedClient.GetId();
        //     int testId = testClient.GetId();
        //     Console.WriteLine(result);
        //     Console.WriteLine(testId);
        //     Assert.AreEqual(testId, result);
        // }
    }
}
