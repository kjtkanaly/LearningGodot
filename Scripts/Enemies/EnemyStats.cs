using Godot;
using System;

public partial class EnemyStats : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
	private EnemyAnimationCtrl AnimeCtrl = null;
	private Area3D BulletArea = null;

    // Godot Types

    // Basic Types
	[Export] public int MaxHealth = 100;
	public int CurrentHealth = 0;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
		CurrentHealth = MaxHealth;

        AnimeCtrl = GetNode<EnemyAnimationCtrl>("../Animation Ctrl");
		BulletArea = GetNode<Area3D>("../Bullet Area");

		BulletArea.AreaEntered += TakeDamage;
    }

    //-------------------------------------------------------------------------
    // Enemy Stats Methods
	private void TakeDamage(Area3D RxArea) {
		CurrentHealth -= 50;

		if (CurrentHealth <= 0) {
			AnimeCtrl.PlayDeathAnimation();
		} else {
			AnimeCtrl.PlayHitAnimation();
		}
	}

    //-------------------------------------------------------------------------
    // Demo Methods
}