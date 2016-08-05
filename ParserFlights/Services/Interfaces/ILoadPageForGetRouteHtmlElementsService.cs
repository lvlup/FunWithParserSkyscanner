using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserFlights.Services.Interfaces
{
    public interface ILoadPageForGetRouteHtmlElementsService
    {
        void WaitLoadPage(string url);
        string GetSummoryInnerHtml();
        string GetDetailsInnerHtml();
        void Dispose();
    }
}
