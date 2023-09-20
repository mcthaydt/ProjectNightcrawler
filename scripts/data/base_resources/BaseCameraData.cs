using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BaseCameraData))]
public partial class BaseCameraData: Resource
{
    [Export] public float PositionLerpSpeed { get; set; }
    [Export] public float TiltLerpSpeed { get; set; }
    [Export] public Vector3 CameraOffset{ get; set; }
    
    public BaseCameraData() : this(0.0f, 0.0f, Vector3.Zero) {}
    public BaseCameraData(float positionLerpSpeed, float tiltLerpSpeed, Vector3 cameraOffset)
    {
        PositionLerpSpeed = positionLerpSpeed;
        TiltLerpSpeed = tiltLerpSpeed;
        CameraOffset = cameraOffset;
    }
}
