using Godot;
using System;

public partial class InventoryUI : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	[Signal]
	public delegate void OpenedEventHandler();
	[Signal]
	public delegate void ClosedEventHandler();
	private Main main = null;

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
		EmitSignal(SignalName.Opened);
	}

	public void Close() {
		Visible = false;
		isOpen = false;
		EmitSignal(SignalName.Closed);
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
