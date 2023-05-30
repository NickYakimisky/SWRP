using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWRP.Pawns
{
	public enum States {
		Grounded,
		Jump
	}
	public class PawnStateFactory
	{

		Dictionary<Enum, BaseState> _states = new Dictionary<Enum, BaseState>();

		public PawnStateFactory(PawnStateMachine stateMachine)
		{
			_states[States.Grounded] = new GroundedState(stateMachine, this);
			_states[States.Jump] = new JumpState(stateMachine, this);
		}

		public BaseState GetState(Enum state)
		{
			return _states[state];
		}
	}
}
