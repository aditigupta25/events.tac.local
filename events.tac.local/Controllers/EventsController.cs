using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;
            string message = null;
            using (var reader = new System.IO.StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch(Exception ex)
                {
                }
            }
         
            var database = Sitecore.Configuration.Factory.GetDatabase("master");
            var parentItem = database.GetItem(parentPath);
            var templateID = new TemplateID(new ID("{FB56D476-904B-4BD8-BD45-4C6B31047677}"));
            using (new SecurityDisabler())
            {
                foreach (var ev in events)
                {
                    var name = ItemUtil.ProposeValidItemName(ev.ContentHeading);
                    Item item = parentItem.Add(name, templateID);
                    item.Editing.BeginEdit();
                    item["ContentHeading"] = ev.ContentHeading;
                    item.Editing.EndEdit();
   
                }
            }
            return View();
        }
    }
}