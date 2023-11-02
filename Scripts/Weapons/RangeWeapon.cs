using Godot;
using System;

public partial class RangeWeapon : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public CameraMovement CM;
	public Node3D Head;
	private Camera3D ActiveCamera = null;
	private Viewport View;
	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		CM = GetParent<CameraMovement>();
		

		GetActiveCamera();
	}

	public override void _PhysicsProcess(double delta) {
		GetActiveCamera();

		Rotation = ActiveCamera.Rotation;
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	public void GetActiveCamera() {
		ActiveCamera = CM.ActiveCamera;
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
