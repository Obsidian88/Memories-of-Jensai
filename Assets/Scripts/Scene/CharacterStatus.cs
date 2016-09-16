using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStatus : MonoBehaviour {

    public int maxHp;
    public int hp;
    public bool takingDamage = false;
    public float damageTime;
    public string gender = "male"; // Needed for genderspecific soundeffects

    private Rigidbody body;
    private CharacterMovement move;

    private float countdown = 0;

    public Animator HealthAnimator;
    public Image PanelDeath;

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

        // Refresh Healthbar-UI
        HealthAnimator.SetFloat("HealthBar", Mathf.Max(hp - args.damage, 0));
        if (hp <= 0)
        {
            // Death-event //
            // Increase corpsemass to prevent postmortem movement
            // Show DeathUI, start countdown on DeathUI and reload scene on countdown
            body.mass = 500000;
            PanelDeath.gameObject.SetActive(true);
            PanelDeath.GetComponentInChildren<IntegerCountdown>().StartCountdown();
            StartCoroutine(DelayedReloadScene(8f));
			
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

    public void PlayDeathSound()
    {
        //source.PlayOneShot(DeathSound, 1.0f);
    }

    IEnumerator DelayedReloadScene(float TimeToWait)
    {
        yield return new WaitForSeconds(TimeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
