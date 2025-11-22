using Godot;
using System;

namespace FSM.StateMachineBase;

/// <summary>
/// A contract for how a state can be coded.
/// </summary>
/// <typeparam name="TStateType">The enum type representing different states.</typeparam>
/// <typeparam name="TActor">The type of actor/controller this state operates on.</typeparam>
public abstract partial class State<TStateType, TActor> : Node where TStateType : Enum {
	public TStateType StateType { get; private set; }
	protected TActor Actor;
	protected State(TStateType type) => StateType = type;
	public virtual void EnterState(TActor actor) => Actor = actor;
	public virtual void ExitState() { }
	public virtual void ProcessState(double delta) { }
	public virtual void PhysicsProcessState(double delta) { }
}
