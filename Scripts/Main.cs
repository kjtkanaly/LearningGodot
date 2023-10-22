using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta)
	{
		TogglePause();
	}

	//-------------------------------------------------------------------------
	// Main Methods
	public void TogglePause() {
		if (Input.IsActionJustPressed("Pause Game")) {
			if (!GetTree().Paused) {
				Input.MouseMode = Input.MouseModeEnum.Visible;
				GetTree().Paused = true;
			} else {
				Input.MouseMode = Input.MouseModeEnum.Captured;
				GetTree().Paused = false;
			}
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
