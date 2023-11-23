using Godot;
using System;

public partial class RangeWeapon : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public CameraMovement CM;
	public Node3D Head;
	private Camera3D ActiveCamera = null;
	private Viewport View;
	public AnimationPlayer Anime = null;
	private PrimaryAttack PA = null;

	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;
	public float reloadTime = 0.0f;
	[Export] public int magazineSize = 12;
	public int currentBullet = 0;
	public bool outOfBullets = false;
	public bool isReloading = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		CM = GetParent<CameraMovement>();
		Anime = GetNode<AnimationPlayer>("AnimationPlayer");
		PA = GetNode<PrimaryAttack>("../../Primary Attack");

		GetActiveCamera();
		currentBullet = magazineSize;
		reloadTime = Anime.GetAnimation("Reload").Length;

		Anime.AnimationFinished += ReloadAmmo;
	}

	public override void _PhysicsProcess(double delta) {
		GetActiveCamera();

		Rotation = ActiveCamera.Rotation;
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	public void GetActiveCamera() {
		ActiveCamera = CM.ActiveCamera;
	}

	public int GetCurrentAmmo() {
		return currentBullet;
	}

	public int IncrementBulletCount() {
		currentBullet -= 1;

		IsMagazineEmpty();

		return currentBullet;
	}

	public bool IsMagazineEmpty() {
		if (currentBullet <= 0) {
			outOfBullets = true;
		}
		return outOfBullets;
	}

	public void BeginAmmoReload() {
		Anime.Play("Reload");
	}

	public void ReloadAmmo(StringName AnimationName) {
		if (AnimationName == "Reload") {
			currentBullet = magazineSize;
			PA.ReloadRangeWeaponUI();
		}
	}

	public void PlayShootAnime() {
		Anime.Play("Shoot");
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
