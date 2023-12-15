using Godot;
using System;
using System.Collections.Generic;

public partial class ItemPickupZone : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types
	public List<DroppedItem> NearbyItems;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
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

	public Item PickupItem() {
		if (NearbyItems.Count == 0) {
			GD.Print("Nothing nearby!");
			return null;
		}

		return NearbyItems[0].ItemParams;
	}

	public void DropWeapon(DroppedWeapon dropWeapon, WeaponData data) {
		// Update the newly dropped weapon's data
		// dropWeapon.ItemParams = data;
		// dropWeapon.Mesh3D.Mesh = data.mesh;
	}

	public void DestroyNearbyWeapon(DroppedWeapon NearbyWeapon) {
		NearbyWeapon.QueueFree();
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
