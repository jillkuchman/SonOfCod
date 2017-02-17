using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SonOfCod.Controllers;
using Xunit;

namespace SonOfCod.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Get_ViewResult_Index()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
