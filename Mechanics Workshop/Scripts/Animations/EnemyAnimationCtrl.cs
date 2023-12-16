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
    private Timer DeathTimer = null;

	// Godot Types

	// Basic Types
	public bool CanInteruptAnime = true;
    private float HitTime = 0.0f;
    private float DeathTime = 0.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Parent = GetNode<EnemyMovement>(ParentPath);
		AnimePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        HitTimer = GetNode<Timer>("Hit Timer");
        DeathTimer = GetNode<Timer>("Death Timer");
        BulletArea = GetNode<Area3D>("../Bullet Area");

        HitTimer.Timeout += StopHitAnimation;
        DeathTimer.Timeout += StopDeathAnimation;

        HitTime = AnimePlayer.GetAnimation("Hit").Length;
        DeathTime = AnimePlayer.GetAnimation("Death").Length;
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

    public void PlayHitAnimation() {
        PreviousAnime = AnimePlayer.CurrentAnimation;
        AnimePlayer.CurrentAnimation = "Hit";
        CanInteruptAnime = false;
        HitTimer.Start(HitTime);
    }

    private void StopHitAnimation() {
        AnimePlayer.CurrentAnimation = PreviousAnime;
        CanInteruptAnime = true;
    }

    public void PlayDeathAnimation() {
        AnimePlayer.CurrentAnimation = "Death";
        CanInteruptAnime = false;
        DeathTimer.Start(DeathTime);
    }

    private void StopDeathAnimation() {
        GetTree().QueueDelete(Parent);
    }

	//-------------------------------------------------------------------------
	// Demo Methods
}
