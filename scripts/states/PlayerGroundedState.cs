using Godot;
using System;

public partial class PlayerGroundedState : State 
{
	[Export] public Resource PlayerData; 
	private Vector3 _playerVelocity = Vector3.Zero;

	public override void Enter()
	{
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			baseCharacterData.CurrentPrimaryCharacterState = BaseCharacterData.PrimaryCharacterState.Grounded;
		}
		else
		{
			GD.Print("Missing Player Data!");
			return;
		}
	}
	public override void Update(double delta)
	{
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			_playerVelocity = baseCharacterData.Velocity;
		}
		else
		{
			GD.Print("Missing Player Data!");
			return;
		}
		
		_playerVelocity = Vector3.Zero; 
		if (Input.IsActionPressed("move_right"))
		{
			_playerVelocity += Vector3.Forward;
		}
		if (Input.IsActionPressed("move_left"))
		{
			_playerVelocity += Vector3.Back;
		}
		if (Input.IsActionPressed("move_down"))
		{
			_playerVelocity += Vector3.Right;
		}
		if (Input.IsActionPressed("move_up"))
		{
			_playerVelocity += Vector3.Left;
		}
		_playerVelocity = _playerVelocity.Normalized();
		baseCharacterData.Velocity = _playerVelocity;	
	}

		// EmitSignal(SignalName.TransitionState, this, "playerspawningstate");
}
