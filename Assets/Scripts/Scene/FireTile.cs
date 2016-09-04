using UnityEngine;
using System.Collections;

public class FireTile : MonoBehaviour {

    public GameObject fire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Activate()
    {
        Instantiate(fire,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
