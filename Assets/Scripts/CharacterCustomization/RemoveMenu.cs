using UnityEngine;
using System.Collections;

public class RemoveMenu : MonoBehaviour {
    GameObject canvas;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        Destroy(canvas);
		
		// canvas = canvas.GetComponent<Canvas>();
		// canvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
