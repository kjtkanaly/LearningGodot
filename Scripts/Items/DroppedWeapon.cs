using Godot;
using System;

public partial class DroppedWeapon : DroppedItem
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public MeshInstance3D Mesh3D = null;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		Mesh3D = GetNode<MeshInstance3D>("Model");
		Mesh3D.Mesh = ItemParams.itemData.mesh;
	}

	//-------------------------------------------------------------------------
	// Dropped Weapon Methods

	//-------------------------------------------------------------------------
	// Demo Methods
}
