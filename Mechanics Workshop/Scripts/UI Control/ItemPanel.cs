using Godot;
using System;

public partial class ItemPanel : Control
{
	public enum Mode {
		Idle = 0,
		FilledValid = 1
	}

	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	[Export] public Texture2D Idle;
	[Export] public Texture2D Hover;
	[Export] public Texture2D FilledValid;
	public Sprite2D Sprite;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("Sprite2D");

		// MouseEntered += Highlight;
		// MouseExited += SetIdle;
	}

	//-------------------------------------------------------------------------
	// Item Panel Methods
	public void Highlight() {
		Sprite.Texture = Hover;
	}

	public void SetIdle() {
		Sprite.Texture = Idle;
	}

	public Texture2D GetModePanel(Mode panelMode) {
		if (panelMode == Mode.FilledValid) {
			return FilledValid;
		} else {
			return Idle;
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
