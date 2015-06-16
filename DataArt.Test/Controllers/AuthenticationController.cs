using System.Web.Mvc;
using DataArt.Test.Models;

namespace DataArt.Test.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult EnterCardNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterCardNumber(CardViewModel card)
        {
            return RedirectToAction("EnterPin");
        }

        [HttpGet]
        public ActionResult EnterPin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterPin(CardViewModel card)
        {
            return View();
        }
    }
}