using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float jumpHeight;
	public float speed;

	public Color color = new Color (0.5f, 0.2f, 0.3f, 0.5f);

	void Update ()
	{

	}

	void FixedUpdate ()
	{
		/////////////Movement//////////////////
		float moveHoriz = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3(moveHoriz, 0.0f, 0.0f);
		rigidbody.AddForce (movement * speed * Time.deltaTime);
		///////////////////////////////////////
		
		
		/////////////RayCasting/Jumping//////////////
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1f)) 
		{	
			if (Input.GetButtonDown ("Jump"))  //if the player is in contact with the floor then let the player jump
			{
				rigidbody.velocity = new Vector3 (moveHoriz, jumpHeight, 0);
				
			}
		} 
		else 
		{
			//if the player is in midair this will print to console and the player cant jump
		}
		//////////////////////////////////////
	}

    public void AddPlayerAction(BlockEnum.BlockPropertyItem action)
    {
        Debug.Log("Adding player controller action");
    }
}