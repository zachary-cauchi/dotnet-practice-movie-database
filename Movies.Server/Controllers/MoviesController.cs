using Microsoft.AspNetCore.Mvc;
using Movies.Contracts.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Server.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : Controller
	{
		private readonly IMovieGrainClient _client;

		public MoviesController(
			IMovieGrainClient client
		)
		{
			_client = client;
		}

		[HttpGet("{id}")]
		public async Task<Movie> Get([FromRoute] string id)
		{
			var result = await _client.Get(id).ConfigureAwait(false);
			return result;
		}

		[HttpPost("{id}")]
		public async Task Set(
			[FromRoute] string id,
			[FromForm] string key,
			[FromForm] string name,
			[FromForm] string description,
			[FromForm] string genres,
			[FromForm] string rate,
			[FromForm] string length,
			[FromForm] string img
		)
		{
			List<string> separateGenres = new();

			separateGenres.AddRange(genres.Split(','));

			await _client.Set(id, key, name, description, separateGenres, rate, length, img).ConfigureAwait(false);
		}
	}
}
