using System;
using System.Collections.Generic;
using Sandbox;
using SWRP.Pawns;
using PawnController = SWRP.Pawns.PawnController;

namespace SWRP
{
	public class GroundedState : BaseState
	{
		public GroundedState(PawnStateMachine stateMachine) : base(stateMachine)
		{
		}
		public override void Enter()
		{
			//Log.Info("Entering Grounded State");
		}

		public override void Exit()
		{
			//Log.Info("Exiting Grounded State");
		}

		public override void Simulate()
		{
			StateMachine.Controller.ControllerEvents.Clear();

			var movement = StateMachine.Controller.Pawn.InputDirection.Normal;
			var angles = StateMachine.Controller.Pawn.ViewAngles.WithPitch(0);
			var moveVector = Rotation.From(angles) * movement * 320f;
			var ground = CheckForGround();

			if (ground.IsValid())
			{
				StateMachine.JumpCount = 0;
				if (!StateMachine.Grounded)
				{
					StateMachine.Controller.Pawn.Velocity = StateMachine.Controller.Pawn.Velocity.WithZ(0);
					StateMachine.Controller.AddEvent("grounded");
				}

				StateMachine.Controller.Pawn.Velocity = Accelerate(StateMachine.Controller.Pawn.Velocity, moveVector.Normal, moveVector.Length, 200.0f * (Input.Down("run") ? 2.5f : 1f), 7.5f);
				StateMachine.Controller.Pawn.Velocity = ApplyFriction(StateMachine.Controller.Pawn.Velocity, 4.0f);
			}
			else
			{
				StateMachine.Controller.Pawn.Velocity = Accelerate(StateMachine.Controller.Pawn.Velocity, moveVector.Normal, moveVector.Length, 100, 20f);
				StateMachine.Controller.Pawn.Velocity += Vector3.Down * StateMachine.Gravity * Time.Delta;
			}

			var mh = new MoveHelper(StateMachine.Controller.Pawn.Position, StateMachine.Controller.Pawn.Velocity);
			mh.Trace = mh.Trace.Size(StateMachine.Controller.Pawn.Hull).Ignore(StateMachine.Controller.Pawn);

			if (mh.TryMoveWithStep(Time.Delta, StateMachine.StepSize) > 0)
			{
				if (StateMachine.Grounded)
				{
					mh.Position = StayOnGround(mh.Position);
				}
				StateMachine.Controller.Pawn.Position = mh.Position;
				StateMachine.Controller.Pawn.Velocity = mh.Velocity;
			}

			StateMachine.Controller.Pawn.GroundEntity = ground;

			if (Input.Pressed("jump"))
			{
				StateMachine.SwitchState(new JumpState(StateMachine));
			}
		}
	}
}
