using Godot;

public partial class PlayerMovementComponent : Node3D
{
	[Export] public Resource PlayerData; 
	private Vector3 _playerVelocity = Vector3.Zero;
	
	public override void _Process(double delta)
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
			_playerVelocity += Vector3.Right;
		}
		if (Input.IsActionPressed("move_left"))
		{
			_playerVelocity += Vector3.Left;
		}
		if (Input.IsActionPressed("move_down"))
		{
			_playerVelocity += Vector3.Back;
		}
		if (Input.IsActionPressed("move_up"))
		{
			_playerVelocity += Vector3.Forward;
		}
		_playerVelocity = _playerVelocity.Normalized();
		baseCharacterData.Velocity = _playerVelocity;
	}
}
