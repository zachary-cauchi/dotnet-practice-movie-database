using Microsoft.AspNetCore.Mvc;
using Movies.Contracts.Movies;
using Movies.Server.Services;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Server.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : Controller
	{
		private readonly MoviesService _moviesService;

		public MoviesController(
			MoviesService moviesService
		)
		{
			_moviesService = moviesService;
		}

		[HttpGet("{id}")]
		public async Task<Movie> Get([FromRoute] string id) =>
			await _moviesService.Get(id).ConfigureAwait(false);

		[HttpPost("{id}")]
		public async Task Set(
			[FromRoute] string id,
			[FromBody] Movie movie
		) =>
			await _moviesService.Set(id, movie);

		[HttpPost]
		public async Task SetMany([FromBody] List<Movie> movies) =>
			await _moviesService.SetMany(movies);
	}
}
