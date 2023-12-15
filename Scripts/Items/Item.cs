using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
	public enum ItemType {
		Default = 0,
		Weapon = 1,
		Basic = 2
	}

	//-------------------------------------------------------------------------
	// Game Componenets
	[Export] public GenericItemData itemData = null;
	[Export] public WeaponData weaponData = null;

	// Godot Types
	[Export] public ItemType itemType = ItemType.Default;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Item Methods

	//-------------------------------------------------------------------------
	// Demo Methods
}
