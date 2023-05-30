using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWRP.Pawns
{
	public class PawnStateFactory
	{
		private Dictionary<string, BaseState> _states = new Dictionary<string, BaseState>();

		public PawnStateFactory(PawnStateMachine stateMachine)
		{
			_states.Add("Grounded", new GroundedState(stateMachine));
			_states.Add("Jump", new JumpState(stateMachine));
		}

		public BaseState GetState(string name)
		{
			return _states[name];
		}
	}
}
