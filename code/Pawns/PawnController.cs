using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWRP.Pawns;
/// <summary>
/// Singleton so as to only ever have one player pawn controller.
/// </summary>
public class PawnController : EntityComponent<Pawn>, ISingletonComponent
{
	private PawnStateMachine _stateMachine;
	public PawnStateFactory StateFactory { get; }
	public Pawn Pawn => Entity;

	public HashSet<string> ControllerEvents = new(StringComparer.OrdinalIgnoreCase);
	public PawnController()
	{
		_stateMachine= new PawnStateMachine(this);
		StateFactory = new PawnStateFactory(_stateMachine);
		_stateMachine.SwitchState(StateFactory.GetState("Grounded"));
	}
	public void Simulate(IClient cl)
	{
		_stateMachine?.Simulate(cl);
	}
	public bool HasEvent(string eventName)
	{
		return ControllerEvents.Contains(eventName);
	}

	public void AddEvent(string eventName)
	{
		if (HasEvent(eventName))
			return;

		ControllerEvents.Add(eventName);
	}
}
