using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class GenericInventoryItem : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Sprite2D Item = null;
	public NinePatchRect Highlight = null;
	Texture2D sprite = null;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Item = GetNode<Sprite2D>("Item");
		Highlight = GetNode<NinePatchRect>("Highlight");

		// this.Visible = false;
		sprite = Item.Texture;

		MouseEntered += HighlightItem;
		MouseExited += DeHighlightItem;

		DeHighlightItem();
	}

	//-------------------------------------------------------------------------
	// Generic Inventory Item Methods
	public void HighlightItem() {
		Color c = Highlight.Modulate;
		c.A = 1.0f;
		Highlight.Modulate = c;
		GD.Print("Highlight");
	}

	public void DeHighlightItem() {
		Color c = Highlight.Modulate;
		c.A = 0.0f;
		Highlight.Modulate = c;
		GD.Print("DeHighlight");
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
