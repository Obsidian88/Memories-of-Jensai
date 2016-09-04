using UnityEngine;
using System.Collections;



public class TorchAI : MonoBehaviour {
    public float MaxSpeed = 20f;

    public GameObject parent;

    Transform target = null;
    private Rigidbody Rigidbody;
    private EnemyStatus stat;

    // Use this for initialization
    void Start () {
        Rigidbody = parent.GetComponent<Rigidbody>();
        stat = parent.GetComponent<EnemyStatus>();
    }
	
	// Update is called once per frame
	void Update () {
        if (stat.hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = parent.transform.position;
        if (target != null && !stat.takingDamage)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            Rigidbody.velocity = direction * MaxSpeed;
        }
        else
        {
            Rigidbody.velocity = new Vector3(0,0,0);
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
        if (obj.tag == "Character" && target!=null)
        {
            target = null;
        }
    }
}
