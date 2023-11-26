using Godot;
using System;

public partial class PlayerUI : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private Label AmmoCountLbl = null;
	private PrimaryAttack PA = null;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		AmmoCountLbl = GetNode<Label>("MarginContainer/Ammo Counter V-Container/Ammo Counter H-Container/Ammo Count");
		PA = GetNode<PrimaryAttack>("../../../../Primary Attack");
		
	}

	//-------------------------------------------------------------------------
	// PlayerUI Methods
	public void UpdateAmmoCountLbl(int MaxCount, int CurrCount) {
		string[] ammoLbl = AmmoCountLbl.Text.Split(" / ");

		ammoLbl[0] = CurrCount.ToString();
		ammoLbl[1] = MaxCount.ToString();

		AmmoCountLbl.Text = ammoLbl[0] + " / " + ammoLbl[1];
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
