using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour 
{
	public enum PlayerProperty
	{
		PLAYER_SPEED,			//how fast the player can travel in a single direction
		PLAYER_LIFE,			//will be set to either 1 for alive or 0 for dead
		PLAYER_JUMP_ACTIVE,		//is the player jumping or not, 1 for yes 0 for no
		PLAYER_JUMP_HEIGHT		//how high can the player jump

	}
}
