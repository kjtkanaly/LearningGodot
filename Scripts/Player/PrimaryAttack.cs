using Godot;
using Godot.Collections;
using System;

public partial class PrimaryAttack : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public RayCast3D AttackNormal = null;
	public EquippedItem EquipItem = null;
	public Node3D BulletInst = null;
	public PackedScene BulletRes = (PackedScene) GD.Load("res://3D Scenes/Bullet.tscn");

	// Godot Types

	// Basic Types
	public const float rayLength = 1000.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		AttackNormal = GetNode<RayCast3D>("../Barrel");
		EquipItem = GetNode<EquippedItem>("../");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton) {
			if (eventMouseButton.IsActionPressed("Primary Attack")) {
				if (EquipItem.ItemParams == null)
					return;

				// Cast Ray from Active Camera
				Dictionary target = CastRayFromCamera(eventMouseButton);
				ShootRifle();
			}
		}

		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Reload")) {
				BeginAmmoReload();
			}
		}
	}

	//-------------------------------------------------------------------------
	// Primary Attack Methods
	public void ShootRifle() {
		if (EquipItem.Anime.IsPlaying() || IsMagazineEmpty())
			return;

		// Cast Ray from Camera

		// Play the Shoot Animation
		PlayShootAnime();

		// Instantiate the bullet
		BulletInst = (Node3D) BulletRes.Instantiate();
		BulletInst.Position = AttackNormal.GlobalPosition;
		BulletInst.Basis = AttackNormal.GlobalTransform.Basis;
		EquipItem.MainRoot.AddChild(BulletInst);

		// Log the consumption of ammo
		IncrementBulletCount();

		// Update Player UI
		EquipItem.PlyrUI.UpdateAmmoCountLbl(
			EquipItem.ItemParams.weaponData.magazineSize, 
			EquipItem.ItemParams.weaponData.currBullet);
	}

	public Dictionary CastRayFromCamera(InputEventMouseButton eventMB) {
		// Generate the From and To Vectors
		Vector3 from = EquipItem.CM.ActiveCamera.ProjectRayOrigin(eventMB.Position);
		Vector3 to = from + EquipItem.CM.ActiveCamera.ProjectRayNormal(eventMB.Position) * rayLength;

		// Perform the Ray Cast Query
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(
			from, 
			to);
		return spaceState.IntersectRay(query);

		/*if (result.Count > 0)
		{
			GodotObject obj = result["collider"].AsGodotObject();
			//GD.Print("Object: ", obj);

			// Node3D temp = (Node3D) result["collider"].AsGodotObject()._Get("Node3D");

			return (Vector3)result["position"];
		}

		return Vector3.Inf;
		*/
	}

	public void ReloadRangeWeaponUI() {
		EquipItem.PlyrUI.UpdateAmmoCountLbl(
			EquipItem.ItemParams.weaponData.magazineSize, 
			EquipItem.ItemParams.weaponData.currBullet);
	}

	public int IncrementBulletCount() {
		EquipItem.ItemParams.weaponData.currBullet -= 1;

		IsMagazineEmpty();

		return EquipItem.ItemParams.weaponData.currBullet;
	}

	public bool IsMagazineEmpty() {
		if (EquipItem.ItemParams.weaponData.currBullet <= 0) {
			return true;
		}
		return false;
	}

	public void BeginAmmoReload() {
		EquipItem.Anime.Play("Reload");
	}

	public void ReloadAmmo(StringName AnimationName) {
		if (AnimationName == "Reload") {
			EquipItem.ItemParams.weaponData.currBullet = 
				EquipItem.ItemParams.weaponData.magazineSize;
			ReloadRangeWeaponUI();
		}
	}

	public void PlayShootAnime() {
		EquipItem.Anime.Play("Shoot");
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
