using Godot;
using System;

public partial class ItemPanel : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	[Export] Texture2D Idle;
	[Export] Texture2D Hover;
	public Sprite2D Sprite;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("Sprite2D");

		MouseEntered += Highlight;
		MouseExited += SetIdle;
	}

	//-------------------------------------------------------------------------
	// Item Panel Methods
	public void Highlight() {
		Sprite.Texture = Hover;
	}

	public void SetIdle() {
		Sprite.Texture = Idle;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
