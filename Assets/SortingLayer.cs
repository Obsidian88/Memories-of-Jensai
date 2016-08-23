using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {

    public string Layer;
    public int Order;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingLayerName = Layer;
        GetComponent<Renderer>().sortingOrder = Order;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
