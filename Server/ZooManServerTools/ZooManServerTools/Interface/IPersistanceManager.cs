using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooManServerTools.PersistanceManager
{
    public interface IPersistanceManager
    {
        void createOrUpdateBlob(ref string blobUri, string data, string blobPrefix);
        void deleteBlob(string blobUri);       
    }
    
}
