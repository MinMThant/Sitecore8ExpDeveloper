﻿using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            using (var reader = new StreamReader(file.InputStream))
            {
                var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(reader.ReadToEnd());
                CreateEvents(events, parentPath);
            }
            return View();
        }

        private void CreateEvents(IEnumerable<Event> events, string parentPath)
        {
            var database = Factory.GetDatabase("master");
            var parent = database.GetItem(parentPath);
            var templateId = new TemplateID(new ID("{33AA7A2F-B443-4199-BAB8-F88830CE6501}"));
            using (new SecurityDisabler())
            {
                foreach (var ev in events)
                {
                    var name = ItemUtil.ProposeValidItemName(ev.ContentHeading);
                    var item = parent.Add(name, templateId);
                    using (new EditContext(item))
                    {
                        item["ContentHeading"] = ev.ContentHeading;
                        item["ContentIntro"] = ev.ContentIntro;
                        item["Difficulty Level"] = ev.Difficulty.ToString();
                        item["Duration"] = ev.Duration.ToString();
                        item["Highlights"] = ev.Highlights;
                        item["Start Date"] = DateUtil.ToIsoDate(ev.StartDate);
                        item[FieldIDs.Workflow] = "{97EE4F91-4053-4F5B-A000-F86CABB14781}";
                        item[FieldIDs.WorkflowState] = "{870CA50B-225D-4481-A839-EB99E83F40E8}";
                    }
                }
            }
        }
    }
}