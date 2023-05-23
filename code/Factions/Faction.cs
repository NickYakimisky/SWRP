using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWRP.Factions.Jobs;

namespace SWRP.Factions
{
	[Title( "Faction" )]
	public partial class Faction
	{
		public readonly string Identifier;
		public readonly string Name;
		public readonly string Description;
		public IDictionary<string, Job> Jobs = new Dictionary<string, Job>();
		public Faction( string identifier, string name, string description )
		{
			Identifier = identifier;
			Name = name;
			Description = description;
		}
	}
}
