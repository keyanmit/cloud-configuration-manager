using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ZooManServerTools.PersistanceManager;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAzureLease();
        }

        static void TestAzureLease()
        {
            AzureInfraTestCases.TestLeaseRenewal();
        }
    }
    
    public static class AzureInfraTestCases
    {
    
        public static void TestLeaseRenewal()
        {
            var blobRef = AzureHelpers.CreateAndGetBlob("zoomantest");

            using (var x = new AzureLeaseManager(blobRef))
            {
                try
                {
                    using (var y = new AzureLeaseManager(blobRef))
                    { }
                }
                catch (StorageException se)
                {
                    //expected;
                }
                Thread.Sleep(60 * 1000);
            }
        }
    }

    public static class AzureHelpers
    {
        public static CloudBlockBlob CreateAndGetBlob(string containerName)
        {
            //var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=karthimudevaccount;AccountKey=N/M1paap0OeL4gNnpq5T1HiU7t+3m5VpEfGUgD80SeEVm0GsWHOHl10M6NxA4/5vMRjovc2f+jf21FNyvLTA+g==;");
            var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;");
            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference("testblob");
            var bytes = Encoding.UTF8.GetBytes("asdjfasdfjlkasjdfaksdjfasdfjasjdfadsjkjflksadf");
            blob.UploadFromByteArray(bytes, 0, bytes.Length);
            return blob;
        }
    }
}
