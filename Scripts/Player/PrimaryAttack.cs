using Godot;
using Godot.Collections;
using System;

public partial class PrimaryAttack : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
	private Node MainRoot = null;
	private CameraMovement CM = null;
	public AnimationPlayer RifleAnime = null;
	public RayCast3D GunBarrel = null;
	private RangeWeapon WeaponInst = null;
	public Node3D BulletInst = null;
	public PackedScene BulletRes = (PackedScene) GD.Load("res://3D Scenes/Bullet.tscn");

    // Godot Types

    // Basic Types
	public const float rayLength = 1000.0f;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
		MainRoot = GetTree().Root.GetChild(0);
		CM = GetNode<CameraMovement>("../Head");
        RifleAnime = GetNode<AnimationPlayer>("../Head/Rifle/AnimationPlayer");
		GunBarrel = GetNode<RayCast3D>("../Head/Rifle/RayCast3D");
		WeaponInst = GetNode<RangeWeapon>("../Head/Rifle");
    }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton 
			&& eventMouseButton.IsActionPressed("Primary Attack"))
		{
			// Cast Ray from Active Camera
			Dictionary target = CastRayFromCamera(eventMouseButton);

			ShootRifle();
		}
	}

    //-------------------------------------------------------------------------
    // Primary Attack Methods
	public void ShootRifle() {
		if (RifleAnime.IsPlaying())
		return;

		// Cast Ray from Camera

		// Play the Shoot Animation
		RifleAnime.Play("Shoot");

		// Instantiate the bullet
		BulletInst = (Node3D) BulletRes.Instantiate();
		BulletInst.Position = GunBarrel.GlobalPosition;
		BulletInst.Basis = GunBarrel.GlobalTransform.Basis;
		MainRoot.AddChild(BulletInst);
	}

	public Dictionary CastRayFromCamera(InputEventMouseButton eventMB) {
		// Generate the From and To Vectors
		Vector3 from = CM.ActiveCamera.ProjectRayOrigin(eventMB.Position);
		Vector3 to = from + CM.ActiveCamera.ProjectRayNormal(eventMB.Position) * rayLength;

		// Perform the Ray Cast Query
		PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		PhysicsRayQueryParameters3D query = 
			PhysicsRayQueryParameters3D.Create(from, 
											   to 
											   );
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

    //-------------------------------------------------------------------------
    // Demo Methods
}