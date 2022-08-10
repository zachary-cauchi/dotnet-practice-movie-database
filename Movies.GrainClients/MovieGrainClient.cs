using Movies.Contracts.Movies;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.GrainClients
{
	public class MovieGrainClient : IMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieGrainClient(
			IGrainFactory grainFactory
		)
		{
			_grainFactory = grainFactory;
		}

		public Task<Movie> Get(string id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			return grain.Get();
		}

		public Task Set(string id, string key, string name, string description, IList<string> genres, string rate, string length, string img)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);

			return grain.Set(key, name, description, genres, rate, length, img);
		}

		public Task Set(Movie movie)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(movie.Id);

			return grain.Set(movie);
		}
	}
}
