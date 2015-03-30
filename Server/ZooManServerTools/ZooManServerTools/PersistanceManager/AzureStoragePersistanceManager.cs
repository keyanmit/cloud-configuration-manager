using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ZooManServerTools.ConfigStore;

namespace ZooManServerTools.PersistanceManager
{
    public class AzureStoragePersistanceManager : IPersistanceManager
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public AzureStoragePersistanceManager(string constring)
        {
            storageAccount = CloudStorageAccount.Parse(constring);
            blobClient = storageAccount.CreateCloudBlobClient();

            container = blobClient.GetContainerReference(ZoomanConfigStore.AzureBlobContainerName);
            container.CreateIfNotExists(BlobContainerPublicAccessType.Off);//blob is private. can be accessed only wiht a SAC
        }

        public void createOrUpdateBlob(ref string blobUri, string data, string blobPrefix)
        {            
            try
            {
                var blobReference = GetBlobReference(blobUri, blobPrefix);

                using (var x = new AzureLeaseManager(blobReference))
                {
                    var bytes = Encoding.UTF8.GetBytes(data);
                    blobReference.UploadFromByteArray(bytes, 0, bytes.Length, new AccessCondition()
                    {
                        LeaseId = x.LeaseId
                    });

                    var sac = blobReference.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        Permissions = SharedAccessBlobPermissions.Read,
                        SharedAccessExpiryTime = DateTime.MaxValue,
                        SharedAccessStartTime = DateTime.Now.AddMinutes(-10)
                    });
                    blobUri = string.Format("{0}{1}", blobReference.Uri.ToString(), sac);   
                }                
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public void deleteBlob(string blobUri)
        {
            throw new NotImplementedException();
        }        

        private CloudBlockBlob GetBlobReference(string blobUri, string blobPrefix)
        {
            string fileName = "";

            if (string.IsNullOrEmpty(blobUri))
            {
                fileName = blobPrefix + DateTime.Now.ToString("yy-MM-dd-hh:mm:ss");
            }
            else
            {
                var uri = new Uri(blobUri);
                var x = uri.AbsolutePath;
                fileName = x.Split('/').Last();
            }

            return container.GetBlockBlobReference(fileName);            
        }
    }
}
