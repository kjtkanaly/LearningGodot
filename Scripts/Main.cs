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
	private const float quitGameDelay = 2.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		QuitGameTimer = GetNode<Timer>("Quit Game");

		QuitGameTimer.Timeout += QuitGame;
	}

	public override void _Process(double delta)
	{
		TogglePause();
		ToggleQuitGame();
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

	public void ToggleQuitGame() {
		if (Input.IsActionJustPressed("Quit Game"))
			QuitGameTimer.Start(quitGameDelay);
		if (Input.IsActionJustReleased("Quit Game"))
			QuitGameTimer.Stop();
	}

	public void QuitGame() {
		GetTree().Quit();
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
