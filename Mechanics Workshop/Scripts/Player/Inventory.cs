using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private ItemPickupZone itemPickupArea = null;
	private EquippedItem equippedItemSlot = null;
	private PlayerUI playerUI = null;
	private InventoryUI inventoryUI = null;

	// Godot Types
	List<Item> MainInventory = new List<Item>();
	ulong[,] InventoryGrid;

	// Basic Types
	public float maxInventoryWeight = 50.0f;
	public float currInventoryWeight = 0.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		itemPickupArea = GetNode<ItemPickupZone>("Item Pickup Area");
		equippedItemSlot = GetNode<EquippedItem>("Equipped Item Slot");
		playerUI = GetNode<PlayerUI>("../Head/1st Person Camera/Player UI/Control");
		inventoryUI = GetNode<InventoryUI>("../Head/1st Person Camera/Inventory UI");
		MainInventory = new List<Item>();

		// Init GridInventory, int[Grid Height, Grid Width]
		InventoryGrid = new ulong[inventoryUI.invGridHeight, inventoryUI.invGridWidth];

		CheckCurrInventoryWeight();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Pickup Item")) {
				PickupItem();
			}

			if (eventAction.IsActionPressed("Debug Pring Inventory")) {
				PrintInventoryGrid();
			}
		}
	}

	//-------------------------------------------------------------------------
	// Inventory Methods
	public void PickupItem() {
		DroppedItem NearbyItem = itemPickupArea.GetNearestItem();

		if (NearbyItem == null)
			return;

		// Check with the Inventory Grid
		bool itemAdded = AddItemToInvGrid(NearbyItem.ItemParams);

		if (!itemAdded) {
			GD.Print("No Space!");
			return;
		}

		MainInventory.Add(NearbyItem.ItemParams);

		// Add new item weight to the current inentory weight
		currInventoryWeight += NearbyItem.ItemParams.itemData.weight;

		// Check if the player should auto equip this item
		if (equippedItemSlot.ItemParams == null)
			equippedItemSlot.equipItem(NearbyItem.ItemParams);

		// Remove the item from the nearby items list
		itemPickupArea.RemoveItemFromNearbyList(NearbyItem);

		// Destroy the Dropped item
		NearbyItem.QueueFree();
	}

	public void EquipItem() {
		if (MainInventory[0].itemType == Item.ItemType.Weapon)
			//EquipWeapon((WeaponData) MainInventory[0]);
			return;
	}

	public void EquipWeapon(Item itemData) {
		/*// Store the Currently Held Weapon's Data
		WeaponData tempData = EquippedItemSlot.Params;

		// Update the Player Weapon Slot with the new Data
		EquippedItemSlot.ItemParams.itemData = weaponData;
		EquippedItemSlot.Mesh3D.Mesh = EquippedItemSlot.Params.mesh;

		// Update the Player UI
		EquippedItemSlot.PlyrUI.UpdateAmmoCountLbl(EquippedItemSlot.Params.magazineSize, 
									 EquippedItemSlot.Params.currBullet);
		/**/
	}

	public void CheckCurrInventoryWeight() {
		for (int i = 0; i < MainInventory.Count; i++) {
			currInventoryWeight += MainInventory[i].itemData.weight;
		}
	}

	public bool AddItemToInvGrid(Item itemObj) {
		ulong id = itemObj.GetInstanceId();
		Vector2 itemSpace = itemObj.itemData.GridSpace;
		
		for (int i = 0; i < InventoryGrid.GetLength(0) - itemSpace.X; i++) {
			for (int j = 0; j < InventoryGrid.GetLength(1) - itemSpace.Y; j++) {
				ulong regionValue = SumInventoryRegion(
					itemSpace, 
					new Vector2(i, j));
				
				if (regionValue > 0)
					continue;
				
				UpdateInventoryRegion(itemSpace, new Vector2(i, j), id);
				inventoryUI.AddSpriteToGrid(itemObj.itemData, i, j);
				MainInventory.Add(itemObj);
				return true;
			}
		}

		return false;
	}

	public ulong SumInventoryRegion(Vector2 RegionSize, Vector2 Origin) {
		ulong Sum = 0;
		for (int i = (int) Origin.X; i < Origin.X + RegionSize.X; i++) {
			for (int j = (int) Origin.Y; j < Origin.Y + RegionSize.Y; j++) {
				Sum += InventoryGrid[i, j];
			}
		}
		return Sum;
	}

	public void UpdateInventoryRegion(Vector2 RegionSize, Vector2 Origin, ulong id) {
		for (int i = (int) Origin.X; i < Origin.X + RegionSize.X; i++) {
			for (int j = (int) Origin.Y; j < Origin.Y + RegionSize.Y; j++) {
				InventoryGrid[i, j] = id;
			}
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
	private void PrintInventoryGrid() {
		string GridString = "";
		for (int y = 0; y < InventoryGrid.GetLength(0); y++) {
			string RowString = "";
			for (int x = 0; x < InventoryGrid.GetLength(1); x++) {
				RowString += InventoryGrid[y, x] + " ";
			}
			RowString += "\n";
			GridString += RowString;
		}
		GD.Print(GridString);
	}
}
