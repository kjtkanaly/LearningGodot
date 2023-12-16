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

	public DroppedItem GetNearestItem() {
		if (NearbyItems.Count == 0) {
			GD.Print("Nothing nearby!");
			return null;
		}

		return NearbyItems[0];
	}

	public void RemoveItemFromNearbyList(DroppedItem item) {
		NearbyItems.Remove(item);
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
