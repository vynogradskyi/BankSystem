using System;
using System.Web.Mvc;
using DataArt.Test.Core.Abstract;
using DataArt.Test.Core.Concrete;
using DataArt.Test.Core.Domain;

namespace DataArt.Test.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private readonly IOperationsService _service;

        public OperationsController(IOperationsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Balance()
        {
            var userId = (int)Session[Strings.UserId];
            var user = _service.Balance(userId);
            return View(user);//todo: in real project will put viewmodel here
        }

        [HttpGet]
        public ActionResult GetMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMoney(int amount)
        {
            var userId = (int) Session[Strings.UserId];
            var operation = _service.GetMoney(userId, amount);
            return RedirectToAction("OperationResult", operation);
        }

        [HttpGet]
        public ActionResult OperationResult(Operation operation)
        {
            return View(operation);
        }
    }


}
