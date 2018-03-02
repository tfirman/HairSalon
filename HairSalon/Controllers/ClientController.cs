using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalonDB.Models;

namespace HairSalonDB.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/client")]
        public ActionResult ClIndex()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpPost("/client/delete")]
        public ActionResult DeleteAll()
        {
            Client.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/client/{id}")]
        public ActionResult ClientDetails(int id)
        {
            Client client = Client.Find(id);
            return View(client);
        }

        [HttpPost("/client/{id}/update")]
        public ActionResult UpdateClient(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.Edit(Request.Form["new-name"],Int32.Parse(Request.Form["new-stylist"]));
            return RedirectToAction("ClientDetails",id);
        }

        [HttpPost("/client/{id}/delete")]
        public ActionResult DeleteClient(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.Delete();
            return RedirectToAction("ClIndex");
        }
    }
}
