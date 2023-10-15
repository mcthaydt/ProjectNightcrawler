using Godot;

namespace Nightcrawler.scripts.components.player;

public partial class PlayerPhysicsAdaptToFrameRateComponent : Node3D
{
	[Export] public Resource PlayerData;
	private Vector3 _playerVelocity;

	[Export] public NodePath PlayerMeshPath;
	private CsgSphere3D _playerMesh;
	
	private float _targetFps = 60.0f;
	private float _lerpSpeed = 20.0f;
	
	public override void _Ready()
	{
		if (PlayerMeshPath != null)
		{
			_playerMesh = GetNode<CsgSphere3D>(PlayerMeshPath);
		}
		else
		{
			GD.Print("Missing Player Body Node Path!");
		}
	}
	public override void _Process(double delta)
	{
		if (PlayerData is data.base_resources.BaseCharacterData baseCharacterData)
		{
			_playerVelocity = baseCharacterData.Velocity;
		}
	
		// Adapted to C# from Garbaj
		float frameRate = (float)Engine.GetFramesPerSecond();
		Vector3 lerpInterval = _playerVelocity / frameRate;
		Vector3 lerpPosition = _playerMesh.GlobalTransform.Origin + lerpInterval;

		if (frameRate > _targetFps)
		{
			_playerMesh.TopLevel = true;
			Transform3D playerBodyGlobalTransform = _playerMesh.GlobalTransform;
			playerBodyGlobalTransform.Origin = _playerMesh.GlobalTransform.Origin.Lerp(lerpPosition, _lerpSpeed * (float)delta);
			_playerMesh.GlobalTransform = playerBodyGlobalTransform;
		}
		else
		{
			_playerMesh.GlobalTransform = GlobalTransform;
			_playerMesh.TopLevel = false;
		}
		
		Transform3D bodyGlobalTransform = _playerMesh.GlobalTransform;
		bodyGlobalTransform.Basis = GlobalTransform.Basis;
		_playerMesh.GlobalTransform = bodyGlobalTransform;
	}
}
