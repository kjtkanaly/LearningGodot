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
	public const float rotationSpeed = 2.0f;
	public const float rotationAcceleration = 150.0f;
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

		HandleLookingHorizontal((float)delta);
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
		Vector2 inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();
		Vector2 velocity2D = new Vector2(velocity.X, velocity.Z);

		if (direction != Vector3.Zero) {
			velocity2D.X = Mathf.MoveToward(velocity2D.X, 
										  speed * direction.X, 
										  acceleration * delta
										  );

			velocity2D.Y = Mathf.MoveToward(velocity2D.Y,
										  speed * direction.Z,
										  acceleration * delta
										  );

			velocity2D = GeneralStatic.MagnitudeClamp(velocity2D, speed);

			velocity.X = velocity2D.X;
			velocity.Z = velocity2D.Y;
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, friction * delta);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, friction * delta);
		}

		return velocity;
	}

	public void HandleLookingHorizontal(float delta) {
		Vector2 direction = Input.GetVector("Look Right", "Look Left", "Look Up", "Look Down");
		Vector3 rotation = RotationDegrees;

		if (direction != Vector2.Zero) {
			rotation.Y = Mathf.MoveToward(rotation.Y, 
										  rotationSpeed * direction.X + rotation.Y, 
										  rotationAcceleration * delta);
		}

		RotationDegrees = rotation;
		
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
