﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace SWRP.Pawns
{
	public abstract class BaseState
	{
		protected PawnStateMachine StateMachine;
		protected PawnStateFactory StateFactory;

		public BaseState(PawnStateMachine stateMachine, PawnStateFactory stateFactory) // Set the StateMachine
		{
			StateMachine = stateMachine;
			StateFactory = stateFactory;
		}

		public abstract void Enter(); // What we do upon Entering a State
		public abstract void Exit(); // What we do upon Exiting a State
		public abstract void Simulate(); // What we do in the State

		protected Entity CheckForGround()
		{
			if (StateMachine.Controller.Pawn.Velocity.z > 100f)
				return null;

			var trace = StateMachine.Controller.Pawn.TraceBBox(StateMachine.Controller.Pawn.Position, StateMachine.Controller.Pawn.Position + Vector3.Down, 2f);

			if (!trace.Hit)
				return null;

			if (trace.Normal.Angle(Vector3.Up) > StateMachine.GroundAngle)
				return null;

			return trace.Entity;
		}

		protected Vector3 ApplyFriction(Vector3 input, float frictionAmount)
		{
			float StopSpeed = 100.0f;

			var speed = input.Length;
			if (speed < 0.1f) return input;

			// Bleed off some speed, but if we have less than the bleed
			// threshold, bleed the threshold amount.
			float control = (speed < StopSpeed) ? StopSpeed : speed;

			// Add the amount to the drop amount.
			var drop = control * Time.Delta * frictionAmount;

			// scale the velocity
			float newspeed = speed - drop;
			if (newspeed < 0) newspeed = 0;
			if (newspeed == speed) return input;

			newspeed /= speed;
			input *= newspeed;

			return input;
		}

		protected Vector3 Accelerate(Vector3 input, Vector3 wishdir, float wishspeed, float speedLimit, float acceleration)
		{
			if (speedLimit > 0 && wishspeed > speedLimit)
				wishspeed = speedLimit;

			var currentspeed = input.Dot(wishdir);
			var addspeed = wishspeed - currentspeed;

			if (addspeed <= 0)
				return input;

			var accelspeed = acceleration * Time.Delta * wishspeed;

			if (accelspeed > addspeed)
				accelspeed = addspeed;

			input += wishdir * accelspeed;

			return input;
		}

		protected Vector3 StayOnGround(Vector3 position)
		{
			var start = position + Vector3.Up * 2;
			var end = position + Vector3.Down * StateMachine.StepSize;

			// See how far up we can go without getting stuck
			var trace = StateMachine.Controller.Pawn.TraceBBox(position, start);
			start = trace.EndPosition;

			// Now trace down from a known safe position
			trace = StateMachine.Controller.Pawn.TraceBBox(start, end);

			if (trace.Fraction <= 0) return position;
			if (trace.Fraction >= 1) return position;
			if (trace.StartedSolid) return position;
			if (Vector3.GetAngle(Vector3.Up, trace.Normal) > StateMachine.GroundAngle) return position;

			return trace.EndPosition;
		}

	}
}