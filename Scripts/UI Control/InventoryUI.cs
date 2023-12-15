using Godot;
using System;

public partial class InventoryUI : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets

    // Godot Types

    // Basic Types
    public bool isOpen = false;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Close();
    }

    //-------------------------------------------------------------------------
    // Inventory UI Methods
    public void Open() {
        Visible = true;
        isOpen = true;
    }

    public void Close() {
        Visible = false;
        isOpen = false;
    }

    //-------------------------------------------------------------------------
    // Demo Methods
}