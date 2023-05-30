using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using SWRP.Pawns;

namespace SWRP.Pawns
{
	public class JumpState : BaseState
	{
		public JumpState(PawnStateMachine stateMachine, PawnController controller) : base(stateMachine, controller)
		{
		}

		public override void Enter()
		{
			//Log.Info("Entering Jump State");
		}

		public override void Exit()
		{
			//Log.Info("Exiting Jump State");
		}

		public override void Simulate()
		{
			DoJump(); 
			StateMachine.SwitchState(new GroundedState(StateMachine, Controller));
		}

		protected void DoJump()
		{
			Controller.Pawn.Velocity = ApplyJump(Controller.Pawn.Velocity, "jump");
		}

		protected Vector3 ApplyJump(Vector3 input, string jumpType)
		{
			Controller.AddEvent(jumpType);

			return input + Vector3.Up * JumpSpeed;
		}

	}
}
