using Azure.Data.Tables;

namespace Movies.Server.Services
{
	public interface ITableStorageService<T> where T : class, ITableEntity, new()
	{
	}
}