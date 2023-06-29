using System.Text.Json.Nodes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SecretsMocker.Helpers
{
    public static class MockHelpers
    {
        private const string SeedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Rand = new();
        
        public static string GenerateId()
        {
            var suffix = new string(Enumerable.Repeat(SeedChars, 12).Select(s => s[Rand.Next(s.Length)]).ToArray());
            return $"tmp.user{suffix}";
        }

        public static string GenerateGuidId()
        {
            return Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// Generates a new asymmetric key pair
        /// Example:  "{\"cert\":\"CER-contents\",\"secret_key\":\"PFX-as-string\"}"
        /// </summary>
        /// <returns>A serialized json object with cert and secret_key fields</returns>
        public static string GenerateCertPayload()
        {
            var ecdsa = ECDsa.Create(); // asymmetric key pair
            var req = new CertificateRequest("cn=foobar", ecdsa, HashAlgorithmName.SHA256);
            var certificate2 = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));

            // Create PFX (PKCS #12) with private key
            var keyBytes = certificate2.Export(X509ContentType.Pfx, "P@55w0rd");
            var privateKey = System.Text.Encoding.Default.GetString(keyBytes);

            // Create Base 64 encoded CER (public key only)
            var cert = "-----BEGIN CERTIFICATE-----\r\n"
                + Convert.ToBase64String(certificate2.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                + "\r\n-----END CERTIFICATE-----";
            
            var json = new JsonObject
            {
                { "cert", cert },
                { "secret_key", privateKey }
            };
            return json.ToJsonString();
        }
        
        /// <summary>
        /// Generates a new password
        /// Example:  "{\"password\":\"AwEsOmE-PaSsWoRd_638236565228985941\"}"
        /// </summary>
        /// <returns>A serialized json object with a password field</returns>
        public static string GeneratePasswordPayload()
        {
            var json = new JsonObject
            {
                { "password", $"AwEsOmE-PaSsWoRd_{DateTime.UtcNow.Ticks}" }
            };
            return json.ToJsonString();
        }
    }
}
