using Godot;
using System;

public partial class EnemyMovement : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	[Export] NodePath PlayerPath = null;
	public CharacterBody3D Player = null;
	public EnemyAnimationCtrl AnimeCtrl = null;

	// Godot Types

	// Basic Types
	[Export] public const float speed = 10.0f;
	const float attackRange = 20.0f;
	public float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>(PlayerPath);
		AnimeCtrl = GetNode<EnemyAnimationCtrl>("Animation Ctrl");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!TargetInRange() || AnimeCtrl.hitCheck) {
			Velocity = Vector3.Zero;
			return;
		}
			
		FacePlayer();
		Vector3 nextNavPoint = Player.GlobalTransform.Origin;
		Velocity = (nextNavPoint - GlobalTransform.Origin).Normalized() * speed;
		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Enemy Movement Methods
	private bool TargetInRange() {
		return GlobalPosition.DistanceTo(Player.GlobalPosition) <= attackRange;
	}

	private void FacePlayer() {
		Vector3 playerPos = Player.GlobalPosition;
		LookAt(new Vector3(playerPos.X, GlobalPosition.Y, playerPos.Z), Vector3.Up);
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}

