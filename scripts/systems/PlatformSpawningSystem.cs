using Godot;
using System;

public partial class PlatformSpawningSystem : Node
{
	private PackedScene _platformScene = GD.Load<PackedScene>("res://objects/Platform.tscn");

	private float _platformDistanceX = 6.0f;
	private float _platformDistanceZ = 12.0f;
	private float _platformDistanceYMin = 0.0f;
	private float _platformDistanceYMax = 3.0f;
	
	public override void _Ready()
	{ 
		SpawnPlatform();
	}

	private void SpawnPlatform()
	{
		float zPos = 0.0f;
		for (int i = 0; i < 20; i++)
		{
			Node3D platformInstance = (Node3D)_platformScene.Instantiate();
			Transform3D platformInstanceTransform = platformInstance.Transform;
			platformInstanceTransform.Origin.Z = zPos;
			platformInstanceTransform.Origin.X = (float)GD.RandRange(-_platformDistanceX, _platformDistanceX);
			platformInstanceTransform.Origin.Y = (float)GD.RandRange(_platformDistanceYMin, _platformDistanceYMax);
			platformInstance.Transform = platformInstanceTransform;
			AddChild(platformInstance);
			zPos -= _platformDistanceZ;
		}
	}
}
