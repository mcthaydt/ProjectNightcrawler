using Godot;
using System;

public partial class State : Node
{
	[Signal]
	public delegate void TransitionStateEventHandler(State from, string to);
	
	public virtual void Enter()
	{
	}

	public virtual void Update(double delta)
	{
	}
	
	public virtual void PhysicsUpdate(double delta)
	{
	}
	
	public virtual void Exit()
	{
	}
}
