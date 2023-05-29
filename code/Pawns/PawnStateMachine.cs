using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWRP.Pawns
{
	public class PawnStateMachine
	{
		private BaseState _currentState;
		protected PawnController _controller;

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
