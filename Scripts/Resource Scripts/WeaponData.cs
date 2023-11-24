using Godot;
using System;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] public float damage = 1.0f;
    [Export] public float reloadTime = 1.0f;
    [Export] public int magazineSize = 12;
}
