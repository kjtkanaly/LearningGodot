using Godot;
using System;

public partial class RangeWeapon : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Player3D Player;
	public Node3D Head;
	private Camera3D ActiveCamera = null;
	private Viewport View;
	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		Player = GetNode<Player3D>("../..");

		GetActiveCamera();
	}

	public override void _PhysicsProcess(double delta) {
		GetActiveCamera();

		Rotation = ActiveCamera.Rotation;
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	public void GetActiveCamera() {
		ActiveCamera = Player.ActiveCamera;
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
