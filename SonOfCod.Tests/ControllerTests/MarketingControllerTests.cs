using Microsoft.AspNetCore.Mvc;
using SonOfCod.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SonOfCod.Tests.ControllerTests
{
    public class MarketingControllerTests
    {
        [Fact]
        public void Get_ViewResult_Index()
        {
            //Arrange
            MarketingController controller = new MarketingController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
