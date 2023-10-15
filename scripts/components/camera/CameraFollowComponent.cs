using Godot;

namespace Nightcrawler.scripts.components.camera;

public partial class CameraFollowComponent : Node3D
{
	[Export] public Resource CameraData; 
	
	[Export] public NodePath TargetFollowObjectPath;
	private Node3D _targetObject;
	
	[Export] public NodePath CameraHolderRootPath;
	private Node3D _cameraHolder;
	
	private Vector3 _cameraOffset = Vector3.Zero;
	private float _cameraLerpSpeed = 0.0f;

	public override void _Ready()
	{
		if (TargetFollowObjectPath != null)
		{
			_targetObject = GetNode<Node3D>(TargetFollowObjectPath);
		}
		else
		{
			GD.Print("Missing Target Object Path!");
		}

		if (CameraHolderRootPath != null)
		{
			_cameraHolder = GetNode<Node3D>(CameraHolderRootPath);
		}
		else
		{
			GD.Print("Missing Camera Holder Path!");
		}
	}
	public override void _Process(double delta)
	{
		if (_targetObject == null || _cameraHolder == null)
		{
			return;
		}
		
		if (CameraData is data.base_resources.BaseCameraData baseCameraData)
		{
			_cameraOffset = baseCameraData.CameraOffset;
			_cameraLerpSpeed = baseCameraData.PositionLerpSpeed;
		} 
		else
		{
			GD.Print("Missing Camera Data!");
			return;
		}	
		
		Vector3 curPos = _cameraHolder.GlobalTransform.Origin;
		Vector3 targetPos = _targetObject.GlobalTransform.Origin;
		curPos = curPos.Lerp(targetPos + baseCameraData.CameraOffset,(float)delta * baseCameraData.PositionLerpSpeed);
		_cameraHolder.Position = curPos;
	}
}
