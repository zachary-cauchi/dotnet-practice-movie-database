using GraphQL.Types;
using Movies.Contracts.Movies;

namespace Movies.Server.Gql.Types
{
	public class MovieGraphType : ObjectGraphType<Movie>
	{
		public MovieGraphType()
		{
			Name = "Movie";
			Description = "A graph type representing a single movie";

			Field(x => x.Id, nullable: true).Description("Unique ID");
			Field(x => x.Key, nullable: true).Description("The slug/key of the movie");
			Field(x => x.Name, nullable: true).Description("Name of the movie.");
			Field(x => x.Description, nullable: true).Description("Description of the movie");
			Field(x => x.Genres, nullable: true).Description("List of genres");
			Field(x => x.Rate, nullable: true).Description("Movie rating");
			Field(x => x.Length, nullable: true).Description("Duration of the movie");
			Field(x => x.Img, nullable: true).Description("Filename of the movie banner");
		}
	}
}
