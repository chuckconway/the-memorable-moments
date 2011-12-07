using System;
using System.Collections.Generic;

namespace Momntz.Infrastructure.Data.Command
{
    public interface IDocumentDatabase
    {
        /// <summary>
        /// Singles the or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        T SingleOrDefault<T>(Func<T, bool> predicate) where T : class;

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> All<T>() where T : class;

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        void Add<T>(T item) where T : class;

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        void Delete<T>(T item) where T : class;

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}