using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalonDB.Models;
using HSalon;

namespace HairSalonDB.Tests
{
    [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public SpecialtyTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=tim_firman_test;";
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
            Specialty.DeleteAll();
        }

        [TestMethod]
        public void GetAll_SpecialtiesEmptyAtFirst_0()
        {
            int result = Specialty.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSame_Specialty()
        {
            Specialty firstSpecialty = new Specialty("Mullets");
            Specialty secondSpecialty = new Specialty("Mullets");
            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void Find_FindFindsSpecialty_Specialty()
        {
            Specialty testSpecialty = new Specialty("Mullets");
            testSpecialty.Save();
            Specialty result = Specialty.Find(testSpecialty.GetId());
            Assert.AreEqual(result, testSpecialty);
        }
    }
}
