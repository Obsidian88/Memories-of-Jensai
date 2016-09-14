// Placed on the CameraObject to enable scrollwheel-zoomcontrols
using UnityEngine;
using System.Collections;

public class CameraZoomHandler : MonoBehaviour {

	public float LowerZoomInterval = 4f;
	public float UpperZoomInterval = 9f;
	public float InitialZoom = 7.0f;

    public float ZoomSensitivity = 0.5f;

    float Timer;
	float ScrollWheel;

	void Start () {
		Camera.main.orthographicSize = InitialZoom;
    }
	
	void Update() {
		ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
		Timer += Time.deltaTime;

        // Scrollwheel going down..
        if (ScrollWheel < 0 && Timer > 0.01f) 
		{
			Timer = 0;

			if (Camera.main.orthographicSize <= UpperZoomInterval) 
			{
				Camera.main.orthographicSize += ZoomSensitivity;
			}
        }
		
		// Scrollwheel going up..
		if (ScrollWheel > 0 && Timer > 0.01f) 
		{
			Timer = 0;

			if (Camera.main.orthographicSize >= LowerZoomInterval) 
			{
				Camera.main.orthographicSize -= ZoomSensitivity;
			}
        }
    }

}
