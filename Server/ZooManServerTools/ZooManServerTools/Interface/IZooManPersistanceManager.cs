using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooManContracts.MetaData;

namespace ZooManServerTools.Interface
{
    public interface IZooManPersistanceManager
    {
        void PrependNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        void AppendNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        void UpdateConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        void DeleteConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
    }
}
