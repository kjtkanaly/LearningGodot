using Godot;
using System;

public partial class ItemPickup : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets


    // Godot Types

    // Basic Types

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        AreaEntered += ItemInPlayerRange;
    }

    //-------------------------------------------------------------------------
    // Item Pickup Methods
	public void ItemInPlayerRange(Area3D RxArea) {
		GD.Print("Player can pickup: " + RxArea.Name);
	}

    //-------------------------------------------------------------------------
    // Demo Methods
}