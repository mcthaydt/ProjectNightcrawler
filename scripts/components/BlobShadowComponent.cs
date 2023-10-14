using Godot;
public partial class BlobShadowComponent : RayCast3D 
{
	[Export] public NodePath DropShadowSpritePath;
	public Sprite3D DropShadowSprite;
	
	private float _dropShadowOffset = 0.2f;
	public override void _Ready()
	{
		if (DropShadowSpritePath != null)
		{
			DropShadowSprite = GetNode<Sprite3D>(DropShadowSpritePath);
		}
	}

	public override void _Process(double delta)
	{
		if (IsColliding())
		{
			DropShadowSprite.Visible = true;
		}
		else
		{
			DropShadowSprite.Visible = false;
		}
		
		Vector3 dropShadowPos = GetCollisionPoint();
		dropShadowPos.Z = GetCollisionPoint().Z + _dropShadowOffset;
		
		Transform3D dropShadowGlobalTransform = DropShadowSprite.GlobalTransform;
		dropShadowGlobalTransform.Origin = dropShadowPos;
		DropShadowSprite.GlobalTransform = dropShadowGlobalTransform;
	}
}
