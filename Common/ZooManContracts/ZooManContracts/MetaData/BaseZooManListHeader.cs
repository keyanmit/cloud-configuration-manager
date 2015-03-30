using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZooManContracts.MetaData
{
    public class BaseZooManListHeader
    {
        public string Location { get; set; }

        public ZooManTicket Ticket { get; set; }
        public string ConfigurationPage { get; set; }
    }

    public class ZooManTicket
    {
        public Guid Namespace { get; set; }
        public string NamespaceFriendlyName { get; set; }
        public string Version { get; set; }
        public bool IsSignatureEnabled { get; set; }
    }

    abstract public class ZooManExtendibleConfigurationPage
    {
        public string ZooManListHeaderUrl { get; set; }
        public string PayloadSignature { get; set; }
        public List<string> ConfigurationPageChildren { get; set; }
        public string Location { get; set; }

        //extending class should define the configuration object type to be serializd.
    }
}
