using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigningAndEncryption;
using SigningAndEncryption.SigningAndExcryption;

namespace ZooManUnitTest.Signing
{
    [TestClass]
    public class SigningAndEncryption
    {
        private readonly ISigningAuthority signatureAuthority;
        private readonly Dictionary<string, string> testObject;
 
        public SigningAndEncryption()
        {
            signatureAuthority = new RSASignatureAuthority();

            testObject = new Dictionary<string, string>();
            testObject["key1"] = Guid.NewGuid().ToString();
            testObject["key2"] = Guid.NewGuid().ToString();
            testObject["key3"] = Guid.NewGuid().ToString();
            testObject["key4"] = Guid.NewGuid().ToString();
        }
        [TestMethod]
        public void CoreFunctionality()
        {
            
            
            var cert = new X509Certificate2(@"D:\code\dump\EncryptionCert.pfx", "Password~1");
            var clientCert = new X509Certificate2(@"D:\code\dump\EncryptionCert.cer");

            var singedHash = signatureAuthority.GetBase64EncodedSignedHashForPayload(testObject, cert);

            

            //valid object
            Assert.AreEqual(true,signatureAuthority.VerifySignatureFromBase64EncodedHash(singedHash, testObject, clientCert));

            //invalid object
            testObject["key1"] = Guid.NewGuid().ToString();
            Assert.AreEqual(false, signatureAuthority.VerifySignatureFromBase64EncodedHash(singedHash, testObject, clientCert));

            
        }

        [TestMethod]
        public void ManInTheMiddle()
        {
            //Man in the middle alter the payload and generated hash
            var cert = new X509Certificate2(@"D:\code\dump\Scribble.DevBox.pfx", "Password~1");
            var clientCert = new X509Certificate2(@"D:\code\dump\EncryptionCert.cer");

            var singedHash = signatureAuthority.GetBase64EncodedSignedHashForPayload(testObject, cert);
            Assert.AreEqual(false, signatureAuthority.VerifySignatureFromBase64EncodedHash(singedHash, testObject, clientCert));   
        }
    }
}
