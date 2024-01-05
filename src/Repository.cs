using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TeqBench.System.Data.NoSql.MongoDB.Models;
using TeqBench.System.Data.NoSql.MongoDB.Repository.Config;
using TeqBench.System.Data.NoSql.Repository;

namespace TeqBench.System.Data.NoSql.MongoDB.Repository
{
    /// <summary>
    /// MongoDB respository operations.
    /// </summary>
    /// <typeparam name="TDocument">The type of the document the respository will operate on.</typeparam>
    /// <seealso cref="TeqBench.System.Data.NoSql.Repository.BaseRepository" />
    /// <seealso cref="TeqBench.System.Data.NoSql.MongoDB.Repository.IRepository{TDocument}" />
    public class Repository<TDocument> : BaseRepository, IRepository<TDocument> where TDocument : IDocument
    {
        #region Member Variables
        private readonly IMongoCollection<TDocument> _collection; 
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TDocument}"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public Repository(IRepositoryConfig settings) : base()
        {
            try
            {
                MongoClient client = new MongoClient(settings.ConnectionString);
                IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

                string collectionName = ((BsonCollectionAttribute)typeof(TDocument).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
                _collection = database.GetCollection<TDocument>(collectionName);
            }
            catch (Exception ex)
            {
                // No fancy logging for now; just output to console.
                Console.WriteLine("Unexpected exception occurred while attempting to create repository for TeqBench.System.Data.NoSql.MongoDB.Repository type " + typeof(TDocument).ToString());
                Console.WriteLine("Exception details: " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Gets a collection as queryable source of documents.
        /// </summary>
        /// <returns>
        /// A queryable source of documents.
        /// </returns>
        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        /// <summary>
        /// Finds documents by expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up documents.</param>
        /// <returns>
        /// A list of documents.
        /// </returns>
        public virtual IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        /// <summary>
        /// Finds documents by expression asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up documents.</param>
        /// <returns>
        /// A list of documents.
        /// </returns>
        public virtual async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).ToListAsync();
        }

        /// <summary>
        /// Finds document by expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up a document.</param>
        /// <returns>
        /// If found, a document, otherwise null.
        /// </returns>
        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        /// <summary>
        /// Finds document by expression asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up a document.</param>
        /// <returns>
        /// If found, a document, otherwise null.
        /// </returns>
        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds a documenty by document ID.
        /// </summary>
        /// <param name="id">The document ID to look up a document.</param>
        /// <returns>
        /// If found, a document, otherwise null.
        /// </returns>
        public virtual TDocument FindById(string id)
        {
            //var objectId = new ObjectId(id);
            //var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        /// <summary>
        /// Finds a documenty by document ID asynchronously.
        /// </summary>
        /// <param name="id">The document ID to look up a document.</param>
        /// <returns>
        /// If found, a document, otherwise null.
        /// </returns>
        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Inserts one document.
        /// </summary>
        /// <param name="document">The document to be inserted.</param>
        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        /// <summary>
        /// Inserts one document asynchronously.
        /// </summary>
        /// <param name="document">The document to be inserted.</param>
        public virtual async Task InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        /// <summary>
        /// Inserts many documents.
        /// </summary>
        /// <param name="documents">The documents to be inserted.</param>
        public void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }

        /// <summary>
        /// Inserts many documents asynchronously.
        /// </summary>
        /// <param name="documents">The documents to be inserted.</param>
        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        /// <summary>
        /// Replaces one document.
        /// </summary>
        /// <param name="document">The document to be replaced.</param>
        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        /// <summary>
        /// Replaces one document asynchronously.
        /// </summary>
        /// <param name="document">The document to be replaced.</param>
        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        /// <summary>
        /// Replaces many documents asynchronously.
        /// </summary>
        /// <param name="documents">The documents to be replaced.</param>
        public virtual async Task ReplaceManyAsync(IEnumerable<TDocument> documents)
        {
            var updates = new List<WriteModel<TDocument>>();
            var filterBuilder = Builders<TDocument>.Filter;

            foreach (TDocument document in documents)
            {
                var filter = filterBuilder.Where(d => d.Id == document.Id);
                updates.Add(new ReplaceOneModel<TDocument>(filter, document));
            }

            await _collection.BulkWriteAsync(updates);
        }

        /// <summary>
        /// Deletes one document.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        /// <summary>
        /// Deletes one document asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await _collection.FindOneAndDeleteAsync(filterExpression);
        }

        /// <summary>
        /// Deletes a document by document's ID
        /// </summary>
        /// <param name="id">The document's ID used to determine which document to delete.</param>
        public void DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }

        /// <summary>
        /// Deletes a document by document's ID asynchronously.
        /// </summary>
        /// <param name="id">The document's ID used to determine which document to delete.</param>
        public async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        /// <summary>
        /// Deletes the many documents.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        /// <summary>
        /// Deletes the many documents asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await _collection.DeleteManyAsync(filterExpression);
        }
    }
}
