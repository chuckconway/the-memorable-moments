﻿using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ThePhotoProject.Infrastructure.Repositories
{
    public class Repository : RepositoryBase, IRepository
    {
        /// <summary>
        /// Gets the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<T> Get<T>(IQuery query) where T : class, new()
        {
            return _database.PopulateCollection(query.Procedure, query.Parameters, _database.AutoPopulate<T>);
        }

        /// <summary>
        /// Gets the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<T> Get<T>(IQuery query) where T : class, new()
        {
            return _database.PopulateCollection(query.Procedure, query.Parameters, _database.AutoPopulate<T>);
        }

        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        public void Execute(IQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            CheckIfCommandTextIsNull(query.Procedure);
            _database.NonQuery(query.Procedure, query.Parameters);
        }

        /// <summary>
        /// Checks if command text is null.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        private static void CheckIfCommandTextIsNull(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new Exception("Command Text is expected.");
            }
        }

        /// <summary>
        /// Executes the specified procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        public void Execute(string procedure)
        {
            CheckIfCommandTextIsNull(procedure);

            List<DbParameter> dbParameters = new List<DbParameter>();
            _database.NonQuery(procedure, dbParameters);
        }

        /// <summary>
        /// Executes the specified procedure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public void Execute<T>(string procedure, T parameters) where T : class
        {
            CheckIfCommandTextIsNull(procedure);

            Query query = new Query();
            List<DbParameter> dbParameters = query.GetParameters(parameters);
            _database.NonQuery(procedure, dbParameters);
        }
    }
}
