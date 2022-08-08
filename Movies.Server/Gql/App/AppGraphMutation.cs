using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Movies;
using Movies.Server.Gql.Types;
using Movies.Server.Services;
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
			MoviesService moviesService
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

					return moviesService.Set(movie.Id, movie);
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

					return moviesService.SetMany(movies);
				}
			);
		}
	}
}
