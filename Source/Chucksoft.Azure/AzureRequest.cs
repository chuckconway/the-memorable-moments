using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Chucksoft.Azure
{
   public class AzureRequest
   {
       public void SendRequest(IAzureRequest request)
       {
           WebRequest webRequest = WebRequest.Create(request.Url);
           webRequest.Method = request.Method;

           foreach (string collection in request.Headers)
           {
               webRequest.Headers.Add(collection);
           }

           WebResponse webResponse = webRequest.GetResponse();
          // webResponse.
       }
   }
}
