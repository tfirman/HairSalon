using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalonDB.Models;

namespace HairSalonDB.Controllers
{
    public class HairSalonDBController : Controller
    {
        [HttpGet("/")]
        public ActionResult ToIndex()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpGet("/stylists/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
            Stylist newStylist = new Stylist (Request.Form["new-stylist"],Request.Form["new-descrip"]);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            Stylist stylist = Stylist.Find(id);
            return View(stylist);
        }

        [HttpPost("/stylists/{id}")]
        public ActionResult CreateClient(int id)
        {
            Client newClient = new Client (Request.Form["new-client-name"],id);
            newClient.Save();
            return RedirectToAction("Details",id);
        }

        [HttpPost("/stylists/{id}/update")]
        public ActionResult UpdatePost(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            thisStylist.Edit(Request.Form["new-name"],Request.Form["new-descrip"]);
            return RedirectToAction("Details",id);
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult DeletePost(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            thisStylist.Delete();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/addspecialty")]
        public ActionResult AddStylistSpecialty(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            Specialty newSpecialty = Specialty.Find(Int32.Parse(Request.Form["new-specialty"]));
            thisStylist.AddSpecialty(newSpecialty);
            return RedirectToAction("Details",id);
        }
    }
}
