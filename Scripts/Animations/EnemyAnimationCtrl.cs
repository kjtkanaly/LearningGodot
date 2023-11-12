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
    private String PreviousAnime = null;
    private Area3D BulletArea = null;
    private Timer HitTimer = null;

	// Godot Types

	// Basic Types
	public bool CanInteruptAnime = true;
    private float HitTime = 0.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Parent = GetNode<EnemyMovement>(ParentPath);
		AnimePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        HitTimer = GetNode<Timer>("Hit Timer");
        BulletArea = GetNode<Area3D>("../Bullet Area");

        BulletArea.AreaEntered += PlayHitAnimation;
        HitTimer.Timeout += StopHitAnimation;
        HitTime = AnimePlayer.GetAnimation("Hit").Length;
	}

	public override void _PhysicsProcess(double delta)
	{
        if (!CanInteruptAnime)
            return;

		SetRunAnimation();
	}

	//-------------------------------------------------------------------------
	// Animation Ctrl Methods

	private void SetRunAnimation() {
		Vector2 LatVelocity = new Vector2(Parent.Velocity.X, Parent.Velocity.Z);
		
		if (LatVelocity.Length() > 0.1f) {
			AnimePlayer.CurrentAnimation = "Run";
		} else {
            AnimePlayer.CurrentAnimation = "Idle";
		}
	}

    private void PlayHitAnimation(Area3D RxArea) {
        GD.Print($"Rx Area Name: {RxArea.Name}");
        PreviousAnime = AnimePlayer.CurrentAnimation;
        AnimePlayer.CurrentAnimation = "Hit";
        CanInteruptAnime = false;
        HitTimer.Start(HitTime);
    }

    private void StopHitAnimation() {
        AnimePlayer.CurrentAnimation = PreviousAnime;
        CanInteruptAnime = true;
    }

	//-------------------------------------------------------------------------
	// Demo Methods
}
