using Godot;
using System;

public static class GeneralStatic
{
	public static Vector2 MagnitudeClamp(Vector2 v, float maxMag) {
		float mag = v.Length();

		if (mag <= maxMag)
			return v;

		return (v.Normalized() * maxMag);
	}
}
