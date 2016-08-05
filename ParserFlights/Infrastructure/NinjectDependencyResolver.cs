using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ninject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using ParserFlights.Services.Implementations;
using ParserFlights.Services.Interfaces;

namespace ParserFlights.Infrastructure
{
  public  class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
              kernel.Bind<ITicketExtractorService>().To<TicketExtractorService>();
              kernel.Bind<IFillVMService>().To<FillVMService>();
              kernel.Bind<ILoadPageForGetRouteHtmlElementsService>().To<LoadPageForGetRouteHtmlElementsService>();

              //kernel.Bind<IWebDriver>().To<ChromeDriver>().WithConstructorArgument("chromeDriverDirectory", AppDomain.CurrentDomain.BaseDirectory);
              kernel.Bind<IWebDriver>().To<PhantomJSDriver>().WithConstructorArgument("phantomJSDriverServerDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
