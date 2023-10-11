using Godot;
using System;

public partial class Player3D : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Unity Types

	// Basic Types
	public const float speed = 50.0f;
	public const float acceleration = 1000.0f;
	public const float friction = 1000.0f;
 	public const float jumpVelocity = 100.0f;  
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		velocity = ApplyGravity(velocity, (float)delta);

		velocity = HandleJump(velocity, jumpVelocity);

		velocity = HandleSidewaysMovement(velocity, (float)delta);

		Velocity = velocity;
		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Player3D Methods
	public Vector3 ApplyGravity(Vector3 velocity, float timeDelta) {
		if (!IsOnFloor())
			velocity.Y -= gravity * timeDelta;

		return velocity;
	}

	public Vector3 HandleJump(Vector3 velocity, float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity.Y = jumpVelocity;
		
		return velocity;
	}

	public Vector3 HandleSidewaysMovement(Vector3 velocity, float delta) {
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		Vector2 movement = new Vector2(velocity.X, velocity.Z);
		
		if (direction != Vector2.Zero) {
			movement.X = Mathf.MoveToward(movement.X, 
										  speed * direction.X, 
										  acceleration * delta
										  );

			movement.Y = Mathf.MoveToward(movement.Y,
										  speed * direction.Y,
										  acceleration * delta
										  );

			movement = GeneralStatic.MagnitudeClamp(movement, speed);

			velocity.X = movement.X;
			velocity.Z = movement.Y;
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, friction * delta);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, friction * delta);
		}

		return velocity;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
