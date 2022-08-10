using GraphQL;
using GraphQL.Types;
using Movies.Contracts;
using Movies.Contracts.Movies;
using Movies.Server.Gql.Types;
using Movies.Server.Services;

namespace Movies.Server.Gql.App
{
	public class AppGraphQuery : ObjectGraphType
	{
		public AppGraphQuery(MoviesService moviesService)
		{
			Name = "AppQueries";

			Field<MovieGraphType>("movie",
				arguments: new QueryArguments(new QueryArgument<StringGraphType>
				{
					Name = "id"
				}),
				resolve: ctx => moviesService.Get(ctx.GetArgument<string>("id"))
			);
		}
	}
}
