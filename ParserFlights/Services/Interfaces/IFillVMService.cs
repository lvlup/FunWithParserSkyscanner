using HtmlAgilityPack;
using ParserFlights.ViewModels;

namespace ParserFlights.Services.Interfaces
{
   public interface IFillVMService
    {
        void FillVM(RouteInfoVM vm, HtmlNode nodeWithSummoryInfo, HtmlNode nodeWithDetails);
    }
}
