using Godot;
using System;

public partial class PlayerPhysicsComponent : RigidBody3D 
{
	[Export] public Resource PlayerData; 
	private Vector3 _playerVelocity = Vector3.Zero;
	private float _playerMovementSpeed = 0.0f;
	public override void _PhysicsProcess(double delta)
	{
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			_playerVelocity = baseCharacterData.Velocity;
			_playerMovementSpeed = baseCharacterData.MovementSpeed;
			
			AngularVelocity = _playerVelocity * _playerMovementSpeed;
			baseCharacterData.Velocity = _playerVelocity;
		}
	}
}
