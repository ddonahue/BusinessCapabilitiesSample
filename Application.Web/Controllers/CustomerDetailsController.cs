using System;
using System.Web.Mvc;
using Application.Web.Models;
using CustomerDetails.Commands;
using NServiceBus;

namespace Application.Web.Controllers
{
    public class CustomerDetailsController : Controller
    {
        private IBus bus;

        public CustomerDetailsController(IBus bus)
        {
            this.bus = bus;
        }

        public ActionResult List()
        {
            return PartialView(CustomerDetails.Read.API.GetAllCustomers());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Create(CustomerDetailsViewModel viewModel)
        {
            var command = new CreateCustomer
                {
                    CustomerId = Guid.NewGuid(),
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };
            bus.Send(command);
            return RedirectToAction("Index", "Home");
        }
    }
}
