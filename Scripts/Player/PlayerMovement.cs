using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private PlayerData PD;
	private AnimationPlayer Anime;

	// Godot Types

	// Basic Types
	public float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		PD = GetNode<PlayerData>("Player Data");
		Anime = GetNode<AnimationPlayer>("Anime");
	}

	public override void _PhysicsProcess(double delta)
	{
		// General Movement
		Vector3 velocity = Velocity;
		velocity = ApplyGravity(velocity, (float)delta);
		velocity = HandleJump(velocity, PD.movementData.jumpVelocity);
		velocity = LateralMovements(velocity, (float)delta);
		Velocity = velocity;
		PlayerDodgeRoll((float)delta);
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

	public Vector3 LateralMovements(Vector3 velocity, float delta) {
		Vector2 inputDirection = Input.GetVector("Right", "Left", "Down", "Up");
		Vector3 direction = (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();
		Vector2 velocity2D = new Vector2(velocity.X, velocity.Z);

		if (direction != Vector3.Zero) {
			velocity2D.X = Mathf.MoveToward(velocity2D.X, 
										  PD.movementData.speed * direction.X, 
										  PD.movementData.acceleration * delta
										  );

			velocity2D.Y = Mathf.MoveToward(velocity2D.Y,
										  PD.movementData.speed * direction.Z,
										  PD.movementData.acceleration * delta
										  );

			velocity2D = GeneralStatic.MagnitudeClamp(velocity2D, PD.movementData.speed);

			velocity.X = velocity2D.X;
			velocity.Z = velocity2D.Y;
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, PD.movementData.friction * delta);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, PD.movementData.friction * delta);
		}

		return velocity;
	}

	public void PlayerDodgeRoll(float delta) {
		if (Input.IsActionPressed("Roll")) {
			// Check if currently rolling
			if (Anime.IsPlaying()) 
				return;

			Anime.Play("Roll");
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
