using Godot;
using Nightcrawler.scripts.data.base_resources;

public partial class PlayerInAirState : State 
{
	[Export] public Resource PlayerData; 
	private float _slamPower = 0.0f;

	[Export] public NodePath PlayerRigidBodyPath;
	private RigidBody3D _playerRigidBody;
	
	[Export] public NodePath PlayerRayCastPath;
	private RayCast3D _playerRayCast;

	[Export] public int SlamMultiplier = 3;
	private const int SlamScaling = 10;
	
	private bool _alreadySlammed;
	
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
			baseCharacterData.CurrentCharacterState = BaseCharacterData.CharacterState.InAir;
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
			_slamPower = baseCharacterData.JumpPower;
		}
		else
		{
			GD.Print("Missing Player Data!");
			return;
		}
		
		if (Input.IsActionJustPressed("Jump") && !_alreadySlammed)
		{
			_playerRigidBody.ApplyCentralImpulse(Vector3.Up * -baseCharacterData.JumpPower * SlamMultiplier * SlamScaling); 
			_alreadySlammed = true;
		}
		if (_playerRayCast.IsColliding())
		{
			EmitSignal(State.SignalName.TransitionState, this, "PlayerGroundedState");
			_alreadySlammed = false;
		}	
	}
}
