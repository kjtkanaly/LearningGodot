using Godot;
using System;

[GlobalClass]
public partial class GenericItemData : Resource
{
	[Export] public Mesh mesh = null;
	[Export] public Texture2D inventorySprite = null;
	[Export] public float weight = 1.0f;

	// Vector2(Number of Rows, Number of Cols)
	[Export] public Vector2 GridSpace = new Vector2(1, 1);
}
