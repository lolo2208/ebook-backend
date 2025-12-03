using EbookBackend.Domain.Security;
using EbookBackend.Infraestructure.Security.Authentication;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EbookBackend.Tests.Security.Encryption
{
    public class PasswordHasherTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IPasswordHasher _passwordHasher;

        public PasswordHasherTest(ITestOutputHelper output)
        {
            _output = output;
            _passwordHasher = new PasswordHasher();
        }

        [Fact]
        public void Encrypt_And_Decrypt_Should_Return_Original_Text()
        {
            //Arrange
            var original = "Lolo2208!";

            //Act
            var hash = _passwordHasher.Hash(original);
            var verify = _passwordHasher.Verify(original, hash);

            //Print
            _output.WriteLine("Hashed password: " + hash);

            //Assert
            Assert.NotEqual(original, hash);
            Assert.True(verify);
        }

        [Fact]
        public void Plain_Password_And_Hash_Should_Return_Original()
        {
            //Arrange
            var hash = "bbzZyyd+bC0D7umFXHCboA==.r4BKwROBQtL0vuoMvUE4YsTi9xESJUTLXrAhAo6zH3g=.100000";
            var original = "Lolo2208!";

            //Act
            var verify = _passwordHasher.Verify(original, hash);

            //Assert
            Assert.True(verify);
        }
    }
}
