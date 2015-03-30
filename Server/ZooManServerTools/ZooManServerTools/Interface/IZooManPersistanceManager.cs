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
        string PrependNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        string AppendNewConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        string UpdateConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
        string DeleteConfiguration(BaseZooManListHeader header, BaseZooManConfigurationPage page);
    }
}
