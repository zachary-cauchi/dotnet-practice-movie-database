using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Contracts.Movies
{
	public interface IMovie
	{
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
	}
}
