using Microsoft.AspNetCore.Mvc;
using SonOfCod.Controllers;
using SonOfCod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SonOfCod.Tests.ModelTests
{
    public class NewsletterRecipientTest
    {
        private SonOfCodDbContext db = new SonOfCodDbContext();

        [Fact]
        public void SaveToDatabase_NewsletterRecipient_CorrectType()
        {
            //Arrange
            ApplicationUser user = new ApplicationUser() { };
            user.UserName = "Test User";
            db.Users.Add(user);
            db.SaveChanges();
            NewsletterRecipient newRecipient = new NewsletterRecipient() { };
            newRecipient.AppUser = user;

            //Act
            db.MailingList.Add(newRecipient);
            db.SaveChanges();
            NewsletterRecipient foundRecipient = db.MailingList.FirstOrDefault(ml => ml.AppUser == user);

            //Assert
            Assert.Equal(newRecipient.AppUserId, foundRecipient.AppUserId);
            db.MailingList.Remove(newRecipient);
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
