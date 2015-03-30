using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SigningAndEncryption;
using SigningAndEncryption.SigningAndExcryption;
using ZooManContracts.Constants;
using ZooManContracts.Interface;
using ZooManContracts.MetaData;

namespace ZooManServerTools.KeyvalueStore
{
    public class KeyValueStoreServerManager<T> : IZooManKeyValueDictionaryServer
    {
        private ISigningAuthority signingAuthority;
        private X509Certificate2 signingCert;
        private BaseZooManListHeader head;

        public KeyValueStoreServerManager()
        {
            signingAuthority = new RSASignatureAuthority();
        }

        public BaseZooManListHeader CreateNewConfigurationManager(string namespaceName, bool isSigned = false)
        {
            return head = new BaseZooManListHeader()
            {
                Ticket = new ZooManTicket()
                {
                    IsSignatureEnabled = isSigned,
                    Namespace = Guid.NewGuid(),
                    NamespaceFriendlyName = namespaceName,
                    Version = "3-1-2015"
                },
                ConfigurationPage = string.Empty
            };            
        }
        

        public BaseZooManListHeader AddNewConfigurationPage(object payload, NewConfigurationPageLocation location)
        {
            if(payload.GetType() != typeof(T))
                throw new ArgumentException();

            var newPage = new BaseZooManConfigurationPage()
            {
                ZooManListHeaderUrl = head.Location,
                Payload = JsonConvert.SerializeObject((T) payload),
                ConfigurationPageChildren = new List<string>(),
                PayloadSignature =
                    head.Ticket.IsSignatureEnabled
                        ? signingAuthority.GetBase64EncodedSignedHashForPayload(payload, signingCert)
                        : string.Empty
            };
            
            return null;            
        }

        public bool DeleteConfigurationList(bool isSoftDelete = false)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdateKeyValuePair(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void DeleteKeyValuePair(string key)
        {
            throw new NotImplementedException();
        }

        public string GetValue(string key)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetValueBatch(string[] keys)
        {
            throw new NotImplementedException();
        }

        public void SetValuesBatch(Dictionary<string, string> keyValuePairs)
        {
            throw new NotImplementedException();
        }
    }
}
