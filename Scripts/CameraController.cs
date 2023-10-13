using Godot;
using System;

public partial class CameraController : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Camera3D FP, TP;

	// Unity Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		FP = GetOwner<Node3D>().GetNode<Camera3D>("1st Person Camera");
		TP = GetOwner<Node3D>().GetNode<Camera3D>("3rd Person Camera");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Change Camera")) {
			if (TP.Current) {
				TP.ClearCurrent();
				FP.MakeCurrent();
			} else {
				FP.ClearCurrent();
				TP.MakeCurrent(); 
			}
		}
	}

	//-------------------------------------------------------------------------
	// Player3D Methods


	//-------------------------------------------------------------------------
	// Demo Methods

}
