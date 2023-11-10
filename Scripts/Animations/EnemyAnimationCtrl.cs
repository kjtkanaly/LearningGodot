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

	// Godot Types

	// Basic Types
	

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Parent = GetNode<EnemyMovement>(ParentPath);
		AnimePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		AnimeTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _PhysicsProcess(double delta)
	{
		SetRunAnimation();
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


	//-------------------------------------------------------------------------
	// Demo Methods
}
