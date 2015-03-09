using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooManServerTools.PersistanceManager
{
    public interface IPersistanceManager
    {
        void updateBlob(string blobUri, string data);
        void deleteBlob(string blobUri);
        void lockBlob(string blobUri, out object lockObject);
        void releaseLock(string blobUri, object lockObject);
    }
    
}
