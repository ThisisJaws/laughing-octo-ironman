using UnityEngine;
using System.Collections;

public class FocalPointRotation : MonoBehaviour 
{
	void Update()
	{
		transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime);
	}
}
