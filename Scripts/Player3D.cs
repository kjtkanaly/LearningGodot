using Godot;
using System;
using System.Diagnostics;

public partial class Player3D : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node3D Head;
	public Camera3D FP, TP;

	// Godot Types
	[Export] public PlayerMovementData movementData = null; 

	// Basic Types
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

		velocity = HandleJump(velocity, movementData.jumpVelocity);

		velocity = HandleSidewaysMovement(velocity, (float)delta);

		Velocity = velocity;
		MoveAndSlide();

		HandleLookingHorizontal((float)delta);

		CastRayFromCamera();
	}

	public override void _Process(double delta)
	{
		SwitchCamera();
	}

	public override void _UnhandledInput(InputEvent ev)
	{
		if (@ev is InputEventMouseMotion eventMouseMotion) {
			RotateY(-eventMouseMotion.Relative.X * movementData.mouseSensitivity);
			Head.RotateX(eventMouseMotion.Relative.Y * movementData.mouseSensitivity);

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
										  movementData.speed * direction.X, 
										  movementData.acceleration * delta
										  );

			velocity2D.Y = Mathf.MoveToward(velocity2D.Y,
										  movementData.speed * direction.Z,
										  movementData.acceleration * delta
										  );

			velocity2D = GeneralStatic.MagnitudeClamp(velocity2D, movementData.speed);

			velocity.X = velocity2D.X;
			velocity.Z = velocity2D.Y;
		}
		else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, movementData.friction * delta);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, movementData.friction * delta);
		}

		return velocity;
	}

	public void HandleLookingHorizontal(float delta) {
		Vector2 direction = Input.GetVector("Look Right", "Look Left", "Look Down", "Look Up");
		Vector3 rotation = RotationDegrees;

		if (direction != Vector2.Zero) {
			rotation.Y = Mathf.MoveToward(rotation.Y, 
										  movementData.rotationSpeed * direction.X + rotation.Y, 
										  movementData.rotationAcceleration * delta);
			
			rotation.X = Mathf.MoveToward(rotation.X, 
										  movementData.rotationSpeed * direction.Y + rotation.X, 
										  movementData.rotationAcceleration * delta);
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
 
	public void CastRayFromCamera() {
		if (Input.IsActionJustPressed("Primary Attack")) {
			Vector3 ray = FP.ProjectRayNormal(Vector2.Zero);
			GD.Print("SHoot");
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
