using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts.Movies
{
	public interface IMovieGrain : IGrainWithStringKey
	{
		Task<Movie> Get();

		Task Set(string key, string name, string description, IList<string> genres, string rate, string length, string img);
	}
}
