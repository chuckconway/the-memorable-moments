using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Chucksoft.Azure
{
   public interface IAzureRequest
    {
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
       string Method{ get; set; }

       /// <summary>
       /// Gets or sets the URL.
       /// </summary>
       /// <value>The URL.</value>
       string Url { get; set; }

       /// <summary>
       /// Gets or sets the headers.
       /// </summary>
       /// <value>The headers.</value>
       NameValueCollection Headers { get; set; }
    }
}
