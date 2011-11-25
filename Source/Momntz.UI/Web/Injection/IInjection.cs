using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Momntz.UI.Web.Injection
{
    public interface IInjection
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();
    }
}