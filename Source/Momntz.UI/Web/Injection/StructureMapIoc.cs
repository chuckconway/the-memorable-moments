﻿using System.Web.Mvc;
using Momntz.Infrastructure;

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
            return DependencyResolver.Current.GetService<T>();
        }
    }
}