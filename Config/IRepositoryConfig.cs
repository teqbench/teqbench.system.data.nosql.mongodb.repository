namespace TeqBench.System.Data.NoSql.MongoDB.Repository.Config
{
    /// <summary>
    /// MongoDb repository config interface.
    /// </summary>
    public interface IRepositoryConfig
    {
        /// <summary>
        /// Gets or sets the connection string for the repository configuration.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the database repository configuration.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        string DatabaseName { get; set; }
    }
}
