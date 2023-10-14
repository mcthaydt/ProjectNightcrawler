using Godot;

public partial class AdaptiveCameraFOVComponent : Node3D 
{
	[Export] public NodePath CameraPath;
	public Camera3D GameCamera;
	
	[Export] public NodePath PlayerRigidBodyPath;
	public RigidBody3D PlayerRigidBody;
	
	private float _playerSpeed = 0.0f;
	private float _curFOV = 46.0f;
	private float _lerpSpeed = 5.0f;
	
	public override void _Ready()
	{
		if (CameraPath != null)
		{
			GameCamera = GetNode<Camera3D>(CameraPath);
		}
		
		if (PlayerRigidBodyPath != null)
		{
			PlayerRigidBody = GetNode<RigidBody3D>(PlayerRigidBodyPath);
		}
	}

	public override void _Process(double delta)
	{
		_playerSpeed = PlayerRigidBody.LinearVelocity.Length();
		
		float mappedFOVValue = Mathf.Lerp(0.0f, 4.0f, _playerSpeed / 10.0f);
		
		if (_playerSpeed > 0.0f)
		{
			_curFOV = 46.0f + mappedFOVValue;
		}
		else
		{
			_curFOV = 46.0f;
		}

		GameCamera.Fov = Mathf.Lerp(GameCamera.Fov, _curFOV, (float)delta * _lerpSpeed);
	}
}
