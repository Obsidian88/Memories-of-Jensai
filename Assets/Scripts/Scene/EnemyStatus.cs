using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

    public int maxHp;
    public int hp;
    public bool takingDamage = false;
    public float damageTime;

    private Rigidbody body;

    private float countdown = 0;

    // Use this for initialization
    void Start()
    {
        hp = Mathf.Min(hp, maxHp);
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            takingDamage = true;
            countdown -= Time.deltaTime;
        }
        else
        {
            takingDamage = false;
        }
    }

    void ApplyDamage(ApplyDamage.Parameter args)
    {
        hp -= args.damage;
        if (hp<=0)
        {
            Destroy(gameObject);
        }
        countdown = damageTime;
        Vector3 incoming = args.direction;
        incoming.Normalize();
        Vector3 direction = body.velocity;
        direction.Normalize();
        Vector3 force;
        if (incoming.Equals(new Vector3(0, 0, 0)))
        {
            force = -direction;
        }
        else if (direction.Equals(new Vector3(0, 0, 0)))
        {
            force = incoming;
        }
        else
        {
            force = incoming + direction;
            force.Normalize();
        }
        if (force.Equals(direction)) force = -direction;
        body.velocity = new Vector3(0, 0, 0);
        body.AddForce(force * args.force, ForceMode.Impulse);
    }
}
