using System.Collections.Generic;

namespace Movies.Contracts.Movies
{
	public class Movie
	{
		public string Id { get; set; }
		
		public string Key { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }

		public List<string> Genres { get; set; }

		public string Rate { get; set; }

		public string Length { get; set; }

		public string Img { get; set; }
	}
}
