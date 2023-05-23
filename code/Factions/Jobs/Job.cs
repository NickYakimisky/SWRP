using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWRP.Factions.Jobs
{
	[Title( "Job" )]
	public partial class Job
	{
		public readonly string Identifier;
		public readonly string Name;
		public readonly string Description;

		public Job( string identifier, string name, string description )
		{
			Identifier = identifier;
			Name = name;
			Description = description;
		}
	}
}
