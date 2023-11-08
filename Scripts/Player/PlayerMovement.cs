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
	private Vector2 RollVelocity = Vector2.Zero;

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
		Vector2 latVelocity = new Vector2(Velocity.X, Velocity.Z);
		float vertVelocity = Velocity.Y;

		// Vertical Movement
		vertVelocity = ApplyGravity(vertVelocity, (float)delta);
		vertVelocity = HandleJump(vertVelocity, PD.movementData.jumpVelocity);

		// Laterial Movement
		latVelocity = LateralMovements(latVelocity, (float)delta);
		latVelocity = PlayerDodgeRoll(latVelocity, (float)delta);

		Velocity = new Vector3(latVelocity.X, vertVelocity, latVelocity.Y);

		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Player3D Methods
	public float ApplyGravity(float velocity, float timeDelta) {
		if (!IsOnFloor())
			velocity -= gravity * timeDelta;

		return velocity;
	}

	public float HandleJump(float velocity, float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity = jumpVelocity;
		
		return velocity;
	}

	public Vector2 LateralMovements(Vector2 velocity, float delta) {
		Vector2 inputDirection = Input.GetVector("Right", "Left", "Down", "Up");
		Vector3 direction = (Transform.Basis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();

		if (direction != Vector3.Zero) {
			velocity.X = Mathf.MoveToward(velocity.X, 
										  PD.movementData.speed * direction.X, 
										  PD.movementData.acceleration * delta
										  );

			velocity.Y = Mathf.MoveToward(velocity.Y,
										  PD.movementData.speed * direction.Z,
										  PD.movementData.acceleration * delta
										  );

			velocity = GeneralStatic.MagnitudeClamp(velocity, PD.movementData.speed);
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, PD.movementData.friction * delta);
			velocity.Y = Mathf.MoveToward(velocity.Y, 0, PD.movementData.friction * delta);
		}

		return velocity;
	}

	public Vector2 PlayerDodgeRoll(Vector2 velocity, float delta) {

		// Check if currently rolling
		if (Anime.IsPlaying() && (Anime.CurrentAnimation == "Roll"))
			return RollVelocity;

		if (Input.IsActionPressed("Roll") && IsOnFloor()) {
			Vector3 basis = GlobalTransform.Basis.Z;
			RollVelocity = new Vector2(basis.X, basis.Z).Normalized() 
						   * PD.movementData.rollSpeed;
			Anime.Play("Roll");
			return RollVelocity;
		}

		return velocity;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
