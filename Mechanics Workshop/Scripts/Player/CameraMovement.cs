using Godot;
using Godot.Collections;
using System;

public partial class CameraMovement : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Camera3D FP = null, TP = null, ActiveCamera = null;
	private PlayerData PD;
	private Node3D Player;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		PD = GetNode<PlayerData>("../Player Data");
		FP = GetNode<Camera3D>("1st Person Camera");
		TP = GetNode<Camera3D>("3rd Person Camera");
		Player = GetParent<Node3D>();

		// Set the active Camera
		SetActiveCamera();
	}

	public override void _Process(double delta)
	{
		SwitchCamera();
	}

	public override void _UnhandledInput(InputEvent ev)
	{
		if (@ev is InputEventMouseMotion eventMouseMotion) {
			MouseCameraMovement(eventMouseMotion);
		}
	}

	//-------------------------------------------------------------------------
	// Camera Movement Methods
	public void MouseCameraMovement(InputEventMouseMotion mouseMotion) {
		Player.RotateY(-mouseMotion.Relative.X * PD.movementData.mouseSensitivity);
		RotateX(mouseMotion.Relative.Y * PD.movementData.mouseSensitivity);

		// Clamp the X-Rotation
		Vector3 rotation = Rotation;
		rotation.X = Mathf.Clamp(rotation.X, 
								 -Mathf.Pi/2 + ActiveCamera.Rotation.X, 
								 Mathf.Pi/2 + ActiveCamera.Rotation.X
								 );
		Rotation = rotation;
	}

	public void SwitchCamera() {
		if (Input.IsActionJustPressed("Change Camera")) {
			if (TP.Current) {
				TP.ClearCurrent();
				FP.MakeCurrent();
			} else {
				FP.ClearCurrent();
				TP.MakeCurrent(); 
			}
		}

		SetActiveCamera();
	}

	private void SetActiveCamera() {
		if (FP.Current) {
			ActiveCamera = FP;
		} else {
			ActiveCamera = TP;
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
