using Movies.Contracts.Movies;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Server.Services
{
	public class MoviesService
	{

		private readonly IMovieGrainClient _client;
		private readonly TableStorageService<TableMovie> _tableStorageService;

		public MoviesService
		(
			IMovieGrainClient client,
			TableStorageService<TableMovie> tableStorageService
		)
		{
			_client = client;
			_tableStorageService = tableStorageService;
		}

		public async Task<Movie> Get(string id)
		{
			var result = await _client.Get(id).ConfigureAwait(false);

			if (result?.Id == null)
			{
				TableMovie tableMovie = (await _tableStorageService.GetEntityAsync(TableMovie._partitionKey, id)).Value;

				return tableMovie;
			}

			return result;
		}

		public async Task Set(
			string id,
			Movie movie
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

		public async Task<bool> SetMany(List<Movie> movies)
		{

			//TODO: To improve performance for frequent runs, use a pool of task instances instead of recreating them each time.
			// See https://github.com/dotnet/orleans/blob/0748f6a0e7e8158b30b674d90ded06e68d8dce44/samples/Blazor/BlazorServer/Services/TodoService.cs
			List<Task> tasks = new List<Task>();

			// Fan out and add all the movie grains.
			foreach (var movie in movies)
			{
				tasks.Add(_client.Set
				(
					movie.Id,
					movie.Key,
					movie.Name,
					movie.Description,
					movie.Genres,
					movie.Rate,
					movie.Length,
					movie.Img
				));
			}

			return await Task.WhenAll(tasks).ContinueWith(res =>
			{
				foreach (var task in tasks)
					if (task.IsFaulted || task.IsCanceled)
						return false;
				return true;
			});
		}

	}
}
