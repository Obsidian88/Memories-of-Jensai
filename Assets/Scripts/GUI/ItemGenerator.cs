using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

    public GameObject ItemToSpawn;
    private MeshCollider FloorplaneCollider;

    public Transform Raycaster;

    public float RateOfSpawn = 2;

    private float nextSpawn = 0;
    public int count = 1;
    public int maximumSpawns = 2;

    void Start()
    {
        //GameObject.Find("InnerFloorPlane").GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && count <= maximumSpawns)
        {
            nextSpawn = Time.time + RateOfSpawn;

            //Vector3 randomPoint = GetPointOnMesh().point;
            Vector3 randomPoint = new Vector3(Random.Range(-300, 75), -3.891f, Random.Range(-20, 20));
            Instantiate(ItemToSpawn, randomPoint, Quaternion.identity);
            count++;
        }
    }
	
	//RaycastHit GetPointOnMesh()
	//{
 //       float length = 100.0f;
 //       //Vector3 direction = Random.onUnitSphere;
 //       //Vector3 direction = new Vector3();
 //       //Vector3 dir = (this.transform.position - camera.transform.position).normalized
 //       Ray ray = new Ray(Raycaster.position, -transform.up);
 //       RaycastHit hit;
 //       gameObject.GetComponent<Collider>().Raycast (ray, out hit, length);
 //       return hit;
 //   }
}
