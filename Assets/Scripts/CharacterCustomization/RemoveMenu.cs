using UnityEngine;
using System.Collections;

public class RemoveMenu : MonoBehaviour {
    Canvas canvas;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
