using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalonDB.Models;

namespace HairSalonDB.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/specialty")]
        public ActionResult SpecialIndex()
        {
            return View();
        }

        [HttpGet("/specialty/new")]
        public ActionResult CreateSpecialForm()
        {
            return View();
        }

        [HttpPost("/specialty/delete")]
        public ActionResult DeleteAll()
        {
            Specialty.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/specialty/{id}")]
        public ActionResult SpecialDetails(int id)
        {
            Specialty specialty = Specialty.Find(id);
            return View(specialty);
        }

        [HttpPost("/specialty")]
        public ActionResult CreateSpecialty()
        {
            Specialty newSpecialty = new Specialty (Request.Form["new-specialty"]);
            newSpecialty.Save();
            return RedirectToAction("SpecialIndex");
        }

        [HttpPost("/specialty/{id}/addStylist")]
        public ActionResult AddSpecialStylist(int id)
        {
            Specialty thisSpecialty = Specialty.Find(id);
            Stylist newStylist = Stylist.Find(Int32.Parse(Request.Form["new-stylist"]));
            thisSpecialty.AddStylist(newStylist);
            return RedirectToAction("SpecialDetails", id);
        }

    }
}
