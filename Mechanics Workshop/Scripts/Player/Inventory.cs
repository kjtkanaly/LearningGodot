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

		CheckCurrInventoryWeight();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Pickup Item")) {
				PickupItem();
			}
		}
	}

	//-------------------------------------------------------------------------
	// Inventory Methods
	public void PickupItem() {
		DroppedItem NearbyItem = itemPickupArea.GetNearestItem();

		if (NearbyItem == null)
			return;

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

	//-------------------------------------------------------------------------
	// Demo Methods
}
