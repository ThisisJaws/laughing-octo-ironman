using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float HorizontalSpeedModifier;
    public float VerticalSpeedModifier;
    public float ScrollSpeedModifier;

    private Vector3 m_CamPosition;

    void Start()
    {
        m_CamPosition = transform.position;
    }
	
	void Update () 
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        m_CamPosition.x += inputX * HorizontalSpeedModifier;
        m_CamPosition.y += inputY * VerticalSpeedModifier;
        m_CamPosition.z += mouseScroll * ScrollSpeedModifier;

        transform.position = m_CamPosition;
	
	}
}
