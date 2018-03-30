using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {

	// Core level info
	public static float TILE_SEPARATION = 5.0f;

	public static int BASE_HEALTH = 4;
	public static int BASE_LIGHT = 100;
	public static int UPGRADEINCREMENT_HEALTH = 1;
	public static int UPGRADEINCREMENT_LIGHT = 10;

	// Interface values
	// HUD
	public static Color TURN_COLOR_PLAYER = new Color(0f / 255f, 141f / 255f, 199f / 255f, 255f / 255f);
	public static Color TURN_COLOR_ALLY = new Color(0f / 255f, 111f / 255f, 5f / 255f, 255f / 255f);
	public static Color TURN_COLOR_ENEMY = new Color(172f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
	public static Color TURN_COLOR_PAUSE = new Color(199f / 255f, 0f / 255f, 185f / 255f, 255f / 255f);
	public static Color INACTIVE_LIGHT_COLOR = new Color(124f / 255f, 124f / 255f, 124f / 255f, 176f / 255f);
}
