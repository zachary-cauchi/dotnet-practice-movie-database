

using Movies.Contracts.Movies;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	[StorageProvider(ProviderName = "moviesDatabase")]
	public class MovieGrain : Grain, IMovieGrain
	{

		private readonly IPersistentState<Movie> _movie;

		public MovieGrain(
			[PersistentState("movie", "moviesDatabase")] IPersistentState<Movie> movie
		)
		{
			_movie = movie;
		}

		public Task<Movie> Get()
			=> Task.FromResult(_movie.State);

		public async Task Set(string key, string name, string description, IList<string> genres, string rate, string length, string img)
		{
			_movie.State = new Movie()
			{
				Id = this.GetPrimaryKeyString(),
				Key = key,
				Name = name,
				Description = description,
				Genres = genres,
				Rate = rate,
				Length = length,
				Img = img
			};

			await _movie.WriteStateAsync();
		}

		public async Task Set(Movie movie)
		{
			_movie.State = movie;

			await _movie.WriteStateAsync();
		}
	}
}
