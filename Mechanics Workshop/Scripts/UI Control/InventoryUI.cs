using Godot;
using System;

public partial class InventoryUI : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public GridContainer gridUI = null;
	public Panel DispItemPnl = null;
	public Panel TouchPnl = null;
	public PackedScene ItemPanelPreFab = (PackedScene) GD.Load(
		"res://Mechanics Workshop/UI/ItemPanel.tscn");
	public PackedScene InventorySprite = (PackedScene) GD.Load(
		"res://Mechanics Workshop/UI/inventory_sprite_generic.tscn");

	// Godot Types
	[Signal]
	public delegate void OpenedEventHandler();
	[Signal]
	public delegate void ClosedEventHandler();
	private Main main = null;
	public ItemPanel[,] InventoryPanels = null;

	// Basic Types
	public Vector2[,] GridPos = null;
	public Vector2 ItemPanelDims = Vector2.Zero;
	public int invGridWidth = 8;
	public int invGridHeight = 4;
	public bool isOpen = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		DispItemPnl = GetNode<Panel>("NinePatchRect/Display Item Panel");
		
		InventoryPanels = new ItemPanel[invGridHeight, invGridWidth];
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

		for (int i = 0; i < GridPos.GetLength(0); i++) {
			for (int j = 0; j < GridPos.GetLength(1); j++) {
				// Create the Item Panel and Add it to the array
				ItemPanel NewItemPanel = (ItemPanel) ItemPanelPreFab.Instantiate();
				InventoryPanels[i, j] = NewItemPanel;

				Control ItemPanelCtrl = (Control) NewItemPanel;
				ItemPanelDims = ItemPanelCtrl.Size;

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

		// GD.Print($"{i}, {j}: ({ii}, {jj})");

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
				GD.Print(eventMouseButton.Position);
			
				Vector2 SlotCord = GetItemSlotCord(eventMouseButton.Position);
				// GD.Print("\n");
				GD.Print($"Slot: {SlotCord.X}, {SlotCord.Y} | {eventMouseButton.Position} | {GridPos[(int)SlotCord.X, (int)SlotCord.Y]}");
			}
		}
	}

	public Vector2 GetItemSlotCord(Vector2 MousePos) {
		int ii = 0;
		int jj = 0;

		float minDiff = MathF.Abs(MousePos.Y - (GridPos[ii, 0].X + (ItemPanelDims.X / 2)));
		for (int i = 1; i < GridPos.GetLength(0); i++) {
			float diff = MathF.Abs(
				MousePos.Y - (GridPos[i, 0].X + (ItemPanelDims.X / 2)));
			
			if (diff < minDiff) {
				ii = i;
				minDiff = diff;
			}
		}
		
		minDiff = MathF.Abs(MousePos.X - (GridPos[0, jj].Y + (ItemPanelDims.Y / 2)));
		for (int j = 1; j < GridPos.GetLength(1); j++) {
			float diff = MathF.Abs(
				MousePos.X - (GridPos[0, j].Y + (ItemPanelDims.Y / 2)));
			
			if (diff < minDiff) {
				jj = j;
				minDiff = diff;
			}
		}

		return new Vector2(ii, jj);
	}

	public void AddSpriteToGrid(GenericItemData item, int i, int j) {
		GenericInventoryItem InventoryItem = 
			(GenericInventoryItem) InventorySprite.Instantiate();

		Control InventoryCtrl = (Control) DispItemPnl;
		InventoryCtrl.AddChild(InventoryItem);

		InventoryItem.Position = new Vector2(GridPos[i, j].Y, GridPos[i, j].X);
		InventoryItem.Highlight.Scale = item.GridSpace;
		InventoryItem.Item.Texture = item.inventorySprite;

		// Update the appropriate panels to be filled
		UpdateInventoryPanels(ItemPanel.Mode.FilledValid, 
							  new Vector2(i, j), 
							  item.GridSpace);

		GD.Print($"{i}, {j}: {GridPos[i, j]}");
	}

	public void UpdateInventoryPanels(ItemPanel.Mode panelMode, Vector2 Origin, Vector2 Size) {
		for (int i = (int) Origin.X; i < Origin.X + Size.X; i++) {
			for (int j = (int) Origin.Y; j < Origin.Y + Size.Y; j++) {
				Texture2D PanelTexture = 
					InventoryPanels[i, j].GetModePanel(panelMode);

				InventoryPanels[i, j].Sprite.Texture = PanelTexture;
			}
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
