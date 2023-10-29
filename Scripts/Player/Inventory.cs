using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets

    // Godot Types
	List<List<Item>> inventory = new List<List<Item>>();

    // Basic Types
	const int inventorySlots = 30;

    //-------------------------------------------------------------------------
    // Game Events
	public override void _Ready()
	{
		inventory = new List<List<Item>>(inventorySlots);
	}

    //-------------------------------------------------------------------------
    // <Inventory> Methods

    //-------------------------------------------------------------------------
    // Demo Methods
}