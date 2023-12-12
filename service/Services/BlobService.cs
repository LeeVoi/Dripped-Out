using Azure.Storage;
using Azure.Storage.Blobs;

namespace service.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _client;
        private readonly string blobconnectionstring = Environment.GetEnvironmentVariable("blobconnectionstring");

        public BlobService()
        {
            _client = new BlobServiceClient(blobconnectionstring);
        }

        public static void GetBlobServiceClient(ref BlobServiceClient blobServiceClient, string accountName,
            string accountKey)
        {
            StorageSharedKeyCredential sharedKeyCredential =
                new StorageSharedKeyCredential(accountName, accountKey);
            string blobUri = "https://" + accountName + ".blob.core.windows.net";

            blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);
        }
        
    }
}