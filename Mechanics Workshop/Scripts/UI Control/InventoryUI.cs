using Godot;
using System;

public partial class InventoryUI : Control
{
	//-------------------------------------------------------------------------
	// Game Componenets
	public GridContainer gridUI = null;
	public Panel DispItemPnl = null;

	// Godot Types
	[Signal]
	public delegate void OpenedEventHandler();
	[Signal]
	public delegate void ClosedEventHandler();
	private Main main = null;

	// Basic Types
	public Vector2[,] GridPos = null;
	private int invGridWidth = 8;
	private int invGridHeight = 4;
	public bool isOpen = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		gridUI = GetNode<GridContainer>("NinePatchRect/GridContainer");
		DispItemPnl = GetNode<Panel>("NinePatchRect/Display Item Panel");

		GridPos = new Vector2[invGridHeight, invGridWidth];
		GetGridPoss();

		Close();
	}

	//-------------------------------------------------------------------------
	// Inventory UI Methods
	public void Open() {
		Visible = true;
		isOpen = true;
		EmitSignal(SignalName.Opened);
	}

	public void Close() {
		Visible = false;
		isOpen = false;
		EmitSignal(SignalName.Closed);
	}

	public void GetGridPoss() {
		Control DispItemPnlCtrl = (Control) DispItemPnl;

		GD.Print(DispItemPnlCtrl.Size);

		float PnlWidth = DispItemPnlCtrl.Size.X;
		float PnlHeight = DispItemPnlCtrl.Size.Y;

		for (int i = 0; i < GridPos.GetLength(0); i++) {
			for (int j = 0; j < GridPos.GetLength(1); j++) {
				float ii = i * (PnlWidth / invGridWidth);
				float jj = j * (PnlHeight / invGridHeight);

				GridPos[i, j] = new Vector2(ii, jj);

				GD.Print($"{i}, {j}: ({ii}, {jj})");
			}
		}
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
