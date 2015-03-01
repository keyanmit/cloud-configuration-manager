using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SigningAndEncryption;
using ZooManContracts.Constants;
using ZooManContracts.Interface;
using ZooManContracts.MetaData;

namespace ZooManServerTools.KeyvalueStore
{
    public class KeyValueStoreServerManager : IZooManKeyValueDictionaryServer
    {
        private ISigningAuthority signingAuthority;

        public BaseZooManListHeader CreateNewConfigurationManager(string namespaceName, bool isSigned = false)
        {
            var x = new BaseZooManListHeader()
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

            throw new NotImplementedException();
        }
        

        public BaseZooManListHeader AddNewConfigurationPage(object payload, NewConfigurationPageLocation location)
        {
            throw new NotImplementedException();
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
    }
}
