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
	[Export] public int magazineSize = 12;
	public int currentBullet = 0;
	public bool outOfBullets = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		CM = GetParent<CameraMovement>();
		GetActiveCamera();
		currentBullet = magazineSize;
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

	public int IncrementBulletCount() {
		currentBullet -= 1;

		IsMagazineEmpty();

		return currentBullet;
	}

	public bool IsMagazineEmpty() {
		if (currentBullet <= 0) {
			outOfBullets = true;
		}
		return outOfBullets;
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
