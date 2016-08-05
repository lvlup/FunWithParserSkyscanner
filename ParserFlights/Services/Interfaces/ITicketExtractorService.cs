using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserFlights.Models;
using ParserFlights.ViewModels;

namespace ParserFlights.Services.Interfaces
{
    public interface ITicketExtractorService
    {
        RouteInfoVM ExtractRouteInfo(SearchRouteParameters parameters);
    }
}
