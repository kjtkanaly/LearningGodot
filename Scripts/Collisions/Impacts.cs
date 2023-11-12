using Godot;
using System;

public partial class Impacts : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
	public EnemyMovement EM = null;

    // Godot Types

    // Basic Types

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        EM = GetNode<EnemyMovement>("../");
    }

    //-------------------------------------------------------------------------
    // Impacts Methods

    //-------------------------------------------------------------------------
    // Demo Methods
}