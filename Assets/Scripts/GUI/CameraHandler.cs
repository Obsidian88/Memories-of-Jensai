// Placed on cameraObject to have more controls
using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {

	public GameObject Character;
	public float rotateSpeed = 3.0f;
	public float LowerZoomInterval = 4f;
	public float ZoomSensitivity = 0.5f;
	public float UpperZoomInterval = 10f;
	public float InitialZoom = 7.5f;

    [HideInInspector]
    public int Facing = 0;
	
	//private Quaternion rotation = Quaternion.identity;
	
	float CurrentZoom;
	float Timer;
	float ScrollWheel;

	void Start () {
		CurrentZoom = InitialZoom;
		UpdateZoom();
		transform.LookAt(Character.transform.position);
        Debug.Log("Current Facing: " + Facing);
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

        // Rotate perspective 90° CCW
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Facing = (Facing - 3) % 12;
            Debug.Log("Current Facing: " + Facing);
            Character.transform.Rotate(new Vector3(0, -90, 0));
            //transform.LookAt(Character.transform.position);
        }

        // Rotate perspective 90° CW
        if (Input.GetKeyDown(KeyCode.E))
        {
            Facing = (Facing + 3) % 12;
            Debug.Log("Current Facing: " + Facing);
            Character.transform.Rotate(new Vector3(0, 90, 0));
            //transform.LookAt(Character.transform.position);
        }
    }

	void UpdateZoom() {
        if(Facing == 0)
        { 
            Vector3 position = new Vector3(0.0f, 0.8f * CurrentZoom, 1.8f * -CurrentZoom) + Character.transform.position;
            transform.position = position;
        }
        if(Facing == 6 || Facing == -6)
        {
            Vector3 position = new Vector3(0.0f, 0.8f * CurrentZoom, 1.8f * CurrentZoom) + Character.transform.position;
            transform.position = position;
        }
        if (Facing == 3 || Facing == -9)
        {
            Vector3 position = new Vector3(1.8f * -CurrentZoom, 0.8f * CurrentZoom, 0) + Character.transform.position;
            transform.position = position;
        }
        if (Facing == 9 || Facing == -3)
        {
            Vector3 position = new Vector3(1.8f * CurrentZoom, 0.8f * CurrentZoom, 0) + Character.transform.position;
            transform.position = position;
        }
        //transform.LookAt(Character.transform.position);

	}
}
