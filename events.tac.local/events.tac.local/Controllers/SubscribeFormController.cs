using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Analytics.Outcome.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;
using Sitecore.Data;

namespace events.tac.local.Controllers
{
    public class SubscribeFormController : Controller
    {
        // GET: SubscribeForm
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(string email)
        {
            Tracker.Current.Session.Identify(email);
            var contact = Tracker.Current.Contact;
            var emails = contact.GetFacet<IContactEmailAddresses>("Emails");
            if (!emails.Entries.Contains("personal"))
            {
                emails.Preferred = "personal";
                var personalEmail = emails.Entries.Create("personal");
                personalEmail.SmtpAddress = email;
            }
            var outcome = new ContactOutcome(ID.NewID, new ID("{E9A7385F-5C84-4C8D-AE77-AB7B6615FEC4}"), new ID(contact.ContactId));
            Tracker.Current.RegisterContactOutcome(outcome);
            return View("Confirmation");
        }

    }
}