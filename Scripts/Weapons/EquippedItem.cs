using Godot;
using System;

public partial class EquippedItem : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public Node MainRoot = null;
	public CameraMovement CM;
	public Node3D Head = null;
	public Camera3D ActiveCamera = null;
	private Viewport View;
	public PlayerUI PlyrUI = null;
	public AnimationPlayer Anime = null;
	private PrimaryAttack PA = null;
	public WeaponData Params = null;
	public MeshInstance3D Mesh3D = null;

	// Godot Types

	// Basic Types
	private float rayLength = 1000.0f;
	public bool isReloading = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		MainRoot = GetTree().Root.GetChild(0);
		CM = GetNode<CameraMovement>("../../Head");
		Anime = GetNode<AnimationPlayer>("Anime");
		Mesh3D = GetNode<MeshInstance3D>("Mesh3D");
		PA = GetNode<PrimaryAttack>("Primary Attack");
		PlyrUI = GetNode<PlayerUI>("../../Head/1st Person Camera/Player UI/Control");
		Head = GetNode<Node3D>("../../Head");

		GetActiveCamera();

		Anime.AnimationFinished += PA.ReloadAmmo;
	}

	public override void _PhysicsProcess(double delta) {
		GetActiveCamera();
		RotateItem();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Drop Item")) {
				DropItem();
			}
		}
	}

	//-------------------------------------------------------------------------
	// RangeWeapon Methods
	public void GetActiveCamera() {
		ActiveCamera = CM.ActiveCamera;
	}

	public void RotateItem(){
		Vector3 rot = Rotation;
		rot.X = Head.Rotation.X;
		Rotation = rot;
	}

	public void DropItem() {
		if (Params == null)
			return;

		Params = null;
		Mesh3D.Mesh = null;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
