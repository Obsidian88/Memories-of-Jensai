// Placed on cameraObject to have more controls
using UnityEngine;
using System.Collections;

public class CameraZoomHandler : MonoBehaviour {

	public GameObject Character;
	public float rotateSpeed = 3.0f;
	public float LowerZoomInterval = 4f;
	public float ZoomSensitivity = 0.5f;
	public float UpperZoomInterval = 10f;
	public float InitialZoom = 7.5f;

	
	float CurrentZoom;
	float Timer;
	float ScrollWheel;

	void Start () {
		CurrentZoom = InitialZoom;
        UpdateZoom();
		transform.LookAt(Character.transform.position);
    }
	
	void Update() {
		ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
		Timer += Time.deltaTime;
		
		// Scrollwheel going up..
		if (ScrollWheel > 0 && Timer > 0.01f) 
		{
			Timer = 0;
			CurrentZoom -= ZoomSensitivity;

			if (CurrentZoom <= LowerZoomInterval) 
			{
				CurrentZoom = LowerZoomInterval;
			}
            UpdateZoom();
        }
		
		// Scrollwheel going down..
		if (ScrollWheel < 0 && Timer > 0.01f) 
		{
			Timer = 0;
			CurrentZoom += ZoomSensitivity;

			if (CurrentZoom >= UpperZoomInterval) 
			{
				CurrentZoom = UpperZoomInterval;
			}
            UpdateZoom();
        }
    }

	 void UpdateZoom()
     {
            // doesn't work yet because of ortographic camera, need to find a solution yet..
            //Vector3 position = new Vector3(0.0f, 0.8f * CurrentZoom, 1.8f * CurrentZoom) + Character.transform.position;
            //transform.position = position;
	 }
}
