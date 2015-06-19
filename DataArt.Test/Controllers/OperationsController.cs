using System.Web.Mvc;
using DataArt.Test.Core.Abstract;

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
            return View();
        }

        [HttpGet]
        public ActionResult GetMoney()
        {
            return View();
        }
    }


}
}