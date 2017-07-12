using UnityEngine;
using System.Collections;


public class SapplingAI : MonoBehaviour
{
    public float RunSpeed = 8f;
    public GameObject parentRigidbody;

    private Transform target = null;
    private Rigidbody Rigidbody;

    // Use this for initialization
    void Start()
    {
        Rigidbody = parentRigidbody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parentRigidbody.transform.position;
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            Rigidbody.velocity = direction * RunSpeed;
        }
        else
        {
            Rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Character")
        {
            target = obj.transform;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "Character" && target != null)
        {
            target = null;
        }
    }
}
