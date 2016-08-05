using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserFlights.Models;
using ParserFlights.Services.Implementations;
using ParserFlights.Services.Interfaces;
using ParserFlights.ViewModels;

namespace ParserFlights.Tests.ServicesUnderTest
{
    /// <summary>
    /// Summary description for FillVMServiceTests
    /// </summary>
    [TestClass]
    public class FillVMServiceTests
    {
        private readonly IFillVMService fillVmService;
        private readonly RouteInfoVM routeInfoVm;
        private readonly HtmlDocument htmlSummoryDoc;
        private readonly HtmlDocument htmlDetailsDoc;
       
        public FillVMServiceTests()
        {
            fillVmService = new FillVMService();
            routeInfoVm = new RouteInfoVM { Routes = new List<RouteInfo>() { new RouteInfo(), new RouteInfo() } };
            htmlSummoryDoc = new HtmlDocument();
            htmlDetailsDoc = new HtmlDocument();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Не удалось распарсить html элементы")]
        public void FillVM_FillVmWithSampleData_ThrowExceptionIfSomeHtmlMissed()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfoNotExpectedChangesInHtml.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_PriceParsedCorrect()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Price, "20 843 p.");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_AviaCompanyNameParsedCorrectForFirstRoute()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[0].AviaCompanyName, "Lufthansa");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_MixedAviaCompanyNameParsedCorrectForFirstRoute()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfoMixedAviaNames.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[0].AviaCompanyName, "S7 Airlines + Air China");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_DepartureTimeParsedCorrectForFirstRoute()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[0].DepartureTime, "09:05");
        }


        [TestMethod]
        public void FillVM_FillVmWithSampleData_ArrivalTimeParsedCorrectForFirstRoute()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[0].ArrivalTime, "17:20");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_RouteNumberParsedCorrectForFirstRoute()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[0].RouteNumber, "Lufthansa LH1453  Lufthansa LH1040");
        }


        [TestMethod]
        public void FillVM_FillVmWithSampleData_AviaCompanyNameParsedCorrectForRouteBack()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[1].AviaCompanyName, "Lufthansa");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_DepartureTimeParsedCorrectForRouteBack()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[1].DepartureTime, "06:10");
        }


        [TestMethod]
        public void FillVM_FillVmWithSampleData_ArrivalTimeParsedCorrectForRouteBack()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[1].ArrivalTime, "12:50");
        }

        [TestMethod]
        public void FillVM_FillVmWithSampleData_RouteNumberParsedCorrectForRouteBack()
        {
            // Arrange
            htmlSummoryDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\summoryInfo.html"));
            htmlDetailsDoc.Load(Path.Combine(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString(), "DataSamples\\detailsInfo.html"));

            // Act
            fillVmService.FillVM(routeInfoVm, htmlSummoryDoc.DocumentNode, htmlDetailsDoc.DocumentNode);

            // Assert
            Assert.AreEqual(routeInfoVm.Routes[1].RouteNumber, "Lufthansa LH1053  Lufthansa LH1444");
        }
    }
}
