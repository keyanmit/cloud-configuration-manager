using System.Security.Cryptography.X509Certificates;

namespace SigningAndEncryption
{
    public interface ISigningAuthority
    {
        string GetBase64EncodedSignedHashForPayload(object payload, X509Certificate2 signingCert);
        bool VerifySignatureFromBase64EncodedHash(string hash, object payload, X509Certificate2 clientCert);
    }
}
