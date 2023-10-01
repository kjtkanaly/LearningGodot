using Godot;
using System;

public partial class World : Node2D
{
	CollisionPolygon2D collPolygon2D;
	Polygon2D polygon2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		collPolygon2D = GetNode<CollisionPolygon2D>("StaticBody2D/CollisionPolygon2D");
		polygon2D = GetNode<Polygon2D>("StaticBody2D/CollisionPolygon2D/Polygon2D");

		polygon2D.Polygon = collPolygon2D.Polygon;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
