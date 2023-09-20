using Godot;

// TODO: Camera should tilt and rotate with mouse movement

public partial class CameraFollowComponent : Node3D
{
	[Export] public Resource CameraData; 
	
	[Export] public NodePath TargetFollowObjectPath;
	public Node3D TargetObject;
	
	[Export] public NodePath CameraTargetRootPath;
	public Node3D CameraTargetRoot;
	
	private Vector3 _cameraOffset = Vector3.Zero;
	private float _cameraLerpSpeed = 0.0f;

	public override void _Ready()
	{
		if (TargetFollowObjectPath != null)
		{
			TargetObject = GetNode<Node3D>(TargetFollowObjectPath);
		}

		if (CameraTargetRootPath != null)
		{
			CameraTargetRoot = GetNode<Node3D>(CameraTargetRootPath);
		}
	}
	public override void _Process(double delta)
	{
		if (TargetObject == null || CameraTargetRoot == null)
		{
			return;
		}
		
		if (CameraData is BaseCameraData baseCameraData)
		{
			_cameraOffset = baseCameraData.CameraOffset;
			_cameraLerpSpeed = baseCameraData.PositionLerpSpeed;
		} 
		else
		{
			GD.Print("Missing Camera Data!");
			return;
		}	
		
		Vector3 curPos = CameraTargetRoot.GlobalTransform.Origin;
		Vector3 targetPos = TargetObject.GlobalTransform.Origin;
		curPos = curPos.Lerp(targetPos + baseCameraData.CameraOffset,(float)delta * baseCameraData.PositionLerpSpeed);
		CameraTargetRoot.Position = curPos;
	}
}
