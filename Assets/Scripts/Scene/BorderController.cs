using UnityEngine;
using System.Collections;

public class BorderController : MonoBehaviour {
    
    public Collider[] ignore;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        foreach (Collider collider in ignore)
        {
            if (collision.collider == collider)
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

}
