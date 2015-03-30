using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZooManContracts.MetaData;
using ZooManServerTools.ConfigStore;
using ZooManServerTools.Interface;

namespace ZooManServerTools.PersistanceManager
{
    public class BaseZooManPersistanceManager : IZooManPersistanceManager
    {
        //private readonly string configurationPageName = "app_{0}_{1}";
        private readonly IPersistanceManager persistanceManager;

        public BaseZooManPersistanceManager(IPersistanceManager manager)
        {
            persistanceManager = manager;
        }

        public string InitializeHeader(BaseZooManListHeader header)
        {
            var headerUri = string.Empty;
            persistanceManager.createOrUpdateBlob(ref headerUri, JsonConvert.SerializeObject(header), ZoomanConfigStore.AppDomain);
            header.Location = headerUri;
            persistanceManager.createOrUpdateBlob(ref headerUri, JsonConvert.SerializeObject(header), ZoomanConfigStore.AppDomain);
            return headerUri;
        }

        public string PrependNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {            
            var headNxt = header.ConfigurationPage;
            var blobUri = page.Location;
            
            if(!string.IsNullOrEmpty(headNxt))
                page.ConfigurationPageChildren.Add(headNxt);
            
            persistanceManager.createOrUpdateBlob(ref blobUri, JsonConvert.SerializeObject(page), ZoomanConfigStore.AppDomain);
            
            header.ConfigurationPage = blobUri;
            var headerUri = header.Location;
            persistanceManager.createOrUpdateBlob(ref headerUri, JsonConvert.SerializeObject(header), ZoomanConfigStore.AppDomain);

            return blobUri;
        }

        public string AppendNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            throw new NotImplementedException();
        }

        public string UpdateConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            var blobUri = page.Location;
            persistanceManager.createOrUpdateBlob(ref blobUri, JsonConvert.SerializeObject(page), ZoomanConfigStore.AppDomain);
            return blobUri;
        }

        public string DeleteConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            throw new NotImplementedException();
        }
    }
}
