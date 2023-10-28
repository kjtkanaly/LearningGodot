using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Timer QuitGameTimer = null;

	// Godot Types

	// Basic Types
	public bool pausedGame = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		QuitGameTimer = GetNode<Timer>("Quit Game");
	}

	public override void _Process(double delta)
	{
		TogglePause();

		QuitGame();
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

	public void QuitGame() {
		if (Input.IsActionJustPressed("Quit Game"))
			QuitGameTimer.Start();
		if (Input.IsActionJustReleased("Quit Game"))
			QuitGameTimer.Stop();
		
		if (Input.IsActionPressed("Quit Game") && (QuitGameTimer.TimeLeft <= 0))
			GetTree().Quit();
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
