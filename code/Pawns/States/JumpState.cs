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

		public JumpState(PawnStateMachine stateMachine) : base(stateMachine)
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
			StateMachine.SwitchState(new GroundedState(StateMachine));
		}

		protected void DoJump()
		{
			if (StateMachine.JumpCount < 2)
			{
				StateMachine.Controller.Pawn.Velocity = ApplyJump(StateMachine.Controller.Pawn.Velocity, "jump");
				StateMachine.JumpCount++;
			}
		}

		protected Vector3 ApplyJump(Vector3 input, string jumpType)
		{
			StateMachine.Controller.AddEvent(jumpType);

			return input + Vector3.Up * StateMachine.JumpSpeed;
		}

	}
}
