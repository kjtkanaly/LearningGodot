using Godot;
using System;

public partial class CameraMount : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets

    // Godot Types

    // Basic Types
	[Export] public float amp = 1.0f;
	[Export] public float frequency = 1.0f;
	[Export] public float angularSpeed = 1.0f;
	public float initHeight = 0.0f;
	public float time = 0.0f;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        initHeight = Position.Y;
    }
    public override void _PhysicsProcess(double delta)
    {
        RaiseAndLower((float) delta);
		RotateMount((float) delta);
    }

    //-------------------------------------------------------------------------
    // Camera Mount Methods
	public void RaiseAndLower(float delta) {
		Vector3 pos = Position;
		time += delta;
		pos.Y = amp * MathF.Sin(2 * MathF.PI * frequency * time) + initHeight;
		Position = pos;
	}

	public void RotateMount(float delta) {
		RotateY(angularSpeed * delta);
	}

    //-------------------------------------------------------------------------
    // Demo Methods
}