using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoBehaviour {

    public int maxHp;
    public int hp;
    public bool takingDamage = false;
    public float damageTime;

    private Rigidbody body;
    private CharacterMovement move;

    private float countdown = 0;

    // Use this for initialization
    void Start () {
        hp = Mathf.Min(hp, maxHp);
        body = GetComponent<Rigidbody>();
        move = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (countdown > 0)
        {
            move.enabled = false;
            takingDamage = true;
            countdown -= Time.deltaTime;
        }
        else
        {
            move.enabled = true;
            takingDamage = false;
        }
	}

    void ApplyDamage(ApplyDamage.Parameter args)
    {
        hp-=args.damage;
		// TODO:
		// Play Hurtanimation
		// Play Hurtsound
		// Refresh Healthbar-UI
		// GameObject-That-Is-The-Healthbar.Value = Mathf.Max(hp - args.damage, 0)
        if (hp <= 0)
        {
			// TODO:
			// PlayDeathSound
			// Show text "You died"
			// Start coroutine with 3 seconds ..
			//.. then reload the current scene automatically
			StartCoroutine(DelayedReloadScene(3f));
			
			
            //Destroy(gameObject);
        }
        countdown = damageTime;
        Vector3 incoming = args.direction;
        incoming.Normalize();
        Vector3 direction = body.velocity;
        direction.Normalize();
        Vector3 force;
        if (incoming.Equals(new Vector3(0, 0, 0)))
        {
            force =-direction;
        } 
        else if(direction.Equals(new Vector3(0, 0, 0)))
        {
            force = incoming;
        }
        else
        {
            force = incoming + direction;
            force.Normalize();
        }
        if (force.Equals(direction)) force = -direction;
        move.enabled = false;
        body.velocity = new Vector3(0, 0, 0);
        body.AddForce(force * args.force, ForceMode.Impulse);
    }
	
	IEnumerator DelayedReloadScene(float TimeToWait)
    {
        yield return new WaitForSeconds(TimeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
