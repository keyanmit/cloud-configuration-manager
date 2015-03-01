using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZooManContracts.MetaData;

namespace ZooManContracts.Interface
{
    public interface IZooManBaseCSharpClient
    {
        //should handle verify signature if needed
        BaseZooManListHeader Connect(string zooManListHeaderUrl, int resyncIntervaInMinutes);
       
        void Disconnect();
    }

    public interface IZooManKeyValueDictionaryClient : IZooManBaseCSharpClient
    {
        string GetValue(string key);       
        void SetValue(string key, string value);
    }
}
