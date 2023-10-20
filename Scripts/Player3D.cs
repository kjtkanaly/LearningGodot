using Godot;
using System;

public partial class Player3D : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node3D Head;
	public Camera3D FP, TP;

	// Godot Types

	// Basic Types
	public const float speed = 50.0f;
	public const float acceleration = 1000.0f;
	public const float friction = 1000.0f;
 	public const float jumpVelocity = 100.0f;  
	public const float rotationSpeed = 2.0f;
	public const float rotationAcceleration = 150.0f;
	public const float mouseSensitivity = 0.01f;
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Head = GetNode<Node3D>("Head");
		FP = Head.GetNode<Camera3D>("1st Person Camera");
		TP = Head.GetNode<Camera3D>("3rd Person Camera");
	}

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

	public override void _Process(double delta)
	{
		SwitchCamera();
	}

	public override void _UnhandledInput(InputEvent ev)
	{
		if (@ev is InputEventMouseMotion eventMouseMotion) {
			RotateY(-eventMouseMotion.Relative.X * mouseSensitivity);
			Head.RotateX(-eventMouseMotion.Relative.Y * mouseSensitivity);

			// Clamp the X-Rotation
			Vector3 rotation = Head.Rotation;
			rotation.X = Mathf.Clamp(rotation.X, -Mathf.Pi/2, Mathf.Pi/2);
			Head.Rotation = rotation;
		}
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
		Vector2 inputDirection = Input.GetVector("Right", "Left", "Down", "Up");
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
		Vector2 direction = Input.GetVector("Look Right", "Look Left", "Look Down", "Look Up");
		Vector3 rotation = RotationDegrees;

		if (direction != Vector2.Zero) {
			rotation.Y = Mathf.MoveToward(rotation.Y, 
										  rotationSpeed * direction.X + rotation.Y, 
										  rotationAcceleration * delta);
			
			rotation.X = Mathf.MoveToward(rotation.X, 
										  rotationSpeed * direction.Y + rotation.X, 
										  rotationAcceleration * delta);
		}

		RotationDegrees = rotation;
		
	}

	public void SwitchCamera() {
		if (Input.IsActionJustPressed("Change Camera")) {
			if (TP.Current) {
				TP.ClearCurrent();
				FP.MakeCurrent();
			} else {
				FP.ClearCurrent();
				TP.MakeCurrent(); 
			}
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
