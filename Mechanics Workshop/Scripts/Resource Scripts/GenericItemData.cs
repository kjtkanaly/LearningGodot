using Godot;
using System;

[GlobalClass]
public partial class GenericItemData : Resource
{
    [Export] public Mesh mesh = null;
    [Export] public float weight = 1.0f;
}
