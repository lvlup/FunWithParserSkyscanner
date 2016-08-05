using System.ComponentModel;

namespace ParserFlights.Models
{
   public class RouteInfo
    {
        [DisplayName("Авиакомпания")]
        public string AviaCompanyName { get; set; }

        [DisplayName("Номер рейса")]
        public string RouteNumber { get; set; }

        [DisplayName("Время отбытия")]
        public string DepartureTime { get; set; }

        [DisplayName("Время прибытия")]
        public string ArrivalTime { get; set; }
    }
}
