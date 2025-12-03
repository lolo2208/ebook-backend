using EbookBackend.Infraestructure.Security.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EbookBackend.Tests.Security.Encryption
{
    public class AesEncryptionTests
    {
        private readonly string _key = "1261882437151234ABCD901234567890";
        private readonly ITestOutputHelper _output;

        public AesEncryptionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Encrypt_And_Decrypt_Should_Return_Original_Text()
        {
            // Arrange
            var original = "Server=tcp:sqlserver-free-lgomez.database.windows.net,1433;Initial Catalog=ebookstore-free;Persist Security Info=False;User ID=lgomez;Password=Kcrrbgh9!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;";


            // Act
            var encrypted = AesEncryption.Encrypt(original, _key);
            var decrypted = AesEncryption.Decrypt(encrypted, _key);

            // Print
            _output.WriteLine("Encrypted value: " + encrypted);

            // Assert
            Assert.NotEqual(original, encrypted);
            Assert.Equal(original, decrypted);
        }

        [Fact]
        public void Encrypt_Should_Generate_Different_IV_Each_Time()
        {
            // Arrange
            var text = "Hola mundo";

            // Act
            var e1 = AesEncryption.Encrypt(text, _key);
            var e2 = AesEncryption.Encrypt(text, _key);

            // Assert
            Assert.NotEqual(e1, e2);
        }

        [Fact]
        public void Decrypt_Should_Throw_When_Key_Is_Wrong()
        {
            // Arrange
            var encrypted = AesEncryption.Encrypt("texto", _key);
            var wrongkey = "12347108736572980736512";

            // Act + Assert
            Assert.ThrowsAny<Exception>(() => AesEncryption.Decrypt(encrypted, wrongkey));
        }

    }
}
