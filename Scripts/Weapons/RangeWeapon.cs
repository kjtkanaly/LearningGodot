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
	[Export] public WeaponData Params = null;
	public MeshInstance3D Mesh3D = null;

	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;
	public bool isReloading = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		CM = GetParent<CameraMovement>();
		Anime = GetNode<AnimationPlayer>("AnimationPlayer");
		Mesh3D = GetNode<MeshInstance3D>("MeshInstance3D");
		PA = GetNode<PrimaryAttack>("../../Primary Attack");

		GetActiveCamera();
		Params.currBullet = Params.magazineSize;

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
		return Params.currBullet;
	}

	public int IncrementBulletCount() {
		Params.currBullet -= 1;

		IsMagazineEmpty();

		return Params.currBullet;
	}

	public bool IsMagazineEmpty() {
		if (Params.currBullet <= 0) {
			return true;
		}
		return false;
	}

	public void BeginAmmoReload() {
		Anime.Play("Reload");
	}

	public void ReloadAmmo(StringName AnimationName) {
		if (AnimationName == "Reload") {
			Params.currBullet = Params.magazineSize;
			PA.ReloadRangeWeaponUI();
		}
	}

	public void PlayShootAnime() {
		Anime.Play("Shoot");
	}
	//-------------------------------------------------------------------------
	// Demo Methods
}
