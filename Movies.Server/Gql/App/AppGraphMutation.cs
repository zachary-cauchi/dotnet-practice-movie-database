using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Movies;
using Movies.Server.Gql.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Server.Gql.App
{
	public class AppGraphMutation : ObjectGraphType
	{
		public AppGraphMutation
		(
			IMovieGrainClient movieClient
		)
		{
			Name = "AppMutations";

			Field<MovieGraphType>("addMovie",
				arguments: new QueryArguments
				(
					new QueryArgument<NonNullGraphType<MovieInputGraphType>>
					{
						Name = "movie",
						Description = "The movie to add."
					}
				),
				resolve: ctx =>
				{
					Movie movie = ctx.GetArgument<Movie>("movie");

					return movieClient.Set(
						movie.Id,
						movie.Key,
						movie.Name,
						movie.Description,
						movie.Genres,
						movie.Rate,
						movie.Length,
						movie.Img
					);
				}
			);

			Field<BooleanGraphType>("addMovies",
				arguments: new QueryArguments
				(
					new QueryArgument<NonNullGraphType<ListGraphType<MovieInputGraphType>>>
					{
						Name = "movies",
						Description = "The movies to add"
					}
				),
				resolve: ctx =>
				{
					List<Movie> movies = ctx.GetArgument<List<Movie>>("movies");
					List<Task> tasks = new();

					foreach (var movie in movies)
					{
						tasks.Add(movieClient.Set(
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

					return Task.WhenAll(tasks).ContinueWith(res =>
					{
						// Checking for errors so that if any are found, the check short circuits.
						return !tasks.Any(task => task.IsFaulted);
					});
				}
			);
		}
	}
}