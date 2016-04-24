using NSubstitute;
using NUnit.Framework;
using OneTimePassword.Core.Interfaces;
using OneTimePassword.Mvc.Controllers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OneTimePassword.Mvc.Tests.Controllers
{
    [TestFixture]
    public class OneTimePasswordControllerShould
    {
        private OneTimePasswordController _controller;
        private IPasswordService _service;

        [SetUp]
        public void Setup()
        {
            _service = Substitute.For<IPasswordService>();
            _controller = new OneTimePasswordController(_service);
        }

        [TestCase]
        public void Show_Index()
        {
            var response = _controller.Index();
            response.ShouldNotBe(null);
        }

        [TestCase("thiagosatoshi@gmail.com")]
        [TestCase("anotheruser@gmail.com")]
        public void Receive_UserId_and_GeneratePassword(string userId)
        {
            var response = _controller.GeneratePasword(userId);
            _service.Received(1).GeneratePassword(userId);
        }

        [TestCase(null, null )]
        public void IsValid_AllowGet(string userId, string password)
        {
            var response = _controller.IsPasswordValid(userId, password);
            response.JsonRequestBehavior.ShouldBe(JsonRequestBehavior.AllowGet);

            _service.Received(0).IsPasswordValid(userId, password);
        }
    }
}
