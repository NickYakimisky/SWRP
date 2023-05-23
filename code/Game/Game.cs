using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sandbox;
using SWRP.API;
using SWRP.API.Users;
using SWRP.Characters;
using SWRP.Users;

namespace SWRP;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
[Title( "SWRP Gamemode" )]
public partial class Game : GameManager

{
	public Dictionary<long, User> PlayerList = new();
	public Game()
	{
		if ( Sandbox.Game.IsClient )
		{
			_ = new HUD();
			if ( Sandbox.Game.IsRunningInVR )
			{

			}
			else
			{

			}
		}

		if ( Sandbox.Game.IsServer )
		{
		
		}
	}

	/// <summary>
	/// A client has joined the server. Load their data.
	/// </summary>
	/// <param name="ev">Client event</param>
	[GameEvent.Server.ClientJoined]
	public async void ClientJoinedAsync( ClientJoinedEvent ev )
	{
		Sandbox.Game.AssertServer();
		var record = await Mongo.GetUser( ev.Client );
		PlayerList.Add( ev.Client.SteamId, new User( ev.Client ) );
		if ( record.banned )
		{
			ev.Client.Kick();
		}

		PlayerList[ev.Client.SteamId].Record = record;
		PlayerList[ev.Client.SteamId].Characters = await Mongo.GetCharacters( PlayerList[ev.Client.SteamId].Record.characters );
		ClientJoined( ev.Client );
	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with.
	/// </summary>
	public new void ClientJoined( IClient client )
	{
		base.ClientJoined( client );
		var user = PlayerList[client.SteamId];
		user.CreatePawn();
		user.Pawn.Respawn();
		user.Pawn.DressFromClient( client );
		// Get all of the spawnpoints
		var spawnpoints = All.OfType<SpawnPoint>();

		// chose a random one
		var randomSpawnPoint = spawnpoints.MinBy( x => Guid.NewGuid() );

		// if it exists, place the pawn there
		if ( randomSpawnPoint != null )
		{
			var tx = randomSpawnPoint.Transform;
			tx.Position = tx.Position + Vector3.Up * 50.0f; // raise it up
			user.Pawn.Transform = tx;
		}
	}

	public override void ClientDisconnect( IClient cl, NetworkDisconnectionReason reason )
	{
		PlayerList.Remove( Client.SteamId );
	}
}
