using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ParserFlights.Models;
using ParserFlights.Services.Implementations;
using ParserFlights.Services.Interfaces;

namespace ParserFlights.Controllers
{
    public class ParserController : Controller
    {
        private readonly ITicketExtractorService ticketExtractorService;

        public ParserController(ITicketExtractorService service)
        {
            this.ticketExtractorService = service;
        }
 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result(SearchRouteParameters parameters)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var model = ticketExtractorService.ExtractRouteInfo(parameters);

            return View(model);
        }
    }
}