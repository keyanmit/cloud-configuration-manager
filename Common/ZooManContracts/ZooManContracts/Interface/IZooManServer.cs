using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZooManContracts.Constants;
using ZooManContracts.MetaData;

namespace ZooManContracts.Interface
{
    public interface IZooManServer
    {
        BaseZooManListHeader CreateNewConfigurationManager(string namespaceName, bool isSigned = false);

        BaseZooManListHeader AddNewConfigurationPage(object payload, NewConfigurationPageLocation location);

        bool DeleteConfigurationList(bool isSoftDelete = false);        
    }

    public interface IZooManKeyValueDictionaryServer : IZooManServer
    {
        void AddOrUpdateKeyValuePair(string key, string value);
        void DeleteKeyValuePair(string key);
        string GetValue(string key);
    }
}
