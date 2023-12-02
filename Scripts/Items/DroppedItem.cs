using Godot;
using System;

public partial class DroppedItem : Node3D
{
	public enum ItemType {
		Default = 0,
		Weapon = 1,
		Basic = 2
	}

	//-------------------------------------------------------------------------
	// Game Componenets
	private RayCast3D GroundRay = null;
	private MeshInstance3D Mesh3D = null;

	// Godot Types
	[Export] public ItemType itemType = ItemType.Default;
	[Export] public Vector3 velocity = new Vector3();

	// Basic Types
	private float time = 0.0f;
	[Export] public float amp = 0.5f;
	[Export] public float frequency = 0.5f;
	[Export] public float yPos = 0.0f;
	[Export] public float angularSpeed = 0.5f;
	[Export] public float fallSpeed = 10.0f;
	[Export] public float fallAccl = -10.0f;
	private bool previousGroundCheck, currentGroundCheck;

	//-------------------------------------------------------------------------
	// Game Events

	public override void _PhysicsProcess(double delta)
	{
		CheckIfAtCorrectYLevel((float) delta);
		RaiseAndLower((float) delta);
		RotateItem((float) delta);
	}

	//-------------------------------------------------------------------------
	// Dropped Item Methods
	public void RaiseAndLower(float delta) {
		if (Mesh3D is null) 
			Mesh3D = GetNode<MeshInstance3D>("Model");

		Vector3 pos = Mesh3D.Position;
		time += delta;
		pos.Y = amp * MathF.Sin(2 * MathF.PI * frequency * time) + yPos;
		Mesh3D.Position = pos;
	}

	public void RotateItem(float delta) {
		Mesh3D.RotateY(angularSpeed * delta);
	}

	public void CheckIfAtCorrectYLevel(float delta) {
		if (GroundRay is null)
			GroundRay = GetNode<RayCast3D>("Ground Check Ray");

		currentGroundCheck = GroundRay.IsColliding();

		if (!previousGroundCheck && currentGroundCheck)
			ResetPhysicProps();

		if (!currentGroundCheck)
			ApplyGravity(delta);

		previousGroundCheck = currentGroundCheck;
	}

	public void ApplyGravity(float delta) {
		Vector3 Pos = Position;
		float Vi = velocity.Y;
		float Vf = Vi + fallAccl * delta;
		Pos.Y += (Vi + Vf) / 2 * delta;
		Position = Pos;		
		velocity.Y = Vf;
	}

	private void ResetPhysicProps() {
		velocity = Vector3.Zero;
	}	
	//-------------------------------------------------------------------------
	// Demo Methods
}
