using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets

    // Godot Types
	List<ItemData> MainInventory = new List<ItemData>();

    // Basic Types
	const int inventorySlots = 30;

    //-------------------------------------------------------------------------
    // Game Events
	public override void _Ready()
	{
		MainInventory = new List<ItemData>();
	}

    //-------------------------------------------------------------------------
    // Inventory Methods
    public void PickupItem() {
        
    }

    //-------------------------------------------------------------------------
    // Demo Methods
}