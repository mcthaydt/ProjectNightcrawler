using Godot;

namespace Nightcrawler.scripts.components.camera;

public partial class AdaptiveCameraFovComponent : Node3D 
{
	[Export] public NodePath CameraPath;
	public Camera3D GameCamera;
	
	[Export] public NodePath PlayerRigidBodyPath;
	public RigidBody3D PlayerRigidBody;
	
	private float _playerSpeed = 0.0f;
	private float _curFov = 46.0f;
	private float _lerpSpeed = 5.0f;
	
	private const float MinFov = 46.0f;
	private const float FovRange = 4.0f;
	private const float MaxPlayerSpeed = 20.0f;
	
	public override void _Ready()
	{
		if (CameraPath != null)
		{
			GameCamera = GetNode<Camera3D>(CameraPath);
		}
		else
		{
			GD.Print("Missing Camera Node Path");
		}
		
		if (PlayerRigidBodyPath != null)
		{
			PlayerRigidBody = GetNode<RigidBody3D>(PlayerRigidBodyPath);
		}
		else
		{
			GD.Print("Missing Player Rigidbody Node Path");
		}
	}

	public override void _Process(double delta)
	{
		_playerSpeed = PlayerRigidBody.LinearVelocity.Length();
		
		float adaptedFovValue = Mathf.Lerp(0.0f, FovRange, Mathf.InverseLerp(0.0f, MaxPlayerSpeed, _playerSpeed));
		
		if (_playerSpeed > 0.0f)
		{
			_curFov = MinFov + adaptedFovValue;
		}
		else
		{
			_curFov = MinFov;
		}

		GameCamera.Fov = Mathf.Lerp(GameCamera.Fov, _curFov, (float)delta * _lerpSpeed);
	}
}
