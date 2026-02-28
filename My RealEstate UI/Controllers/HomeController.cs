using System;
using System.Web.Mvc;

namespace My_RealEstate_UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Home";
            return View();
        }

        // GET: /Home/Service
        public ActionResult Service()
        {
            ViewBag.ActivePage = "Service";
            return View();
        }

        // GET: /Home/Agents
        public ActionResult Agents()
        {
            ViewBag.ActivePage = "Agents";
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.ActivePage = "Contact";
            return View();
        }

        // POST: /Home/Subscribe  (Footer email capture)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["SubscribeError"] = "Please enter a valid email address.";
                return RedirectToAction("Index");
            }

            // TODO: Persist email to database / send to mailing service
            TempData["SubscribeSuccess"] = "Thank you! You have been subscribed.";
            return RedirectToAction("Index");
        }
    }
}
