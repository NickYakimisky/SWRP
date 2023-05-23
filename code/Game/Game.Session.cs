using Sandbox;
using SWRP.API;

namespace SWRP
{
	public partial class Game : GameManager
	{
		private static long nick = 76561198147524302;

		public override bool ShouldConnect( long steamId )
		{
			return (steamId == nick);
		}

		public static void KickPlayer( IClient client )
		{
			client.Kick();
			Log.Info( $"Player with ID {client.SteamId} was kicked from the server." );
		}
	}
}
