using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float countdown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (countdown < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            countdown -= Time.deltaTime;
        }
	}
}
