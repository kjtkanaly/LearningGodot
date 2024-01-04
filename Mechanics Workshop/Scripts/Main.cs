using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Timer QuitGameTimer = null;

	// Godot Types
	private PlayerUI PlayerUICtrl = null;
	private InventoryUI InventoryUICtrl = null;

	// Basic Types
	private const float quitGameDelay = 2.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		QuitGameTimer = GetNode<Timer>("Quit Game");
		PlayerUICtrl = GetNode<PlayerUI>(
			"Player/Head/1st Person Camera/Player UI/Control");
		InventoryUICtrl = GetNode<InventoryUI>(
			"Player/Head/1st Person Camera/Inventory UI");

		Input.MouseMode = Input.MouseModeEnum.Captured;

		QuitGameTimer.Timeout += QuitGame;
		InventoryUICtrl.Closed += ResumeGame;
		InventoryUICtrl.Opened += PauseGame;
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
			}
		}
	}

	//-------------------------------------------------------------------------
	// Main Methods
	public void TogglePause() {
		if (!GetTree().Paused) {
			PauseGame();
		} else {
			ResumeGame();
		}
	}

	public void PauseGame() {
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetTree().Paused = true;

		GD.Print("Pause Game");
	}

	public void ResumeGame() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().Paused = false;
	}

	public void ToggleQuitGame() {
		if (Input.IsActionJustPressed("Quit Game"))
			QuitGameTimer.Start(quitGameDelay);
		if (Input.IsActionJustReleased("Quit Game"))
			QuitGameTimer.Stop();
	}

	public void QuitGame() {
		GetTree().Quit();
	}

	public void ToggleInventoryUI() {
		if (InventoryUICtrl.isOpen) {
			InventoryUICtrl.Close();
		} else {
			if (GetTree().Paused)
				return;

			InventoryUICtrl.Open();
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
