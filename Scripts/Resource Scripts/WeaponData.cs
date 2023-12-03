using Godot;
using System;

[GlobalClass]
public partial class WeaponData : ItemData
{
	[Export] public float damage = 1.0f;
	[Export] public float reloadTime = 1.0f;
	[Export] public int magazineSize = 12;
	[Export] public int currBullet = 0;
}
