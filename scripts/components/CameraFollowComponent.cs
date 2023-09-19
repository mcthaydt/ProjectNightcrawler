using Godot;

// Camera should tilt and rotate with mouse movement
public partial class CameraFollowComponent : Node3D
{
	[Export] public Resource CameraData; 
	
	[Export] public NodePath TargetFollowObjectPath;
	public Node3D TargetObject;
	
	[Export] public NodePath CameraTargetRootPath;
	public Node3D CameraTargetRoot;

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
		if (TargetObject != null && CameraTargetRoot != null)
		{
			return;
		}
		
		if (CameraData is BaseCameraData baseCameraData)
		{
		   Vector3 curPos = CameraTargetRoot.GlobalTransform.Origin;
		   Vector3 targetPos = TargetObject.GlobalTransform.Origin;
		   curPos = curPos.Lerp(targetPos + baseCameraData.CameraOffset,(float)delta * baseCameraData.LerpSpeed);
		   CameraTargetRoot.Position = curPos;
		}
	}
}
