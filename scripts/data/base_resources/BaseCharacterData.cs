using Godot;
using MonoCustomResourceRegistry;

namespace Nightcrawler.scripts.data.base_resources;

[RegisteredType(nameof(BaseCharacterData))]
public partial class BaseCharacterData : Resource
{
    public enum CharacterState 
    {
        Spawning,
        Grounded,
        InAir,
        Death
    }
    
    [Export] public float MovementSpeed { get; set; }
    [Export] public Vector3 Velocity { get; set; }
    [Export] public float JumpPower { get; set; }
    public CharacterState CurrentCharacterState { get; set; }

    public BaseCharacterData() : this(0.0f, Vector3.Zero, 0.0f, CharacterState.Spawning) {}

    public BaseCharacterData(float movementSpeed, Vector3 velocity, float jumpPower, CharacterState characterState)
    {
        MovementSpeed = movementSpeed;
        Velocity = velocity;
        JumpPower = jumpPower;
        CurrentCharacterState = characterState;
    }
}
