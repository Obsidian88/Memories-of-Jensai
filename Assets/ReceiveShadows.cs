using UnityEngine;
using System.Collections;

public class ReceiveShadows : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().receiveShadows = true;
        GetComponent<SpriteRenderer>().shadowCastingMode= UnityEngine.Rendering.ShadowCastingMode.On;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
