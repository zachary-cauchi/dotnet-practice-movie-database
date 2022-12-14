using Microsoft.Extensions.DependencyInjection;
using Movies.Contracts;
using Movies.Contracts.Movies;

namespace Movies.GrainClients
{
	public static class GrainClientsServiceCollectionExtensions
	{
		public static void AddAppClients(this IServiceCollection services)
		{
			services.AddSingleton<IMovieGrainClient, MovieGrainClient>();
		}
	}
}