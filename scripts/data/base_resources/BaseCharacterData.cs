using Godot;
using MonoCustomResourceRegistry;

[RegisteredType(nameof(BaseCharacterData))]
public partial class BaseCharacterData : Resource
{
    // Character should have a state machine that decides if motion
    
    public enum PrimaryCharacterState 
    {
        Spawning,
        Grounded,
        InAir,
        Death
    }
    private enum GroundedCharacterState
    {
        Idle,
        Moving 
    }
    private enum InAirCharacterState 
    {
        Jumping,
        Falling 
    }
    [Export] public float MovementSpeed { get; set; }
    [Export] public Vector3 Velocity { get; set; }
    [Export] public float JumpPower { get; set; }
    [Export] public PrimaryCharacterState CurrentPrimaryCharacterState { get; set; }

    public BaseCharacterData() : this(0.0f, Vector3.Zero, 0.0f, PrimaryCharacterState.Spawning) {}

    public BaseCharacterData(float movementSpeed, Vector3 velocity, float jumpPower, PrimaryCharacterState primaryCharacterState)
    {
        MovementSpeed = movementSpeed;
        Velocity = velocity;
        JumpPower = jumpPower;
        CurrentPrimaryCharacterState = primaryCharacterState;
    }
}
