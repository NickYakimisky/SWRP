using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWRP.Pawns
{
	// Context
	public class PawnStateMachine
	{
		private BaseState _currentState;
		public PawnController Controller;

		#region BaseState Variables
		public int JumpCount = 0;
		public int StepSize => 24;
		public int GroundAngle => 45;
		public int JumpSpeed => 300;
		public float Gravity => 800f;
		public bool Grounded => Controller.Pawn.GroundEntity.IsValid();
		#endregion


		public PawnStateMachine(PawnController controller)
		{
			Controller = controller;
		}
		public void SwitchState(BaseState newState)
		{
			_currentState?.Exit(); // Exit the old state
			_currentState = newState; // Set the new state
			_currentState?.Enter(); // Enter the new state
		}

		public void Simulate(IClient cl)
		{
			_currentState?.Simulate();
		}
	}
}
