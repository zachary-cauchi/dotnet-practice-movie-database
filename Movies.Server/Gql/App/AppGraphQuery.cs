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
		public AppGraphQuery(ISampleGrainClient sampleClient, MoviesService moviesService)
		{
			Name = "AppQueries";

			Field<SampleDataGraphType>("sample",
				arguments: new QueryArguments(new QueryArgument<StringGraphType>
				{
					Name = "id"
				}),
				resolve: ctx => sampleClient.Get(ctx.Arguments["id"].ToString())
			);

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
