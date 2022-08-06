using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Contracts.Movies
{
	public interface IMovieGrainClient
	{
		Task<Movie> Get(string key);

		Task Set(string id, string key, string name, string description, List<string> genres, string rate, string length, string img);
	}
}
