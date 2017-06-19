using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

    // being used on GameObject "PanelMenu"

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
