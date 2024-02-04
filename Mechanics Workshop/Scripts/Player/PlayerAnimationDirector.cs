using Godot;
using System;

public partial class PlayerAnimationDirector : AnimationPlayer
{
	//-------------------------------------------------------------------------
	// Game Componenets

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Player Animation Director Methods
	public void PlayRollAnimation() {
		Play("Roll");
	}

	public bool CheckPlayingStatus() {
		return IsPlaying();
	}

	public string GetCurrentAnimationName() {
		// (Anime.CurrentAnimation == "Roll")
		return CurrentAnimation;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
