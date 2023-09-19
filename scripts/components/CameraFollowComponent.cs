using Godot;
using System;

public partial class CameraFollowComponent : Node3D
{
	// Camera should tilt and rotate with mouse movement
	[Export] public NodePath TargetFollowObjectPath;
	public Node3D TargetObject;
	
	[Export] public NodePath CameraTargetRootPath;
	public Node3D CameraTargetRoot;

	[Export] public float LerpSpeed = 0.1f;

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
			Vector3 curPos = CameraTargetRoot.GlobalTransform.Origin;
			Vector3 targetPos = TargetObject.GlobalTransform.Origin;
			Vector3 offset = new Vector3(0.0f, 3.0f, 10.0f);
			curPos = curPos.Lerp(targetPos + offset,(float)delta * LerpSpeed);
			CameraTargetRoot.Position = curPos;
		}
	}
}
