using System;
using System.Collections.Generic;
using Sandbox;
using SWRP.Pawns;
using PawnController = SWRP.Pawns.PawnController;

namespace SWRP
{
	public class GroundedState : BaseState
	{
		public GroundedState(PawnStateMachine stateMachine, PawnController controller) : base(stateMachine, controller)
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
			Controller.ControllerEvents.Clear();

			var movement = Controller.Pawn.InputDirection.Normal;
			var angles = Controller.Pawn.ViewAngles.WithPitch(0);
			var moveVector = Rotation.From(angles) * movement * 320f;
			var ground = CheckForGround();

			if (ground.IsValid())
			{
				if (!Grounded)
				{
					Controller.Pawn.Velocity = Controller.Pawn.Velocity.WithZ(0);
					Controller.AddEvent("grounded");
				}

				Controller.Pawn.Velocity = Accelerate(Controller.Pawn.Velocity, moveVector.Normal, moveVector.Length, 200.0f * (Input.Down("run") ? 2.5f : 1f), 7.5f);
				Controller.Pawn.Velocity = ApplyFriction(Controller.Pawn.Velocity, 4.0f);
			}
			else
			{
				Controller.Pawn.Velocity = Accelerate(Controller.Pawn.Velocity, moveVector.Normal, moveVector.Length, 100, 20f);
				Controller.Pawn.Velocity += Vector3.Down * Gravity * Time.Delta;
			}

			var mh = new MoveHelper(Controller.Pawn.Position, Controller.Pawn.Velocity);
			mh.Trace = mh.Trace.Size(Controller.Pawn.Hull).Ignore(Controller.Pawn);

			if (mh.TryMoveWithStep(Time.Delta, StepSize) > 0)
			{
				if (Grounded)
				{
					mh.Position = StayOnGround(mh.Position);
				}
				Controller.Pawn.Position = mh.Position;
				Controller.Pawn.Velocity = mh.Velocity;
			}

			Controller.Pawn.GroundEntity = ground;

			if (Grounded && Input.Pressed("jump"))
			{
				StateMachine.SwitchState(new JumpState(StateMachine, Controller));
			}
		}
	}
}
