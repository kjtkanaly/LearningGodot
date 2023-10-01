using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float speed = 100.0f;
	public const float acceleration = 1000.0f;
	public const float friction = 1000.0f;
	public const float jumpVelocity = -300.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		velocity = ApplyGravity(velocity, (float)delta);

		velocity = HandleJump(velocity, jumpVelocity);

		velocity = HandleSidewaysMovement(velocity, (float)delta);

		Velocity = velocity;
		MoveAndSlide();
	}

	public Vector2 ApplyGravity(Vector2 velocity, float timeDelta) {
		if (!IsOnFloor())
			velocity.Y += gravity * timeDelta;

		return velocity;
	}

	public Vector2 HandleJump(Vector2 velocity, float jumpVelocity) {
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = jumpVelocity;
		
		return velocity;
	}

	public Vector2 HandleSidewaysMovement(Vector2 velocity, float delta) {
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero) {
			velocity.X = Mathf.MoveToward(velocity.X, 
										  speed * direction.X, 
										  acceleration * delta
										  );
		}
		else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, friction * (float)delta);
		}

		return velocity;
	}
}
