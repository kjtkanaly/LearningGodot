using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private PlayerData PD;
	private PlayerAnimationDirector PAD;

	// Godot Types
	private Vector2 lateralVelocitySnapshot;
	private Vector2 inputDirection;

	// Basic Types
	private float verticalVelocitySnapshot;
	public float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		PD = GetNode<PlayerData>("Player Data");
		PAD = GetNode<PlayerAnimationDirector>("Anime");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update Velocity Snapshot Variables
		lateralVelocitySnapshot = new Vector2(Velocity.X, Velocity.Z);
		verticalVelocitySnapshot = Velocity.Y;

		// Apply Vertical Velocity M A T H & Logic
		ApplyGravity((float)delta);
		HandleJump(PD.movementData.jumpVelocity);

		// Apply Laterial Velocity Logic
		HandleBasicLateralMovement((float)delta);
		HandleDodgeRoll((float)delta);

		Velocity = new Vector3(lateralVelocitySnapshot.X, 
							   verticalVelocitySnapshot, 
							   lateralVelocitySnapshot.Y);

		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Player3D Methods
	public void ApplyGravity(float timeDelta) {
		if (!IsOnFloor())
			verticalVelocitySnapshot -= gravity * timeDelta;
	}

	public void HandleJump(float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			verticalVelocitySnapshot = jumpVelocity;
	}

	public void HandleBasicLateralMovement(float delta) {
		Vector3 direction = GetGlobalInputDirectionNorm();

		if (direction != Vector3.Zero) {
			lateralVelocitySnapshot.X = Mathf.MoveToward(
										  lateralVelocitySnapshot.X, 
										  PD.movementData.speed * direction.X, 
										  PD.movementData.acceleration * delta);

			lateralVelocitySnapshot.Y = Mathf.MoveToward(
										  lateralVelocitySnapshot.Y,
										  PD.movementData.speed * direction.Z,
										  PD.movementData.acceleration * delta);

			lateralVelocitySnapshot = GeneralStatic.MagnitudeClamp(
										  lateralVelocitySnapshot, 
										  PD.movementData.speed);
		}
		else {
			lateralVelocitySnapshot = lateralVelocitySnapshot.MoveToward(
										  Vector2.Zero,
										  PD.movementData.friction * delta);
		}
	}

	public void HandleDodgeRoll(float delta) {
		// Check if currently rolling
		if (PAD.CheckPlayingStatus() && (PAD.GetCurrentAnimationName() == "Roll"))
			return;

		if (Input.IsActionPressed("Roll") && IsOnFloor()) {
			Vector3 direction = GetGlobalInputDirectionNorm();

			if (direction == Vector3.Zero) {
				Vector3 basis = GlobalTransform.Basis.Z;
				direction = new Vector3(basis.X, 0, basis.Z).Normalized();
			}

			lateralVelocitySnapshot = new Vector2(direction.X, direction.Z) 
									  * PD.movementData.rollSpeed;
			PAD.PlayRollAnimation();;
		}
	}

	private Vector3 GetGlobalInputDirectionNorm() {
		inputDirection = Input.GetVector("Right", "Left", "Down", "Up");
		Vector3 direction = (Transform.Basis 
							 * new Vector3(inputDirection.X, 0, inputDirection.Y));
		direction = direction.Normalized();
		return direction;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
