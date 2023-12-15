using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    private ItemPickupZone ItemPickupArea = null;
    private EquippedItem EquippedItemSlot = null;

    // Godot Types
	List<Item> MainInventory = new List<Item>();

    // Basic Types
	const int inventorySlots = 30;

    //-------------------------------------------------------------------------
    // Game Events
	public override void _Ready()
	{
        ItemPickupArea = GetNode<ItemPickupZone>("Item Pickup Area");
        EquippedItemSlot = GetNode<EquippedItem>("../Equipped Item Slot");
		MainInventory = new List<Item>();
	}

    //-------------------------------------------------------------------------
    // Inventory Methods
    public void PickupItem() {
        Item NearbyItemData = ItemPickupArea.PickupItem();
        MainInventory.Add(NearbyItemData);
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

    //-------------------------------------------------------------------------
    // Demo Methods
}