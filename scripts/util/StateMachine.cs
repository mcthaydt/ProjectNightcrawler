using Godot;
using Godot.Collections;

public partial class StateMachine : Node
{
	Dictionary _states = new Godot.Collections.Dictionary();
	public State CurrentState;
	
	[Export] public State InitialState;
	[Export] public Array<State> StateCollection;
	public override void _Ready()
	{
		foreach (var state in StateCollection)
		{
			_states.Add(state.Name, state);
			state.TransitionState += OnTransitionState;
		}
		
		if (InitialState != null)
		{
			InitialState.Enter();
			CurrentState = InitialState;
		}
	}

	public override void _Process(double delta)
	{
		if (CurrentState != null)
		{
			CurrentState.Update(delta);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (CurrentState != null)
		{
			CurrentState.PhysicsUpdate(delta);
		}
	}

	private void OnTransitionState(State prevState, string nextState)
	{
		if (prevState != CurrentState)
		{
			return;
		}
		
		if (CurrentState != null)
		{
			CurrentState.Exit();
		}
		
		State newState = (State)_states[nextState];
		newState.Enter();
		CurrentState = newState;
	}
}
