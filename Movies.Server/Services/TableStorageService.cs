using Azure;
using Azure.Data.Tables;
using Movies.Contracts.Movies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Server.Services
{

	// https://stackoverflow.com/questions/71472311/how-to-use-azure-data-tables-tableclient-with-dependency-injection-multiple-tabl
	/// <summary>
	/// Wraps around a table client and provides its functions for CRUD operations.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TableStorageService<T> : ITableStorageService<T> where T : class, ITableEntity, new()
	{
		private readonly TableClient _tableClient;

		public TableStorageService(TableServiceClient tableServiceClient, string tableName)
		{
			_tableClient = tableServiceClient.GetTableClient(tableName);
		}

		public AsyncPageable<List<T>> QueryAsync(Expression<Func<T, bool>> query) =>
			(AsyncPageable<List<T>>)_tableClient.QueryAsync(query).AsPages();

		public async Task<Response<T>> GetEntityAsync(string partitionKey, string rowKey) =>
			await _tableClient.GetEntityAsync<T>(partitionKey, rowKey);

		public async Task<Response> AddEntityAsync(T entity) =>
			await _tableClient.AddEntityAsync(entity);
	}
}
