using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	public List<Node3D> Bullets = new List<Node3D>();
	[Export] public PackedScene bulletPrefab = null;

	// Basic Types
	[Export] public int BulletInstanceCount = 30;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		PreLoadNode();
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

	public void PreLoadNode() {
		
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
