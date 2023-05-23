using System.Collections.Generic;
using Sandbox;
using SWRP.API.Users;
using SWRP.Characters;
using SWRP.Pawns;

namespace SWRP.Users
{
	[Title( "User" )]
	public partial class User
	{
		public IClient Client;
		public Record Record;
		public Pawn Pawn;
		public List<Character> Characters = new();

		public User( IClient client )
		{
			Client = client;
		}

		public void CreatePawn()
		{
			Pawn = new Pawn();
			Client.Pawn = Pawn;
		}
	}
}
