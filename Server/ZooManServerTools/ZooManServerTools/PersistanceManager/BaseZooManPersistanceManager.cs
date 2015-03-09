using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZooManContracts.MetaData;
using ZooManServerTools.Interface;

namespace ZooManServerTools.PersistanceManager
{
    public class BaseZooManPersistanceManager : IZooManPersistanceManager
    {
        private IPersistanceManager persistanceManager;

        public BaseZooManPersistanceManager(IPersistanceManager manager)
        {
            persistanceManager = manager;
        }

        public void PrependNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            object headLock;
            persistanceManager.lockBlob(header.Location, out headLock);
            var headNxt = header.ConfigurationPage;
            header.ConfigurationPage = page.Location;
            page.ConfigurationPageChildren.Add(headNxt);
            persistanceManager.releaseLock(header.Location, headLock);
            
        }

        public void AppendNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            throw new NotImplementedException();
        }

        public void UpdateConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            object pageLock; 
            persistanceManager.lockBlob(page.Location, out pageLock);
            persistanceManager.updateBlob(page.Location, JsonConvert.SerializeObject(page));
            persistanceManager.releaseLock(page.Location, pageLock);
        }

        public void DeleteConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page)
        {
            throw new NotImplementedException();
        }
    }
}
