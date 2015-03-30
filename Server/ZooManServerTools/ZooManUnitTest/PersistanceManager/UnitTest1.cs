using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZooManContracts.MetaData;
using ZooManServerTools.PersistanceManager;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;

namespace ZooManUnitTest.PersistanceManager
{
    [TestClass]
    public class AzureStoragePersistanceMgrTestCases
    {
        [TestMethod]
        public void TestBlobUpload()
        {
            var azureSPM = new AzureStoragePersistanceManager("UseDevelopmentStorage=true;");
            var url = string.Empty;
            azureSPM.createOrUpdateBlob(ref url,File.ReadAllText(@"D:\code\smb-workspace.xml"),"keyan");

            Assert.AreEqual(true, InternalAzureVerify(url, File.ReadAllText(@"D:\code\smb-workspace.xml")));
        }

        private bool InternalAzureVerify(string url, string fileAsText)
        {
            using (var x = new WebClient())
            {
                var data = x.DownloadString(url);
                return data.CompareTo(fileAsText)==0;
            }

            return false;
        }
    }

    [TestClass]
    public class AzureInfraTestCases
    {
        [TestMethod]
        public void TestLeaseRenewal()
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
            }
        }
    }

    [TestClass]
    public class ZooManPersistanceCases
    {
        [TestMethod]
        public void TestZooManPersistanceMgrCreateNewConfig()
        {
            var persistanceMgr =
                new BaseZooManPersistanceManager(new AzureStoragePersistanceManager("UseDevelopmentStorage=true;"));

            var head = new BaseZooManListHeader()
            {
                ConfigurationPage = string.Empty,
                Ticket = new ZooManTicket()
                {
                    IsSignatureEnabled = false,
                    Namespace = Guid.NewGuid(),
                    NamespaceFriendlyName = "keyantest",
                    Version = "1.0"
                }
            };

            head.Location = persistanceMgr.InitializeHeader(head);
            

            var sampleconfig = new BaseZooManConfigurationPage()
            {
                ConfigurationPageChildren = new List<string>(),
                Location = string.Empty,
                Payload = "hta",
                PayloadSignature = string.Empty,
                ZooManListHeaderUrl = head.Location
            };

            sampleconfig.Location = persistanceMgr.PrependNewConfiguration(head, sampleconfig);
        }
    }

    public static class AzureHelpers
    {
        public static CloudBlockBlob CreateAndGetBlob(string containerName)
        {
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
