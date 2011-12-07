using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace Momntz.Infrastructure.Data.Command
{
    public class Raven : IDocumentDatabase
    {
        private readonly IDocumentStore _store;
        private readonly IDocumentSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="Raven"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public Raven(IDocumentStore store)
        {
            _store = store;
            _session = _store.OpenSession();
        }

        /// <summary>
        /// Singles the or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T SingleOrDefault<T>(Func<T, bool> predicate) where T : class
        {
            return _session.Query<T>().SingleOrDefault(predicate);                        
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> All<T>() where T : class
        {
            return _session.Query<T>();
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public void Add<T>(T item) where T : class
        {
            _session.Store(item);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public void Delete<T>(T item) where T : class
        {
            _session.Delete(item);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _session.SaveChanges();
        }
    }
}
