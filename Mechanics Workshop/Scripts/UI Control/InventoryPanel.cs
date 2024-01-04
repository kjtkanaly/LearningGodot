using Godot;
using System;

public partial class InventoryPanel : Control
{
	public enum Mode {
		Idle = 0,
		FilledAndUsable = 1,
		FilledAndNotUsable = 2
	}

	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	[Export] public Texture2D Idle;
	[Export] public Texture2D Hover;
	[Export] public Texture2D FilledAndValid;
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
	// Inventory Panel Methods
	public void Highlight() {
		Sprite.Texture = Hover;
	}

	public void SetIdle() {
		Sprite.Texture = Idle;
	}

	public Texture2D GetModePanel(Mode panelMode) {
		if (panelMode == Mode.FilledAndUsable) {
			return FilledAndValid;
		} else {
			return Idle;
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
