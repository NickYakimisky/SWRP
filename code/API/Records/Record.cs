using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SWRP.API.Users
{
	public class Record
	{
		public static string id { get; set; }
		public long steam_id { get; set; }
		public string username { get; set; }
		public bool banned { get; set; }
		public int[] characters { get; set; }
	}
}
