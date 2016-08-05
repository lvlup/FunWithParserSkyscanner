using System;
using System.Linq;
using HtmlAgilityPack;
using ParserFlights.Models;
using ParserFlights.Services.Interfaces;
using ParserFlights.ViewModels;

namespace ParserFlights.Services.Implementations
{
    public class FillVMService : IFillVMService
    {
        public void FillVM(RouteInfoVM vm, HtmlNode nodeWithSummoryInfo, HtmlNode nodeWithDetails)
        {
            try
            {
                //получаем цену
                vm.Price =
                    nodeWithSummoryInfo.Descendants()
                        .Single(n => n.GetAttributeValue("class", "").Equals("mainquote-group-price"))
                        .Descendants("a")
                        .First()
                        .InnerText.Replace("&nbsp;", " ");
                // парсим секции
                var sections = nodeWithSummoryInfo.Descendants("section").ToList();

                //заполняем маршруты именами авиокомпаний, датами
                FillSummoryRouteModel(sections.First(), vm.Routes[0]);
                FillSummoryRouteModel(sections.Last(), vm.Routes[1]);


                var routeDetailsHtml =
                    nodeWithDetails.Descendants()
                        .Where(n => n.GetAttributeValue("class", "").Equals("itinerary-leg  "))
                        .ToList();
                //заполняем маршруты номера маршрутами
                FillDetailsRouteModel(routeDetailsHtml.First(), vm.Routes[0]);
                FillDetailsRouteModel(routeDetailsHtml.Last(), vm.Routes[1]);
            }
            catch (Exception)
            {
                throw new Exception("Не удалось распарсить html элементы");
            }
        }

        private void FillSummoryRouteModel(HtmlNode sectionNode, RouteInfo route)
        {
            try
            {
                //получаем имена авиакомпаний
                HtmlNode bigAirlineNode =
                    sectionNode.Descendants().Single(n => n.GetAttributeValue("class", "").Equals("big-airline"));
                var imgTag = bigAirlineNode.Descendants("img").SingleOrDefault();

                if (imgTag != null)
                {
                    // имя авиакомпании  будет в тэге img, если не составное
                    route.AviaCompanyName = imgTag.Attributes["alt"].Value;
                }
                else
                {
                    route.AviaCompanyName =
                       sectionNode.Descendants().Single(n => n.GetAttributeValue("class", "").Equals("big-airline")).
                            Descendants("span").Single().InnerText;
                }

                //получаем время отлета
                route.DepartureTime =
                   sectionNode.Descendants().Single(n => n.GetAttributeValue("class", "").Equals("depart")).
                       Descendants("span").Single(n => n.GetAttributeValue("class", "").Equals("times")).InnerText;

                //получаем время прибытия
                route.ArrivalTime =
                   sectionNode.Descendants().Single(n => n.GetAttributeValue("class", "").Equals("arrive")).
                       Descendants("span").Single(n => n.GetAttributeValue("class", "").Equals("times")).InnerText;
            }
            catch (Exception)
            {
                throw new Exception("Не удалось распарсить html элементы");
            }

        }

        private void FillDetailsRouteModel(HtmlNode details, RouteInfo route)
        {
            try
            {
                //получаем номера рейсов
                var airRouteNumbersNodes = details.Descendants()
                    .Where(n => n.GetAttributeValue("class", "").Equals("airline-minimal")).ToList();

                route.RouteNumber = string.Join(" ", airRouteNumbersNodes.Select(x => x.Descendants("td").First().InnerText)).TrimEnd();
            }
            catch (Exception)
            {
                throw new Exception("Не удалось распарсить html элементы");
            }

        }
    }
}
