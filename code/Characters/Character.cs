using SWRP.Factions;
using SWRP.Factions.Jobs;

namespace SWRP.Characters
{
	public partial class Character
	{
		public int Id { get; set; }
		public string First { get; set; }
		public string Middle { get; set; }
		public string Last { get; set; }
		public string Faction { get; set; }
		public string Job { get; set; }
	}
}
