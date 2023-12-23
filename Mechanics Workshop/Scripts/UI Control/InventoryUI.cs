using Godot;
using System;

public partial class InventoryUI : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public GridContainer gridUI = null;
	public Panel DispItemPnl = null;
	public PackedScene ItemPanel = (PackedScene) GD.Load(
		"res://Mechanics Workshop/UI/ItemPanel.tscn");

	// Godot Types
	[Signal]
	public delegate void OpenedEventHandler();
	[Signal]
	public delegate void ClosedEventHandler();
	private Main main = null;

	// Basic Types
	public Vector2[,] GridPos = null;
	private int invGridWidth = 8;
	private int invGridHeight = 4;
	public bool isOpen = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		DispItemPnl = GetNode<Panel>("NinePatchRect/Display Item Panel");

		GridPos = new Vector2[invGridHeight, invGridWidth];
		CreateInventoryGrid();

		DispItemPnl.GuiInput += GetSlotItem;

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

	public void CreateInventoryGrid() {
		Control InventoryCtrl = (Control) DispItemPnl;
		Vector2 InventoryDims = InventoryCtrl.Size;

		GD.Print($"Inventory Size{InventoryCtrl.Size}");
		GD.Print($"i: {GridPos.GetLength(1)}, j: {GridPos.GetLength(0)}");
		GD.Print($"{GridPos[invGridHeight - 1, invGridWidth - 1]}");

		for (int i = 0; i < GridPos.GetLength(0); i++) {
			for (int j = 0; j < GridPos.GetLength(1); j++) {
				// Create the Item Panel
				Control ItemPanelCtrl = (Control) ItemPanel.Instantiate();

				GridPos[i, j] = GetGridPos(
					i, j, 
					InventoryDims, ItemPanelCtrl.Size);

				SetItemPanelPos(ItemPanelCtrl , GridPos[i, j]);

				// Attach Signal to Panel GUI interactions
				// ItemPanelCtrl.GuiInput += GetSlotItem;

				// Add Item Panel as child of Invetory
				InventoryCtrl.AddChild(ItemPanelCtrl);
			}
		}
	}

	private Vector2 GetGridPos(int i, int j, Vector2 InventoryDims, Vector2 ItemPanelDims) {
		// Get the panel's pos
		float ii = i * (InventoryDims.X / invGridWidth);
				   // + (ItemPanelDims.X / 2);
		float jj = j * (InventoryDims.Y / invGridHeight);
				   // + (ItemPanelDims.Y / 2);

		GD.Print($"{i}, {j}: ({ii}, {jj})");

		return new Vector2(ii, jj);
	}

	private void SetItemPanelPos(Control ItemPanelCtrl, Vector2 ItemPanelPos) {
		ItemPanelCtrl.Position = new Vector2(
			ItemPanelPos.Y, 
			ItemPanelPos.X);
	}

	public void GetSlotItem(InputEvent ev) {
		if (@ev is InputEventMouseButton eventMouseButton) {
			if (eventMouseButton.ButtonIndex == MouseButton.Left
				&& eventMouseButton.IsPressed()) {
			
				Vector2 SlotCord = GetItemSlotCord(eventMouseButton.Position);
				GD.Print($"Slot: {SlotCord.X}, {SlotCord.Y} | {eventMouseButton.Position}");
			}
		}
	}

	public Vector2 GetItemSlotCord(Vector2 MousePos) {
		int ii = 0;
		int jj = 0;

		for (int i = 1; i < GridPos.GetLength(0); i++) {
			//GD.Print($"{GridPos[i, 0].X}");
			if (MathF.Abs(MousePos.Y - GridPos[i, 0].X) < MathF.Abs(MousePos.Y - GridPos[ii, 0].X)) {
				ii = i;
			}
		}

		// GD.Print(MousePos.X);
		for (int j = 1; j < GridPos.GetLength(1); j++) {
			// GD.Print($"{MousePos.X} - {GridPos[0, j].Y} = {(MousePos.X - GridPos[0, j].Y)}");
			if (MathF.Abs(MousePos.X - GridPos[0, j].Y) < MathF.Abs(MousePos.X - GridPos[0, jj].Y)) {
				jj = j;
			}
		}

		return new Vector2(ii, jj);
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
