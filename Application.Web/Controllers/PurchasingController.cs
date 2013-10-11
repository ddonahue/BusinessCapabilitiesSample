using System;
using System.Web.Mvc;
using NServiceBus;
using Purchasing.Commands;

namespace Application.Web.Controllers
{
    public class PurchasingController : Controller
    {
        private IBus bus;

        public PurchasingController(IBus bus)
        {
            this.bus = bus;
        }

        public ActionResult Count(Guid customerId)
        {
            return Content(Purchasing.Read.API.GetPurchases(customerId));
        }

        [HttpPost]
        public RedirectToRouteResult Add(Guid customerId)
        {
            var command = new AddPurchase { CustomerId = customerId, PurchaseId = Guid.NewGuid() };
            bus.Send(command);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult New(Guid customerId)
        {
            return PartialView("New", customerId);
        }
    }
}
