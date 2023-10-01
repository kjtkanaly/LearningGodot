using Godot;
using System;

public partial class BallPhysics : CharacterBody3D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// velocity = Apply3DGravity(velocity, (float)delta);

		Velocity = velocity;
		MoveAndSlide();
	}

	public Vector3 Apply3DGravity(Vector3 velocity, float timeDelta) {
		if (!IsOnFloor())
			velocity.Y += gravity * timeDelta;

		return velocity;
	}
}
