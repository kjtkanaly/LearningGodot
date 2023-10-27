using Godot;
using System;

public partial class Demo3D : Node3D
{
	//-------------------------------------------------------------------------
    // Game Componenets
	Node3D player;

    // Godot Types

    // Basic Types
	public const float rotateSpeed = 0.5f; 
	public bool PauseDemo = true;
	[Export] bool DemoActive = false;

    //-------------------------------------------------------------------------
    // Game Events
	public override void _Ready() {
		player = GetNode<Node3D>("../Player");
	}

	public override void _Process(double delta) {
		if (!DemoActive)
		return;

		ToggleDemo();

		if (!PauseDemo) 
		return;

		DemoReel1((float) delta);
	}

    //-------------------------------------------------------------------------
    // Demo3D Methods
	public void ToggleDemo() {
		if (Input.IsActionJustPressed("Toggle Demo"))
		PauseDemo = !PauseDemo;
	}

	public void DemoReel1(float delta) {
		player.RotateY(rotateSpeed * delta);
	}

    //-------------------------------------------------------------------------
    // Demo Methods
}
