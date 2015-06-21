using System.Web.Mvc;
using System.Web.Security;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Concrete;
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
                ModelState.AddModelError("CardNumber", Strings.ErrorCardDoesntExist);
                return View();
            }

            if (!_service.CheckCardNotBlocked(card.CardNumber))
            {
                return View(Strings.ErrorView);
            }
            TempData[Strings.Redirected] = true;
            return RedirectToAction("EnterPin", new {cardNumber = card.CardNumber});
        }

        [HttpGet]
        public ActionResult EnterPin(string cardNumber)
        {
            if (TempData[Strings.Redirected] == null || !(bool)TempData[Strings.Redirected]) return View(Strings.ErrorView);//todo: message "direct invocation of EnterPin prohibited" 
            var isNotBlocked = _service.CheckCardNotBlocked(cardNumber);
            var card = new CardViewModel
            {
                CardNumber = cardNumber
            };
            return TempData.ContainsKey(Strings.Redirected) &&
                   isNotBlocked
                ? View(card)
                : View(Strings.ErrorView); //todo: Card blocked
        }

        [HttpPost]
        public ActionResult EnterPin(CardViewModel card)
        {
            if (_service.CheckPin(card.CardNumber,card.Pin))
            {
                var user = _service.GetUser(card.CardNumber);
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                Session[Strings.UserId] = user.Id;
                return RedirectToAction("Index", "Operations");
            }


            if(_service.CheckCardNotBlocked(card.CardNumber))
            {
                ModelState.AddModelError("Pin", "Wrong Pin");
            }
            else
            {
                return View(Strings.ErrorView); //todo: add message "Your card is blocked"
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