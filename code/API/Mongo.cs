using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.Diagnostics;

namespace SWRP.API
{
	internal partial class Mongo
	{
		private static string Endpoint => "https://us-west-2.aws.data.mongodb-api.com/app/data-piccg/endpoint/data/v1";
		private static readonly Dictionary<string, string> Headers = new()
		{
			{"Accept", "parameters/json"},
			{"api-key", "1dwyfSMjAjILzWnGt6gZHnKXpsbzA7Ase0h9ppK0S2W7G3nz4SU8So8Vi0smMLop"} // This is a public API key i.e. ReadOnly
		};

		public static async Task<T> PostAsync<T>(string api, HttpContent content)
		{
			try
			{
				return await Sandbox.Http.RequestJsonAsync<T>( Endpoint + api, "POST", content, Headers );
			}
			catch ( Exception ex )
			{
				Log.Error( $" [POST ERROR] on {api} | " + ex );
				return default;
			}
		}
	}

}
