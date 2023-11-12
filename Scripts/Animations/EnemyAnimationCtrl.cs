using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyAnimationCtrl : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	[Export] NodePath ParentPath = null;
	private CharacterBody3D Parent = null;
	public AnimationPlayer AnimePlayer = null;
	public AnimationTree AnimeTree = null;
    private Timer HitTimer = null;

	// Godot Types

	// Basic Types
	public bool hitCheck = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Parent = GetNode<EnemyMovement>(ParentPath);
		AnimePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		AnimeTree = GetNode<AnimationTree>("AnimationTree");
        HitTimer = GetNode<Timer>("Hit Timer");
	}

	public override void _PhysicsProcess(double delta)
	{
		SetRunAnimation();
        CheckForHit();
        CheckHitTimer();
	}

	//-------------------------------------------------------------------------
	// Animation Ctrl Methods

	private void SetRunAnimation() {
		Vector2 LatVelocity = new Vector2(Parent.Velocity.X, Parent.Velocity.Z);
		
		if (LatVelocity.Length() > 0.1f) {
			AnimeTree.Set("parameters/conditions/Idle", false);
			AnimeTree.Set("parameters/conditions/Run", true);
		} else {
            AnimeTree.Set("parameters/conditions/Idle", true);
			AnimeTree.Set("parameters/conditions/Run", false);
		}
	}

    private void CheckForHit() {
        if (Input.IsActionJustPressed("Check Hit")) {
            AnimeTree.Set("parameters/conditions/Hit", true);
            HitTimer.Start();
            hitCheck = true;
        }
    }

    private void CheckHitTimer() {
        if ((HitTimer.TimeLeft <= 0) && hitCheck) {
            AnimeTree.Set("parameters/conditions/Hit", false);
            hitCheck = false;
        }
    }

	//-------------------------------------------------------------------------
	// Demo Methods
}
