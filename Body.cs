using Godot;

namespace GravitySim;

public partial class Body : Node3D
{
    
    [Export]
    public double Mass { get; set; }
    
    [Export]
    public Vector3 Velocity { get; set; }

    public void ApplyForce(Vector3 force, double deltaTime) // delta time c'est le jolie temps
    {
        var a = force / (float)Mass;
        Velocity = a * (float)deltaTime;
        Position += Velocity *  (float)deltaTime;
    }
}