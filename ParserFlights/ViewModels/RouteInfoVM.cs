using System.Collections.Generic;
using ParserFlights.Models;

namespace ParserFlights.ViewModels
{
   public class RouteInfoVM
    {
        public string Price { get; set; }
        public List<RouteInfo> Routes { get; set; } 
    }
}
