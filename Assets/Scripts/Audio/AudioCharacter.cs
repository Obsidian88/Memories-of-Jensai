using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioCharacter : MonoBehaviour {

    private string absolutePathMaleDeath = "Audio/Sounds/Character/Death/Male";
    private string absolutePathMaleHurt = "Audio/Sounds/Character/Hurt/Male";
    private string absolutePathFemaleDeath = "Audio/Sounds/Character/Death/Female";
    private string absolutePathFemaleHurt = "Audio/Sounds/Character/Hurt/Female";
    public AudioClip SoundOfFootstep;

    private AudioClip[] maleDeaths;
    private AudioClip[] maleHurts;
    private AudioClip[] femaleDeaths;
    private AudioClip[] femaleHurts;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private CharacterStatus CharStatus;
    private string gender;

    private bool dead = false;
    private float lastHP;

    // Use this for initialization
    void Start () {
        CharStatus = GetComponentInParent<CharacterStatus>();

        lastHP = CharStatus.hp;
        gender = CharStatus.gender;

        gameObject.AddComponent<AudioSource>();

        maleDeaths = Resources.LoadAll<AudioClip>(absolutePathMaleDeath);
        maleHurts = Resources.LoadAll<AudioClip>(absolutePathMaleHurt);
        femaleDeaths = Resources.LoadAll<AudioClip>(absolutePathFemaleDeath);
        femaleHurts = Resources.LoadAll<AudioClip>(absolutePathFemaleHurt);
    }

    void Update ()
    {

        if (gender == "male")
        {
            if(CharStatus.hp < lastHP && dead == false)
            {
                PlayMaleHurtSound();
                lastHP = CharStatus.hp;
            }
            if(CharStatus.hp <= 0 && dead == false)
            {
                PlayMaleDeathSound();
                dead = true;
            }
        }
        else if (gender == "female")
        {
            if (CharStatus.hp < lastHP && dead == false)
            {
                PlayFemaleHurtSound();
                lastHP = CharStatus.hp;
            }
            if (CharStatus.hp <= 0 && dead == false)
            {
                PlayFemaleDeathSound();
                dead = true;
            }
        }
    }

   public void PlayFootstepSound()
    {
        source.PlayOneShot(SoundOfFootstep, 1.0f);
    }

    public void PlayMaleDeathSound()
    {
        int clipToPlay = Random.Range(0, maleDeaths.Length);
        source.PlayOneShot(maleDeaths[clipToPlay], 1.0f);
    }

    public void PlayMaleHurtSound()
    {
        int clipToPlay = Random.Range(0, maleHurts.Length);
        source.PlayOneShot(maleHurts[clipToPlay], 0.7f);
    }

    public void PlayFemaleDeathSound()
    {
        int clipToPlay = Random.Range(0, femaleDeaths.Length);
        source.PlayOneShot(femaleDeaths[clipToPlay], 1.0f);
    }

    public void PlayFemaleHurtSound()
    {
        int clipToPlay = Random.Range(0, femaleHurts.Length);
        source.PlayOneShot(femaleHurts[clipToPlay], 0.7f);
    }

}