using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Contracts.Movies
{
	public class TableMovie : ITableEntity
	{
		public static readonly string _partitionKey = "movies";

		public string Key { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }

		public string Genres { get; set; }
		
		public string Rate { get; set; }
		
		public string Length { get; set; }

		public string Img { get; set; }
		
		public string PartitionKey { get; set; } = _partitionKey;
		
		public string RowKey { get; set; }
		
		public DateTimeOffset? Timestamp { get; set; }

		public ETag ETag { get; set; }

		public static implicit operator Movie(TableMovie tableMovie) =>
			new Movie()
			{
				Id = tableMovie.RowKey,
				Key = tableMovie.Key,
				Name = tableMovie.Name,
				Description = tableMovie.Description,
				Genres = JsonConvert.DeserializeObject<IList<string>>(tableMovie.Genres),
				Rate = tableMovie.Rate,
				Length = tableMovie.Length,
				Img = tableMovie.Img
			};
	}
}
