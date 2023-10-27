using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class Player3D : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node3D Head = null;
	public Camera3D FP = null, TP = null, ActiveCamera = null;
	public AnimationPlayer RifleAnime = null;
	public RayCast3D GunBarrel = null;
	public PackedScene BulletRes = (PackedScene) GD.Load("res://3D Scenes/Bullet.tscn");
	public Node3D BulletInst = null;
	private RangeWeapon WeaponInst = null;

	// Godot Types
	[Export] public PlayerMovementData movementData = null; 

	// Basic Types
	public float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();
	public const float rayLength = 1000.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Head = GetNode<Node3D>("Head");
		FP = Head.GetNode<Camera3D>("1st Person Camera");
		TP = Head.GetNode<Camera3D>("3rd Person Camera");
		RifleAnime = Head.GetNode<AnimationPlayer>("Rifle/AnimationPlayer");
		GunBarrel = Head.GetNode<RayCast3D>("Rifle/RayCast3D");
		WeaponInst = Head.GetNode<RangeWeapon>("Rifle");

		// Set the active Camera
		SetActiveCamera();
	}

	public override void _PhysicsProcess(double delta)
	{
		// General Movement
		Vector3 velocity = Velocity;
		velocity = ApplyGravity(velocity, (float)delta);
		velocity = HandleJump(velocity, movementData.jumpVelocity);
		velocity = LateralMovements(velocity, (float)delta);
		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		SwitchCamera();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton 
			&& eventMouseButton.IsActionPressed("Primary Attack"))
		{
			// Cast Ray from Active Camera
			Dictionary target = CastRayFromCamera(eventMouseButton);

			ShootRifle();
		}
	}

	public override void _UnhandledInput(InputEvent ev)
	{
		if (@ev is InputEventMouseMotion eventMouseMotion) {
			MouseCameraMovement(eventMouseMotion);
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

	public Vector3 LateralMovements(Vector3 velocity, float delta) {
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

	private void SetActiveCamera() {
		if (FP.Current) {
			ActiveCamera = FP;
		} else {
			ActiveCamera = TP;
		}
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

		SetActiveCamera();
	}
 
	public void ShootRifle() {
		if (RifleAnime.IsPlaying())
		return;

		// Cast Ray from Camera

		// Play the Shoot Animation
		RifleAnime.Play("Shoot");

		// Instantiate the bullet
		BulletInst = (Node3D) BulletRes.Instantiate();
		BulletInst.Position = GunBarrel.GlobalPosition;
		BulletInst.Basis = GunBarrel.GlobalTransform.Basis;
		GetParent().AddChild(BulletInst);
	}

	public void MouseCameraMovement(InputEventMouseMotion mouseMotion) {
		RotateY(-mouseMotion.Relative.X * movementData.mouseSensitivity);
		Head.RotateX(mouseMotion.Relative.Y * movementData.mouseSensitivity);

		// Clamp the X-Rotation
		Vector3 rotation = Head.Rotation;
		rotation.X = Mathf.Clamp(rotation.X, 
								 -Mathf.Pi/2 + ActiveCamera.Rotation.X, 
								 Mathf.Pi/2 + ActiveCamera.Rotation.X
								 );
		Head.Rotation = rotation;
	}

	public Dictionary CastRayFromCamera(InputEventMouseButton eventMB) {
		// Generate the From and To Vectors
		Vector3 from = ActiveCamera.ProjectRayOrigin(eventMB.Position);
		Vector3 to = from + ActiveCamera.ProjectRayNormal(eventMB.Position) * rayLength;

		// Perform the Ray Cast Query
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		PhysicsRayQueryParameters3D query = 
			PhysicsRayQueryParameters3D.Create(from, 
											   to,
											   CollisionMask 
											   );
		return spaceState.IntersectRay(query);

		/*if (result.Count > 0)
		{
			GodotObject obj = result["collider"].AsGodotObject();
			//GD.Print("Object: ", obj);

			// Node3D temp = (Node3D) result["collider"].AsGodotObject()._Get("Node3D");

			return (Vector3)result["position"];
		}

		return Vector3.Inf;
		*/
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
