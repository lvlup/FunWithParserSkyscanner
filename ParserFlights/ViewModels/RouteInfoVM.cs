using System.Collections.Generic;
using System.ComponentModel;
using ParserFlights.Models;

namespace ParserFlights.ViewModels
{
   public class RouteInfoVM
    {
        [DisplayName("Цена")]
        public string Price { get; set; }
        public List<RouteInfo> Routes { get; set; } 
    }
}
