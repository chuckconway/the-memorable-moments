using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Momntz.UI.Web.Injection
{
    public class StructureMapIoc : IInjection
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return IoC.Container.GetInstance<T>();
        }
    }
}