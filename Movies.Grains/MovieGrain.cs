

using Movies.Contracts.Movies;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MovieGrain : Grain<Movie>, IMovieGrain
	{
		public Task<Movie> Get()
			=> Task.FromResult(State);

		public Task Set(string key, string name, string description, List<string> genres, string rate, string length, string img)
		{
			State = new Movie()
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

			return Task.CompletedTask;
		}
	}
}
