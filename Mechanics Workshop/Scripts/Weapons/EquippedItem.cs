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
	public AnimationPlayer Anime = null;
	public MeshInstance3D Mesh3D = null;

	// Godot Types
	private PrimaryAttack PA = null;
	public Item ItemParams = null;
	public PlayerUI PlyrUI = null;

	// Basic Types
	private float rayLength = 1000.0f;
	public bool isReloading = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		MainRoot = GetTree().Root.GetChild(0);
		CM = GetNode<CameraMovement>("../../../Camera Director");
		Anime = GetNode<AnimationPlayer>("../../../Visual Director/Animation Director");
		Mesh3D = GetNode<MeshInstance3D>("../../../Visual Director/Mesh Director");
		PA = GetNode<PrimaryAttack>("Primary Action");
		PlyrUI = GetNode<PlayerUI>("../../../Camera Director/First Person Camera/UI Director/Player UI/Control");
		Head = GetNode<Node3D>("../../../Camera Director");

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

	public void RotateItem() {
		Vector3 rot = Rotation;
		rot.X = Head.Rotation.X;
		Rotation = rot;
	}

	public void equipItem(Item item) {
		ItemParams = item;
		Mesh3D.Mesh = item.itemData.mesh;

		if (item.itemType == Item.ItemType.Weapon) 
			PlyrUI.UpdateAmmoCountLbl(
				item.weaponData.magazineSize, 
				item.weaponData.currBullet);
	}

	public void DropItem() {
		if (ItemParams == null)
			return;

		ItemParams = null;
		Mesh3D.Mesh = null;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
