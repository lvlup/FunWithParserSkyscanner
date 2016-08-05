using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ParserFlights.Models;
using ParserFlights.Services.Interfaces;
using ParserFlights.ViewModels;

namespace ParserFlights.Services.Implementations
{
    public class TicketExtractorService : ITicketExtractorService
    {
        private readonly IFillVMService fillVmService;
        private readonly ILoadPageForGetRouteHtmlElementsService loadPageForGetRouteHtmlElementsService;

        public TicketExtractorService(IFillVMService fillVmService, ILoadPageForGetRouteHtmlElementsService loadPageForGetRouteHtmlElementsService)
        {
            this.fillVmService = fillVmService;
            this.loadPageForGetRouteHtmlElementsService = loadPageForGetRouteHtmlElementsService;
        }


        public RouteInfoVM ExtractRouteInfo(SearchRouteParameters parameters)
        {
            var result = new RouteInfoVM {Routes = new List<RouteInfo> {new RouteInfo(), new RouteInfo()}};

            var htmlSummoryDoc = new HtmlDocument();
            var htmlDetailsDoc = new HtmlDocument();

            var url =
                $"https://www.skyscanner.ru/transport/flights/{parameters.Source.ToLower()}/{parameters.Destination.ToLower()}/{parameters.DateSource}/{parameters.DateDestination}#results";

            loadPageForGetRouteHtmlElementsService.WaitLoadPage(url);
            htmlSummoryDoc.LoadHtml(loadPageForGetRouteHtmlElementsService.GetSummoryInnerHtml());
            htmlDetailsDoc.LoadHtml(loadPageForGetRouteHtmlElementsService.GetDetailsInnerHtml());
            loadPageForGetRouteHtmlElementsService.Dispose();

            //начинаем парсить и заполнять вьюмодель данными
            fillVmService.FillVM(result, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            return result;
        }
    }
}
