using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts.Movies
{
	public interface IMovieGrainClient
	{
		Task<Movie> Get(string key);

		Task Set(string id, string key, string name, string description, IList<string> genres, string rate, string length, string img);

		Task Set(Movie movie);
	}
}
