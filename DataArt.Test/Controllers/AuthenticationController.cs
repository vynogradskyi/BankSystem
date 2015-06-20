using System.Web.Mvc;
using System.Web.Security;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Models;

namespace DataArt.Test.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IAccountService _service;

        public AuthenticationController(IAccountService service)
        {
            _service = service;
        }

        private const string Redirected = "IsRedirectedFromEnterCardNumber";
        [HttpGet]
        public ActionResult EnterCardNumber()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnterCardNumber(CardViewModel card)
        {
            if (!_service.CheckCardExist(card.CardNumber))
            {
                ModelState.AddModelError("CardNumber", "Card doesn't exist");
                return View();
            }

            if (!_service.CheckCardNotBlocked(card.CardNumber))
            {
                return View("Error");
            }
            TempData[Redirected] = true;
            return RedirectToAction("EnterPin", new {cardNumber = card.CardNumber});
        }

        [HttpGet]
        public ActionResult EnterPin(string cardNumber)
        {
            if (TempData[Redirected] == null || !(bool)TempData[Redirected]) return View("Error");//todo: message "direct invocation of EnterPin prohibited" 
            var isNotBlocked = _service.CheckCardNotBlocked(cardNumber);
            var card = new CardViewModel
            {
                CardNumber = cardNumber
            };
            return TempData.ContainsKey(Redirected) &&
                   isNotBlocked
                ? View(card)
                : View("Error"); //todo: Card blocked
        }

        [HttpPost]
        public ActionResult EnterPin(CardViewModel card)
        {
            if (_service.CheckPin(card.CardNumber,card.Pin))
            {
                var user = _service.GetUser(card.CardNumber);
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session["User"] = user;
                return RedirectToAction("Index", "Operations");
            }


            if(_service.CheckCardNotBlocked(card.CardNumber))
            {
                ModelState.AddModelError("Pin", "Wrong Pin");
            }
            else
            {
                return View("Error"); //todo: add message "Your card is blocked"
            }

            return View();

        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("EnterCardNumber");
        }
    }
}