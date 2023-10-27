using Godot;
using System;

public partial class RangeWeapon : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node3D Head;
	private Camera3D ActiveCamera = null;
	private Viewport View;
	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		View = GetViewport();
	}

	public override void _PhysicsProcess(double delta) {

	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	public void SetActiveCamera3D(Camera3D FP, Camera3D TP){
		if (FP.Current) {
			ActiveCamera = FP;
		} else {
			ActiveCamera = TP;
		}

		return;
	}

	public void ShootBullet() {
		if (ActiveCamera == null)
		GD.Print("Null Camera");

		Vector3 from = ActiveCamera.ProjectRayOrigin(View.GetMousePosition());
		Vector3 to = from + ActiveCamera.ProjectRayNormal(View.GetMousePosition()) * rayLength;

		GD.Print(ActiveCamera.Name);
		GD.Print(to);
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
