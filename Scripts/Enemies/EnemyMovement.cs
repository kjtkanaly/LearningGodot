using Godot;
using System;

public partial class EnemyMovement : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	[Export] NodePath PlayerPath = null;
	public CharacterBody3D Player = null;
	public EnemyAnimationCtrl AnimeCtrl = null;
	public Area3D BulletArea = null;

	// Godot Types

	// Basic Types
	[Export] public float friction = 1000.0f;
	[Export] public float impactForce = 50.0f;
	[Export] public float maxSpeed = 10.0f;
	[Export] public float acceleration = 1000.0f;
	const float attackRange = 20.0f;
	public float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>(PlayerPath);
		AnimeCtrl = GetNode<EnemyAnimationCtrl>("Animation Ctrl");
		BulletArea = GetNode<Area3D>("Bullet Area");

		// Connect Signals
		BulletArea.AreaEntered += BulletImpact;
	}

	public override void _PhysicsProcess(double delta)
	{
		// General Movement
		Vector2 latVelocity = new Vector2(Velocity.X, Velocity.Z);

		if (TargetInRange()) {
			FacePlayer();
			latVelocity = FollowPlayer(latVelocity, (float) delta);
		} else {
			latVelocity = PumpOnTheBrakes(latVelocity, (float) delta);
		}

		Velocity = new Vector3(latVelocity.X, Velocity.Y, latVelocity.Y);
		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Enemy Movement Methods
	private bool TargetInRange() {
		return GlobalPosition.DistanceTo(Player.GlobalPosition) <= attackRange;
	}

	private Vector2 FollowPlayer(Vector2 latVelocity, float delta) {
		Vector3 nextNavPoint = Player.GlobalTransform.Origin;
		Vector3 direction = (nextNavPoint - GlobalTransform.Origin).Normalized();

		latVelocity.X = Mathf.MoveToward(latVelocity.X, 
										 maxSpeed * direction.X, 
										 acceleration * delta
										 );

		latVelocity.Y = Mathf.MoveToward(latVelocity.Y, 
										 maxSpeed * direction.Z, 
										 acceleration * delta
										 );

		latVelocity = GeneralStatic.MagnitudeClamp(latVelocity, maxSpeed);
		return latVelocity;
	}

	private void FacePlayer() {
		Vector3 playerPos = Player.GlobalPosition;
		LookAt(new Vector3(playerPos.X, GlobalPosition.Y, playerPos.Z), Vector3.Up);
	}

	private Vector2 PumpOnTheBrakes(Vector2 latVelocity, float delta) {

		latVelocity.X = Mathf.MoveToward(latVelocity.X, 0, friction * delta);
		latVelocity.Y = Mathf.MoveToward(latVelocity.Y, 0, friction * delta);

		return latVelocity;
	}

	private void BulletImpact(Area3D RxArea) {
		Vector3 impulseVect = RxArea.GlobalTransform.Basis.Z;
		impulseVect.Y = 0.0f;

		Velocity = impulseVect.Normalized() * -impactForce;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}

