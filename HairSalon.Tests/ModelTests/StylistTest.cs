using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonDB.Models;
using HSalon;

namespace HairSalonDB.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tim_firman_test;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Stylist.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Stylist_DoesntSaveToDatabase_StylistCount()
        {
            //Arrange
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");

            //Act
            int result = Stylist.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Stylist()
        {
            // Arrange, Act
            Stylist firstStylist = new Stylist("Kevin Smith", "Clerk");
            Stylist secondStylist = new Stylist("Kevin Smith", "Clerk");

            // Assert
            Assert.AreEqual(firstStylist, secondStylist);
        }
        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Kevin Smith", "Clerk");

            //Act
            testStylist.Save();
            List<Stylist> result = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist>{testStylist};
            //Assert
            CollectionAssert.AreEqual(testList, result);
        }
    }

}
