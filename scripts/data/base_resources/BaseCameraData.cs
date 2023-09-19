using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BaseCameraData))]
public partial class BaseCameraData: Resource
{
    [Export] public float LerpSpeed { get; set; }
    [Export] public Vector3 CameraOffset{ get; set; }
    
    public BaseCameraData() : this(0.0f, Vector3.Zero) {}
    public BaseCameraData(float lerpSpeed, Vector3 cameraOffset)
    {
        LerpSpeed = lerpSpeed;
        CameraOffset = cameraOffset;
    }
}
