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
		Head = GetNode<Node3D>("../Head");
	}

	public override void _PhysicsProcess(double delta) {
		PointTowardsCursor();
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	private void PointTowardsCursor() {
		RotationDegrees = Head.RotationDegrees;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
