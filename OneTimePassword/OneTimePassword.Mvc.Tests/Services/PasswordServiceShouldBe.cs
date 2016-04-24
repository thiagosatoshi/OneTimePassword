using NUnit.Framework;
using OneTimePassword.Core.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTimePassword.Mvc.Tests.Services
{
    [TestFixture]
    public class PasswordServiceShouldBe
    {
        private PasswordService _service;
        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [SetUp]
        public void Setup()
        {
            _service = new PasswordService();
        }

        [TestCase("thiagosatoshi@gmail.com", 123456)]
        [TestCase("user@gmail.com", 123456)]
        public void Generate_NonEmpty_Password(string userId, long iterationNumber)
        {
            var password = _service.GeneratePassword(userId, iterationNumber, 6);
            password.ShouldNotBeNullOrWhiteSpace();
        }

        [TestCase("thiagosatoshi@gmail.com")]
        [TestCase("anotheruser@gmail.com")]
        public void Generate_Password_SixDigits(string userId)
        {
            var password = _service.GeneratePassword(userId);
            password.Length.ShouldBe(6);
        }

        [TestCase("thiagosatoshi@gmail.com", 123456)]
        [TestCase("user@gmail.com", 123456)]
        public void Generate_Password_Overload_SixDigits(string userId, long iterationNumber)
        {
            var password = _service.GeneratePassword(userId, iterationNumber);
            password.Length.ShouldBe(6);
        }

        [TestCase("thiagosatoshi@gmail.com", 123456)]
        public void Generate_Valid_Password(string userId, long iterationNumber)
        {
            var password = _service.GeneratePassword(userId, iterationNumber);
            var valid = _service.IsPasswordValid(userId, password, iterationNumber);

            valid.ShouldBe(true);
        }

        [TestCase("thiagosatoshi@gmail.com")]
        public void Generate_Valid_WrongPassword_Afer30seconds(string userId)
        {
            long iteration = (long)(DateTime.UtcNow - UNIX_EPOCH).TotalSeconds / 30;
            long iteration31seconds = (long)(DateTime.UtcNow.AddSeconds(30) - UNIX_EPOCH).TotalSeconds / 30;

            var password = _service.GeneratePassword(userId, iteration);
            
            var valid = _service.IsPasswordValid(userId, password, iteration31seconds);

            valid.ShouldBe(false);
        }

        [TestCase("thiagosatoshi@gmail.com", 123456)]
        public void Generate_Valid_SamePassword_AnotherID(string userId, long iterationNumber)
        {
            var password = _service.GeneratePassword(userId, iterationNumber);
            var valid = _service.IsPasswordValid("another@gmail.com", password, iterationNumber);

            valid.ShouldBe(false);
        }
    }
}
