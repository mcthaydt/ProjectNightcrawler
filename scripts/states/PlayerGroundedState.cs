using Godot;
using System;

public partial class PlayerGroundedState : State 
{
	[Export] public Resource PlayerData; 
	private float _jumpPower = 0.0f;

	[Export] public NodePath PlayerRigidBodyPath;
	private RigidBody3D PlayerRigidBody;

	[Export] public int JumpMultipler = 3;
	private const int JUMP_SCALING = 10;
	
	public override void Enter()
	{
		if (PlayerRigidBodyPath != null)
		{
			PlayerRigidBody = GetNode<RigidBody3D>(PlayerRigidBodyPath);
		}
		
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
	public override void PhysicsUpdate(double delta)
	{
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			_jumpPower = baseCharacterData.JumpPower;
		}
		else
		{
			GD.Print("Missing Player Data!");
			return;
		}
		
		if (Input.IsActionJustPressed("Jump"))
		{
			PlayerRigidBody.ApplyCentralImpulse(Vector3.Up * baseCharacterData.JumpPower * JumpMultipler * JUMP_SCALING); 
		}
	}

		// EmitSignal(SignalName.TransitionState, this, "playerspawningstate");
}
