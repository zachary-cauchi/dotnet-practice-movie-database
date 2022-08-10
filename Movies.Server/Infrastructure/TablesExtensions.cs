using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Contracts.Movies;
using Movies.Server.Services;

namespace Movies.Server.Infrastructure
{
	public static class TablesExtensions
	{
		public static IServiceCollection ConfigureTableServices(this IServiceCollection services, IConfiguration configuration)
		{
			string connectionString = configuration.GetValue<string>("cosmosDb:connectionString");

			services.AddSingleton<TableServiceClient>(c =>
				new TableServiceClient(connectionString));
			services.AddSingleton<TableStorageService<TableMovie>>(c =>
				new TableStorageService<TableMovie>(c.GetRequiredService<TableServiceClient>(), "MoviesStore")
			);

			return services;
		}
	}
}
