using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeqBench.System.Data.NoSql.MongoDB.Models;
using TeqBench.System.Data.NoSql.Repository;

namespace TeqBench.System.Data.NoSql.MongoDB.Repository
{
    /// <summary>
    /// MongoDB respository operations interface.
    /// </summary>
    /// <typeparam name="TDocument">The type of the document the respository will operate on.</typeparam>
    /// <seealso cref="TeqBench.System.Data.NoSql.Repository.IRepository" />
    public interface IRepository<TDocument> : IRepository where TDocument : IDocument
    {
        /// <summary>
        /// Gets a collection as queryable source of documents.
        /// </summary>
        /// <returns>A queryable source of documents.</returns>
        IQueryable<TDocument> AsQueryable();

        /// <summary>
        /// Finds documents by expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up documents.</param>
        /// <returns>A list of documents.</returns>
        IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Finds documents by expression asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up documents.</param>
        /// <returns>A list of documents.</returns>
        Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Finds document by expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up a document.</param>
        /// <returns>If found, a document, otherwise null.</returns>
        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Finds document by expression asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to look up a document.</param>
        /// <returns>If found, a document, otherwise null.</returns>
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Finds a documenty by document ID.
        /// </summary>
        /// <param name="id">The document ID to look up a document.</param>
        /// <returns>If found, a document, otherwise null.</returns>
        TDocument FindById(string id);

        /// <summary>
        /// Finds a documenty by document ID asynchronously.
        /// </summary>
        /// <param name="id">The document ID to look up a document.</param>
        /// <returns>If found, a document, otherwise null.</returns>
        Task<TDocument> FindByIdAsync(string id);

        /// <summary>
        /// Inserts one document.
        /// </summary>
        /// <param name="document">The document to be inserted.</param>
        void InsertOne(TDocument document);

        /// <summary>
        /// Inserts one document asynchronously.
        /// </summary>
        /// <param name="document">The document to be inserted.</param>
        /// <returns>The result of the insert operation.</returns>
        Task InsertOneAsync(TDocument document);

        /// <summary>
        /// Inserts many documents.
        /// </summary>
        /// <param name="documents">The documents to be inserted.</param>
        void InsertMany(ICollection<TDocument> documents);

        /// <summary>
        /// Inserts many documents asynchronously.
        /// </summary>
        /// <param name="documents">The documents to be inserted.</param>
        /// <returns>The result of the insert operation.</returns>
        Task InsertManyAsync(ICollection<TDocument> documents);

        /// <summary>
        /// Replaces one document.
        /// </summary>
        /// <param name="document">The document to be replaced.</param>
        void ReplaceOne(TDocument document);

        /// <summary>
        /// Replaces one document asynchronously.
        /// </summary>
        /// <param name="document">The document to be replaced.</param>
        /// <returns>The returned document.</returns>
        Task ReplaceOneAsync(TDocument document);

        /// <summary>
        /// Replaces many documents asynchronously.
        /// </summary>
        /// <param name="documents">The documents to be replaced.</param>
        /// <returns>The result of the writing.</returns>
        Task ReplaceManyAsync(IEnumerable<TDocument> documents);

        /// <summary>
        /// Deletes one document.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deletes one document asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        /// <returns>The deleted document if one was deleted.</returns>
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deletes a document by document's ID
        /// </summary>
        /// <param name="id">The document's ID used to determine which document to delete.</param>
        void DeleteById(string id);

        /// <summary>
        /// Deletes a document by document's ID asynchronously.
        /// </summary>
        /// <param name="id">The document's ID used to determine which document to delete.</param>
        /// <returns>The deleted document if one was deleted.</returns>
        Task DeleteByIdAsync(string id);

        /// <summary>
        /// Deletes the many documents.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Deletes the many documents asynchronously.
        /// </summary>
        /// <param name="filterExpression">The filter expression to determine which documents to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}
