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
			[FromBody] Movie movie
		) => await _client.Set
			(
				id,
				movie.Key,
				movie.Name,
				movie.Description,
				movie.Genres,
				movie.Rate,
				movie.Length,
				movie.Img
			).ConfigureAwait(false);
	}
}
