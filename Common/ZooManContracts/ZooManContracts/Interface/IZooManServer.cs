using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZooManContracts.Constants;
using ZooManContracts.MetaData;

namespace ZooManContracts.Interface
{
    interface IZooManServer
    {
        BaseZooManListHeader CreateNewConfigurationManager(string namespaceName);

        BaseZooManListHeader AddNewConfigurationPage(object payload, NewConfigurationPageLocation location);

        bool DeleteConfigurationList(bool isSoftDelete = false);        
    }

    interface IZooManKeyValueDictionaryServer : IZooManServer
    {
        void AddOrUpdateKeyValuePair(string key, string value);
        void DeleteKeyValuePair(string key);
        string GetValue(string key);
    }
}
