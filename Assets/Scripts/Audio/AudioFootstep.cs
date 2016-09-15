using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioFootstep : MonoBehaviour {

    public AudioClip SoundOfFootstep;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = SoundOfFootstep;
        source.playOnAwake = false;
        source.volume = 1.0f;
    }

   public void PlayFootstepSound()
    {
        source.PlayOneShot(SoundOfFootstep, 1.0f);
    }

}   


