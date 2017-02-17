using Microsoft.AspNetCore.Mvc;
using SonOfCod.Controllers;
using SonOfCod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SonOfCod.Tests.ControllerTests
{
    public class NewsletterControllerTests
    {
        [Fact]
        public void Get_ViewResult_Index()
        {
            //Arrange
            NewsletterController controller = new NewsletterController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ViewResult_Signup()
        {
            //Arrange
            NewsletterController controller = new NewsletterController();

            //Act
            var result = controller.Signup();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ViewResult_AddedToNewsletter()
        {
            //Arrange
            NewsletterController controller = new NewsletterController();

            //Act
            var result = controller.AddedToNewsletter();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_ViewResult_MailingList()
        {
            //Arrange
            ViewResult mailingListView = new NewsletterController().MailingList() as ViewResult;

            //Act
            var result = mailingListView.ViewData.Model;

            //Assert
            Assert.IsType<List<ApplicationUser>>(result);
        }
    }
}
