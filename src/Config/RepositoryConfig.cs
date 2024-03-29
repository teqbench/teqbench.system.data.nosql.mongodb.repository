﻿namespace TeqBench.System.Data.NoSql.MongoDB.Repository.Config
{
    /// <summary>
    /// MongoDb repository config implementation.
    /// </summary>
    /// <seealso cref="TeqBench.System.Data.NoSql.MongoDB.Repository.Config.IRepositoryConfig" />
    public class RepositoryConfig : IRepositoryConfig
    {
        /// <summary>
        /// Gets or sets the connection string for the repository configuration.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the database repository configuration.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public required string DatabaseName { get; set; }
    }
}
