using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using Sandbox;
using System.Threading.Tasks;
using Sandbox.Internal;
using SWRP.API.Requests;
using SWRP.API.Users;
using SWRP.Characters;
using SWRP.Users;

namespace SWRP.API
{
	internal partial class Mongo
	{
		public static async Task<List<Character>> GetCharacters( int[] cids )
		{
			var requestBody = new Request( "swrp0", "SWRP", "Characters", $"\"id\": {{\"$in\": [{string.Join(",", cids)}]}}" ).GetContent();
			var response = await PostAsync<JsonObject>( "/action/find", requestBody );
			var responseString = response["Documents"]?.ToJsonString();
			if ( responseString == "[]")
			{
				Log.Info(response);
				return new List<Character>();
			}

			try
			{
				var characters = Json.Deserialize<List<Character>>( responseString );
				return characters;
			}
			catch ( Exception ex )
			{
				Log.Error(ex);
				return new List<Character>();
			}
		}
		public static async Task<Record> GetUser( IClient client )
		{
			var requestBody = new Request( "swrp0", "SWRP", "Users", $"\"steam_id\": {{\"$numberLong\": \"{client.SteamId}\"}}" ).GetContent();
			var response = await PostAsync<JsonObject>( "/action/findOne", requestBody );
			if ( response["Document"] == null )
			{
				return new Record();
			}

			var record = Json.Deserialize<Record>( response["document"]?.ToJsonString() );
			return record;
		}
	}

}
