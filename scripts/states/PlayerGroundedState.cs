using Godot;

public partial class PlayerGroundedState : State 
{
	[Export] public Resource PlayerData; 
	private float _jumpPower;

	[Export] public NodePath PlayerRigidBodyPath;
	private RigidBody3D _playerRigidBody;

	[Export] public NodePath PlayerRayCastPath;
	private RayCast3D _playerRayCast;
	
	[Export] public int JumpMultiplier = 3;
	private const int JumpScaling = 10;
	
	private const float CoyoteTime = 0.1f; 
	private float _coyoteTimeRemaining = 0f;	
	private bool _alreadyJumped;
	
	public override void Enter()
	{
		if (PlayerRigidBodyPath != null)
		{
			_playerRigidBody = GetNode<RigidBody3D>(PlayerRigidBodyPath);
		}
		
		if (PlayerRayCastPath != null)
		{
			_playerRayCast = GetNode<RayCast3D>(PlayerRayCastPath);
		}
		
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			baseCharacterData.CurrentPrimaryCharacterState = BaseCharacterData.PrimaryCharacterState.Grounded;
		}
		else
		{
			GD.Print("Missing Player Data!");
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
		
		if (Input.IsActionJustPressed("Jump") & !_alreadyJumped & _playerRayCast.IsColliding())
		{
			_playerRigidBody.ApplyCentralImpulse(Vector3.Up * baseCharacterData.JumpPower * JumpMultiplier * JumpScaling);
			_alreadyJumped = true;
		}
	
		if (_playerRayCast.IsColliding() == false)
		{
			_alreadyJumped = false;
			EmitSignal(State.SignalName.TransitionState, this, "PlayerInAirState");
		}	
	}
}
