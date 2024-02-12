using Godot;
using System;
using System.Collections.Generic;

// This class should handle things such as
// - Pause Game
// - Quit Game
// - Resume Game
// - Scene Control
public partial class PlayerSceneDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private Timer QuitGameTimer = null;
	private PlayerUI PlayerUICtrl = null;
	private InventoryUI InventoryUICtrl = null;

	// Godot Types

	// Basic Types
	private const float quitGameDelay = 2.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		QuitGameTimer = GetNode<Timer>("Quit Game");
		PlayerUICtrl = GetNode<PlayerUI>(
			"../Camera Director/First Person Camera/UI Director/Player UI");
		InventoryUICtrl = GetNode<InventoryUI>(
			"../Camera Director/First Person Camera/UI Director/Inventory UI");

		Input.MouseMode = Input.MouseModeEnum.Captured;

		QuitGameTimer.Timeout += QuitGame;
	}

	public override void _Process(double delta)
	{
		ToggleQuitGame();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Pause Game") &&
				!InventoryUICtrl.isOpen) {
				TogglePause();
			} else if (eventAction.IsActionPressed("Inventory")) {
				ToggleInventoryUI();
				PlayerUICtrl.Toggle();
			} /**/
		}
	}

	//-------------------------------------------------------------------------
	// Player Scene Director Methods
	public void TogglePause() {
		if (!GetTree().Paused) {
			PauseGame();
		} else {
			ResumeGame();
		}
	}

	private void PauseGame() {
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetTree().Paused = true;

		GD.Print("Pause Game");
	}

	private void ResumeGame() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().Paused = false;
	}

	public void ToggleQuitGame() {
		if (Input.IsActionJustPressed("Quit Game"))
			QuitGameTimer.Start(quitGameDelay);
		if (Input.IsActionJustReleased("Quit Game"))
			QuitGameTimer.Stop();
	}

	private void QuitGame() {
		GetTree().Quit();
	}

	public void ToggleInventoryUI() {
		if (InventoryUICtrl.isOpen) {
			InventoryUICtrl.Close();
		} else {
			if (GetTree().Paused)
				return;

			InventoryUICtrl.Open();
		} /**/
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
