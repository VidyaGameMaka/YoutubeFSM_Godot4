using Godot;
using System;
using System.Collections.Generic;

namespace FSM;

/// <summary>
/// Generic state machine that manages states and transitions.
/// </summary>
/// <typeparam name="TStateType">The enum type representing different states.</typeparam>
/// <typeparam name="TActor">The type of actor/controller this state machine operates on.</typeparam>
public partial class StateMachine<TStateType, TActor> : Node where TStateType : Enum {

	private readonly Dictionary<TStateType, State<TStateType, TActor>> _statesDict = new Dictionary<TStateType, State<TStateType, TActor>>();
	public State<TStateType, TActor> _currentState { get; private set; }

	// Godot Signal - StateChanged
	[Signal] public delegate void StateChangedEventHandler(int newState, int previousState);
	// Debugging helper
	public string GetCurrentStateName() => _currentState != null ? _currentState.StateType.ToString() : "<none>";

	public void AddState(State<TStateType, TActor> state) {
		if (!_statesDict.ContainsKey(state.StateType))
			_statesDict.Add(state.StateType, state);
	}

	public void Init(TStateType initialState, TActor actor) {
		if (!_statesDict.ContainsKey(initialState)) {
			GD.PrintErr($"StateMachine: State {initialState} not found, cannot init state.");
			return;
		}

		_currentState = _statesDict[initialState];
		_currentState.EnterState(actor);

		EmitSignal(SignalName.StateChanged, Convert.ToInt32(_currentState.StateType), Convert.ToInt32(_currentState.StateType));
	}

	public void ChangeState(TStateType newState, TActor actor) {
		if (!_statesDict.ContainsKey(newState)) {
			GD.PrintErr($"StateMachine: State {newState} not found, cannot change state.");
			return;
		}

		// Exit previous state
		var previous = _currentState;
		previous?.ExitState();

		// Enter new state
		_currentState = _statesDict[newState];
		_currentState.EnterState(actor);

		// Emit signal for Debugging
		var previousStateInt = previous != null ? Convert.ToInt32(previous.StateType) : Convert.ToInt32(_currentState.StateType);
		EmitSignal(SignalName.StateChanged, Convert.ToInt32(_currentState.StateType), previousStateInt);
	}

	public void Process(double delta) => _currentState?.ProcessState(delta);
	public void PhysicsProcess(double delta) => _currentState?.PhysicsProcessState(delta);
	public bool HasState(TStateType type) => _statesDict.ContainsKey(type);
}
