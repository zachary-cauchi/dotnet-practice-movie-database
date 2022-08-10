using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Contracts.Movies
{
	public class Movie
	{

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "genres")]
		public IList<string> Genres { get; set; }

		[JsonProperty(PropertyName = "rate")]
		public string Rate { get; set; }

		[JsonProperty(PropertyName = "length")]
		public string Length { get; set; }

		[JsonProperty(PropertyName = "img")]
		public string Img { get; set; }

		public static implicit operator TableMovie(Movie movie) =>
			new TableMovie()
			{
				RowKey = movie.Id,
				Key = movie.Key,
				Name = movie.Name,
				Description = movie.Description,
				Genres = JsonConvert.SerializeObject(movie.Genres),
				Rate = movie.Rate,
				Length = movie.Length,
				Img = movie.Img
			};
	}
}
