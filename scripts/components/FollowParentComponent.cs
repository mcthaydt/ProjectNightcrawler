using Godot;
using System;

public partial class FollowParentComponent : Node3D
{
	
	[Export] public NodePath TargetObjectPath;
	public Node3D TargetObject;
	
	[Export] public NodePath FollowObjectPath;
	public Node3D FollowObject;
	
	[Export] public Vector3 FollowOffset = Vector3.Zero;
	
	public override void _Ready()
	{
		if (TargetObjectPath != null)
		{
			TargetObject = GetNode<Node3D>(TargetObjectPath);
		}

		if (FollowObjectPath != null)
		{
			FollowObject = GetNode<Node3D>(FollowObjectPath);
		}
	}

	public override void _Process(double delta)
	{
		
		if (TargetObject == null || FollowObject == null)
		{
			return;
		}
		
		
		Vector3 curPos = FollowObject.GlobalTransform.Origin;
		Vector3 targetPos = TargetObject.GlobalTransform.Origin;
		FollowObject.Position = targetPos + FollowOffset;
	}
}
