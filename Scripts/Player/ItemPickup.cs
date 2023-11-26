using Godot;
using System;
using System.Collections.Generic;

public partial class ItemPickup : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public RayCast3D PlayerLOS = null;
	private PrimaryAttack PA = null;

	// Godot Types
	public List<DroppedItem> NearbyItems;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		PA = GetNode<PrimaryAttack>("../Primary Attack");
		PlayerLOS = GetNode<RayCast3D>("../Head/1st Person Camera/Iteract with items");
		// PlayerLOS = GetNode<RayCast3D>("../Head/Rifle/Iteract with items");

		NearbyItems = new List<DroppedItem>();

		AreaEntered += ItemInPlayerRange;
		AreaExited += ItemOutPlayerRange;
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
	// Item Pickup Methods
	public void ItemInPlayerRange(Area3D RxArea) {
		DroppedItem NearbyItem = RxArea.GetNode<DroppedItem>("..");

		NearbyItems.Add(NearbyItem);
	}

	public void ItemOutPlayerRange(Area3D RxArea) {
		DroppedItem NearbyItem = RxArea.GetNode<DroppedItem>("..");

		NearbyItems.Remove(NearbyItem);
	}

	public void PickupItem() {
		if (NearbyItems.Count == 0) {
			GD.Print("Nothing nearby!");
			return;
		}

		if (NearbyItems[0].itemType == DroppedItem.ItemType.Weapon) {
			PickupWeapon((DroppedWeapon) NearbyItems[0]);
		}
	}

	public void PickupWeapon(DroppedWeapon NearbyWeapon) {
		// Store the Currently Held Weapon's Data
		WeaponData tempData = PA.RangeWeaponSlot.Params;

		// Update the Player Weapon Slot with the new Data
		PA.RangeWeaponSlot.Params = NearbyWeapon.Params;
		PA.RangeWeaponSlot.Mesh3D.Mesh = PA.RangeWeaponSlot.Params.mesh;

		// Update the newly dropped weapon's data
		NearbyWeapon.Params = tempData;
		NearbyWeapon.Mesh3D.Mesh = tempData.mesh;

		// Update the Player UI
		PA.PlyrUI.UpdateAmmoCountLbl(PA.RangeWeaponSlot.Params.magazineSize, 
									 PA.RangeWeaponSlot.Params.currBullet);
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
