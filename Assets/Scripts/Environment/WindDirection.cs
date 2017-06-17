using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirection : MonoBehaviour {
    public int windDirection;
    public float maxWindstrength;
    public float maxWindsduration;
    public float currentWindstrength;
    public float currentWindduration;

    // Use this for initialization
    void Start () {
        currentWindduration = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWindduration <= 0)
        {
            windDirection = Random.Range(-1, 2);
            currentWindstrength = Random.Range(0, maxWindstrength);
            currentWindduration = Random.Range(0, maxWindsduration);
        }
        else
        {
            currentWindduration -= Time.deltaTime;
        }
	}
}
