using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour 
{
	public GameObject player;
	private Vector3 offset;

	public float distance;
	private float damping = 2.5f;
	private float min = -15;
	private float max = -2;
	private Vector3 zDistance;
		
	void Start ()
	{
		offset = transform.position;
		distance = -10f;
		distance = transform.localPosition.z;
	}

	void Update()
	{
			//for a scroll wheel to be used if you need to zoom in, not used at the moment
			distance -= Input.GetAxis("Mouse ScrollWheel") * 7.5f;
			distance = Mathf.Clamp (distance, min, max);
			zDistance.z = Mathf.Lerp (transform.localPosition.z, distance, Time.deltaTime * damping);
			transform.localPosition = zDistance;

	}

	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
	}
	
}
