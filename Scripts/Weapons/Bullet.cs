using Godot;
using System;

public partial class Bullet : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public MeshInstance3D mesh = null;
	public RayCast3D ray = null;
	public Timer Despawn = null;

	// Godot Types

	// Basic Types
	public const float speed = 200.0f;
	public double timeLeft = 1.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		ray = GetNode<RayCast3D>("RayCast3D");
		Despawn = GetNode<Timer>("Despawn");
	}

	public override void _PhysicsProcess(double delta)
	{
		timeLeft = Despawn.TimeLeft;

		if (timeLeft <= 0.0)
		DespawnBullet();

		Position += Transform.Basis * new Vector3(0, 0, -speed) * (float) delta;		
	}

	//-------------------------------------------------------------------------
	// Bullet Methods
	public void DespawnBullet() {
		GD.Print("Despawn");
		QueueFree();
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
