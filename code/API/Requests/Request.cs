using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWRP.API.Requests
{
	internal class Request
	{
		public string dataSource { get; set; }
		public string database { get; set; }
		public string collection { get; set; }
		public string filter { get; set; }

		private static readonly JsonSerializerOptions options = new()
		{
			PropertyNameCaseInsensitive = true,
			NumberHandling = JsonNumberHandling.AllowReadingFromString
		};
		public Request(string dataSource, string database, string collection, string filter )
		{
			this.dataSource = dataSource;
			this.database = database;
			this.collection = collection;
			this.filter = filter;
		}
		public StringContent GetContent()
		{
			return new StringContent( "{\"dataSource\": \""+ dataSource + "\",\"database\": \"" + database + "\",\"collection\": \""+ collection +"\",\"filter\": {"+ filter +"}}", null, "parameters/json" ); ;
		}
	}
}
