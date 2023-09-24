using Godot;
using Godot.Collections;

public partial class StateMachine : Node
{
	Dictionary _states = new Godot.Collections.Dictionary();
	State _currentState;
	
	[Export] public State InitialState;
	[Export] public Array<State> StateCollection;
	public override void _Ready()
	{
		foreach (var state in StateCollection)
		{
			_states.Add(state.Name.ToString()!.ToLower(), state);
			state.TransitionState += OnTransitionState;
		}
		
		if (InitialState != null)
		{
			InitialState.Enter();
			_currentState = InitialState;
		}
	}

	public override void _Process(double delta)
	{
		if (_currentState != null)
		{
			_currentState.Update(delta);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (_currentState != null)
		{
			_currentState.PhysicsUpdate(delta);
		}
	}

	private void OnTransitionState(State prevState, State nextState)
	{
		if (prevState != _currentState)
		{
			return;
		}
		
		if (_currentState != null)
		{
			_currentState.Exit();
		}
		
		State newState = (State)_states[nextState.Name.ToString()!.ToLower()];
		newState.Enter();
		_currentState = newState;
	}
}
