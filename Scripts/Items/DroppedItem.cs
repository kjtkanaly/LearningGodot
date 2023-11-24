using Godot;
using System;

public partial class DroppedItem : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets

    // Godot Types

    // Basic Types
	private float time = 0.0f;
	[Export] public float amp = 0.5f;
	[Export] public float frequency = 0.5f;
	[Export] public float yPos = 1.5f;
	[Export] public float angularSpeed = 0.5f;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _PhysicsProcess(double delta)
    {
        RaiseAndLower((float) delta);
		RotateItem((float) delta);
    }

    //-------------------------------------------------------------------------
    // Dropped Item Methods
	public void RaiseAndLower(float delta) {
		Vector3 pos = Position;
		time += delta;
		pos.Y = amp * MathF.Sin(2 * MathF.PI * frequency * time) + yPos;
		Position = pos;
	}

	public void RotateItem(float delta) {
		RotateY(angularSpeed * delta);
	}

    //-------------------------------------------------------------------------
    // Demo Methods
}