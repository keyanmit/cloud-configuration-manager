using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using ZooManContracts.Interface;

namespace SigningAndEncryption.SigningAndExcryption
{
    public class RSASignatureAuthority : ISigningAuthority
    {
        private readonly object _algo = CryptoConfig.MapNameToOID("SHA1");
        public string GetBase64EncodedSignedHashForPayload(object payload, X509Certificate2 signingCert)
        {
            return GetBase64EncodedSignedHashForPayload(payload, signingCert,_algo);
        }

        public bool VerifySignatureFromBase64EncodedHash(string hash, object payload, X509Certificate2 clientCert)
        {
            return VerifySignatureFromBase64EncodedHash(hash, payload, clientCert, _algo);
        }

        public string GetBase64EncodedSignedHashForPayload(object payload, X509Certificate2 signingCert,
            object cryptoAlgo)
        {            
            if(signingCert == null || signingCert.HasPrivateKey == false)
                throw new ArgumentException("siging certificate is not valid. make sure u provide a pfx cert with a private key for siging.");

            cryptoAlgo = cryptoAlgo ?? _algo;
            var cryptoProvider = signingCert.PrivateKey as RSACryptoServiceProvider;
                       

            var serializedPayload = JsonConvert.SerializeObject(payload);
            var payloadBytes = ByteConvertHelper.GetBytes(serializedPayload);
            var hashPayload = cryptoProvider.SignData(payloadBytes, cryptoAlgo);
            return Convert.ToBase64String(hashPayload); //ByteStringHelper.getString(hashPayload);    
        }


        public bool VerifySignatureFromBase64EncodedHash(string hash, object payload, X509Certificate2 clientCert, object cryptoAlgo)
        {
            cryptoAlgo = cryptoAlgo ?? _algo; ;
            
            if(clientCert == null || clientCert.HasPrivateKey)
                throw new ArgumentException("Pass a valid client certificate. For security reasons this certificate should not have private key.");

            var cryptoProvider = clientCert.PublicKey.Key as RSACryptoServiceProvider;

            
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var payloadBytes = ByteConvertHelper.GetBytes(serializedPayload);
            var decodedHash = Convert.FromBase64String(hash);
            return cryptoProvider.VerifyData(payloadBytes, cryptoAlgo, decodedHash);            
        }
    }
}
