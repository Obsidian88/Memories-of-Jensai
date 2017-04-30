using UnityEngine;
using System.Collections;

public class TriggerDoorDust : MonoBehaviour {

    public GameObject Doordust;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TriggerDust()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y-15, transform.position.z);
        GameObject DoordustNew = Instantiate(Doordust, new Vector3(transform.position.x, transform.position.y - 15, transform.position.z), transform.rotation) as GameObject; // y -15
        DoordustNew.gameObject.SetActive(true);
    }
}
