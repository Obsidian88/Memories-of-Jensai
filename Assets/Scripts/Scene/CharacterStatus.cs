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
    public Image Healthbar;
    public Text Healthbartext;

    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    bool damaged;                                               // True when the player gets damaged.

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

        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    void ApplyDamage(ApplyDamage.Parameter args)
    {
        damaged = true;
        hp-=args.damage;

        // Refresh Healthbar-UI
        HealthAnimator.SetFloat("HealthBar", Mathf.Max(hp - args.damage, 0));

        float damagedHealth = ((float)(hp - args.damage)) / 100f;
        StartCoroutine(DecreaseHealthbarOverTime(damagedHealth));
       
        Healthbartext.text = "Health " + ((float)(hp - args.damage)).ToString() + " / " + maxHp.ToString();
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

    // Make the healthbar loose health gradually over time
    IEnumerator DecreaseHealthbarOverTime(float damagedHealth)
    {
        int Speedratio = 1; // Higher values make a faster healthbar-animation
        while (Healthbar.fillAmount > damagedHealth)
        {
            Healthbar.fillAmount = Mathf.Lerp(Healthbar.fillAmount, damagedHealth-0.1f, Speedratio * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DelayedReloadScene(float TimeToWait)
    {
        yield return new WaitForSeconds(TimeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
