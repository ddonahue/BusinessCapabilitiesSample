using System;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class PricingController : Controller
    {
        public ActionResult CustomerStatus(Guid customerId)
        {
            return Content(Pricing.Read.API.GetCustomerStatus(customerId));
        }
    }
}