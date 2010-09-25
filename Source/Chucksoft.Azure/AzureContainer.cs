using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Chucksoft.Azure.Model.Blob;
using Chucksoft.Azure.Model.Container;

namespace Chucksoft.Azure
{
    public class AzureContainer
    {
        public void CreateContainer(string name, string accountName, string accountKey)
        {
            string url = string.Format(@"http://{0}.blob.core.windows.net/{1}?restype=container",accountName, name);

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "PUT";
            webRequest.ContentLength = 0;

            NameValueCollection collection = new NameValueCollection();
           
            collection.Add("x-ms-version", "2009-09-19");
            collection.Add("x-ms-date", DateTime.UtcNow.ToString("R"));
            collection.Add("x-ms-blob-public-access", ""); 
            collection.Add("Authorization", string.Format("SharedKey {0}:{1}", accountName, accountKey));
           
            WebResponse webResponse = webRequest.GetResponse();
            webResponse.Close();

        }

        public void DeleteContainer()
        {
           
        }

        public ContainerMetadata GetContainerMetadata()
        {
            return new ContainerMetadata();
        }

        public ContainerProperties GetContainerProperties()
        {
            return new ContainerProperties();
        }

        public List<AzureBlob> ListBlobs()
        {
            return new List<AzureBlob>();
        }

        public void SetACLs()
        {
           
        }

        public void SetMetadata()
        {
           
        }

    }
}