using Godot;
using System;

public partial class RangeWeapon : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node3D Head;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		
	}

	public override void _PhysicsProcess(double delta) {
		ShootBullet();
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods

	public void ShootBullet() {
		
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
