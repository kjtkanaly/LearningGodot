using Godot;
using System;

public partial class Bullet : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public MeshInstance3D mesh = null;
	public RayCast3D ray = null;

	// Godot Types

	// Basic Types
	public const float speed = 200.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		ray = GetNode<RayCast3D>("RayCast3D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Transform.Basis * new Vector3(0, 0, -speed) * (float) delta;
		
	}

	//-------------------------------------------------------------------------
	// Bullet Methods

	//-------------------------------------------------------------------------
	// Demo Methods
}
