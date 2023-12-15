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
	public GenericItemData itemData = null;
	public WeaponData weaponData = null;

	// Godot Types
	public ItemType itemType = ItemType.Default;

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Item Methods

	//-------------------------------------------------------------------------
	// Demo Methods
}
