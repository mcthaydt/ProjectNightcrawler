using Godot;

namespace Nightcrawler.scripts.components.player;

public partial class PlayerPhysicsAdaptToFrameRateComponent : Node
{
	[Export] public Resource PlayerData;
	private Vector3 _playerVelocity;

	[Export] public NodePath PlayerBodyPath;
	private CsgSphere3D _playerBody;
	
	[Export] public NodePath ParentObjectPath;
	private Node3D _parentObject;
	
	private float _targetFps = 60.0f;
	private float _lerpSpeed = 20.0f;

	private Vector3 _followOffset = new Vector3(0.0f, 2.471f, 0.0f);
	public override void _Ready()
	{
		if (PlayerBodyPath != null)
		{
			_playerBody = GetNode<CsgSphere3D>(PlayerBodyPath);
		}
		
		if (ParentObjectPath != null)
		{
			_parentObject = GetNode<Node3D>(ParentObjectPath);
		}
	}
	public override void _Process(double delta)
	{
		if (PlayerData is BaseCharacterData baseCharacterData)
		{
			_playerVelocity = baseCharacterData.Velocity;
		}
	
		// Adapted to C# from Garbaj
		float frameRate = (float)Engine.GetFramesPerSecond();
		Vector3 lerpInterval = _playerVelocity / frameRate;
		Vector3 lerpPosition = _playerBody.GlobalTransform.Origin + lerpInterval +_followOffset;

		if (frameRate > _targetFps)
		{
			_playerBody.TopLevel = true;
			Transform3D playerBodyGlobalTransform = _playerBody.GlobalTransform;
			playerBodyGlobalTransform.Origin = _playerBody.GlobalTransform.Origin.Lerp(lerpPosition, _lerpSpeed * (float)delta);
			_playerBody.GlobalTransform = playerBodyGlobalTransform;
		}
		else
		{
			_playerBody.GlobalTransform = _parentObject.GlobalTransform;
			_playerBody.TopLevel = false;
		}
		
		Transform3D bodyGlobalTransform = _playerBody.GlobalTransform;
		bodyGlobalTransform.Basis = _parentObject.GlobalTransform.Basis;
		_playerBody.GlobalTransform = bodyGlobalTransform;
	}
}
