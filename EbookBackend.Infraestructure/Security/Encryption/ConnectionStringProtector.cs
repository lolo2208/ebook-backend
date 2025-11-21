using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.Security.Encryption
{
    public class ConnectionStringProtector
    {
        private readonly IConfiguration _config;

        public ConnectionStringProtector(IConfiguration config)
        {
            _config = config;
        }

        public string EncryptConnectionString(string connString)
        {
            var key = _config["EncryptionKey"]!;
            return AesEncryption.Encrypt(connString, key);
        }

        public string DecryptConnectionString(string encryptedConn)
        {
            var key = _config["EncryptionKey"]!;
            return AesEncryption.Decrypt(encryptedConn, key);
        }
    }
}
