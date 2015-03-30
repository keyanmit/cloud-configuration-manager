using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NatThread = System.Threading;

namespace ZooManServerTools.PersistanceManager
{
    public class AzureLeaseManager : IDisposable
    {
        private bool renewLease = true;
        private CloudBlockBlob blob;
        private System.Threading.Thread renewLeaseThread;
        
        private DateTime leaseStartTime; 

        private int leaseInterval = 20;

        public string LeaseId { get; private set; }

        public bool LeaseTaken
        {
            get
            {
                return LeaseId != null;
            }
        }

        public AzureLeaseManager(CloudBlockBlob renewableBlob)
        {
            blob = renewableBlob;
            
            if(!renewableBlob.Exists())
                renewableBlob.UploadText("place-holder-will-be-overwritten");
            LeaseId = blob.AcquireLease(TimeSpan.FromSeconds(leaseInterval), Guid.NewGuid().ToString());
            leaseStartTime = DateTime.Now;

            renewLeaseThread = new System.Threading.Thread(() =>
            {
                while (renewLease)
                {
                    NatThread.Thread.Sleep((int)(0.75*leaseInterval) * 1000);
                    if (renewLease && leaseStartTime.AddMinutes(10) > DateTime.Now)
                    {
                        blob.RenewLease(AccessCondition.GenerateLeaseCondition(LeaseId));
                        System.Diagnostics.Trace.WriteLine(string.Format("Renewing lease {0} : {1}",LeaseId,DateTime.Now));
                        Console.WriteLine((string.Format("Renewing lease {0} : {1}",LeaseId,DateTime.Now)));
                    }
                }
            });

            renewLeaseThread.Start();
        }

        ~AzureLeaseManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool shouldIDispose)
        {
            if (shouldIDispose)
            {
                renewLease = false;
                renewLeaseThread.Abort();
                blob.ReleaseLease(AccessCondition.GenerateLeaseCondition(LeaseId));
                renewLeaseThread = null;
            }
            else if (renewLeaseThread != null)
            {
                Dispose(true);
            }
        }
    }
}
